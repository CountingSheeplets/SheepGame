using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class MenuNetworkHandler : Singleton<MenuNetworkHandler> {
	//public List<int> premiumIds = new List<int>();
	public List<Owner> waitLine = new List<Owner>();
	public List<int> connectedIds = new List<int>();
	void Start() {
		if (AirConsole.instance == null)
			return;
		AirConsole.instance.onConnect += OnConnect;
		AirConsole.instance.onPremium += OnPremium;
		AirConsole.instance.onDisconnect += OnDisconnect;
	}
	void OnConnect(int device_id) {
		Owner owner = OwnersCoordinator.TryCreateOwner(device_id);
		if (!connectedIds.Contains(device_id))
			connectedIds.Add(device_id);
		if (GameStateView.HasState(GameState.started)) {
			Owner ownerRec = OwnersCoordinator.ReconnectOwner(device_id);
			if (ownerRec != null) {
				EventCoordinator.TriggerEvent(EventName.Input.Network.PlayerJoined(), GameMessage.Write().WithOwner(ownerRec));
				return;
			}
			//Debug.LogWarning("New cannot join, game already started");
			NetworkCoordinator.SendShowView(device_id, "in_game");
			return;
		}
		int count = OwnersCoordinator.GetOwners().Where(x => x.connected).ToList().Count;
		if (count > 7) {
			NetworkCoordinator.SendShowView(device_id, "max_players");
			waitLine.Add(owner);
			return;
		}
		if (owner) {
			TrySetupFirstOwner();
			NetworkCoordinator.SendShowView(device_id, "menu");
			EventCoordinator.TriggerEvent(EventName.Input.Network.PlayerJoined(), GameMessage.Write().WithOwner(owner));
		} // else
		//	Debug.LogError("OnConnect returned null Owner!");
	}
	public void OnDisconnect(int device_id) {
		Owner owner = OwnersCoordinator.DisconnectOwner(device_id);
		if (connectedIds.Contains(device_id))
			connectedIds.Remove(device_id);
		if (!owner) {
			//Debug.LogWarning("OnDisconnect returned null Owner! Nothing to disconnect...");
			return;
		}
		EventCoordinator.TriggerEvent(EventName.Input.Network.PlayerLeft(), GameMessage.Write().WithOwner(owner));
		if (GameStateView.HasState(GameState.started)) {
			return;
		}
		if (owner.IsFirstOwner) {
			owner.IsFirstOwner = false;
			TrySetupFirstOwner();
		}
		foreach (Owner stayingOwner in OwnersCoordinator.GetOwners()) {
			stayingOwner.ready = false;
		}
		if (waitLine.Count > 0) {
			Owner fromQueue = waitLine[0];
			if (fromQueue) {
				fromQueue.teamId = OwnersCoordinator.GetNewTeamId();
				NetworkCoordinator.SendShowView(fromQueue.deviceId, "menu");
				EventCoordinator.TriggerEvent(EventName.Input.Network.PlayerJoined(), GameMessage.Write().WithOwner(fromQueue));
			}
			waitLine.Remove(fromQueue);
		}
	}
	public static void ReloadAllConnected() {
		foreach (int id in Instance.connectedIds) {
			Instance.OnConnect(id);
		}
	}
	private void OnDestroy() {
		if (AirConsole.instance != null) {
			AirConsole.instance.onConnect -= OnConnect;
			AirConsole.instance.onPremium -= OnPremium;
			AirConsole.instance.onDisconnect -= OnDisconnect;
		}
	}
	void OnPremium(int device_id) {
		PremiumCoordinator.AddPremium(device_id);
		OwnersCoordinator.GetOwner(device_id).GetPlayerProfile().SetPremium();
	}

	void TrySetupFirstOwner() {
		if (!OwnersCoordinator.ContainsActiveFirstOwner() && OwnersCoordinator.GetOwnersAll().Count > 0)
			OwnersCoordinator.GetOwnersAll().Where(x => x.IsPlayer()).FirstOrDefault().IsFirstOwner = true;
	}
}