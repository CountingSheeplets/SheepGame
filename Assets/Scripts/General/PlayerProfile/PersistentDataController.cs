using System.Collections;
using System.Collections.Generic;
//using NDream.AirConsole;
//using Newtonsoft.Json.Linq;
using UnityEngine;

/*public class PersistentDataController : MonoBehaviour {
	void Awake() {
		AirConsole.instance.onPersistentDataStored += OnDataStored;
		AirConsole.instance.onPersistentDataLoaded += OnDataLoaded;
		EventCoordinator.StartListening(EventName.System.Player.ProfileCreated(), OnProfileCreated);
		EventCoordinator.StartListening(EventName.UI.ShowScoreScreen(), OnShowScore);
		EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
	}
	void OnProfileCreated(GameMessage msg) {
		PersistentDataCoordinator.RequestData(msg.playerProfile.owner);
	}
	void OnShowScore(GameMessage msg) {
		foreach (Owner owner in OwnersCoordinator.GetOwners()) {
			if (owner.ownerName.ToLower().Contains("guest"))
				owner.GetPlayerProfile().tutorialIndex = 1;
			else
				owner.GetPlayerProfile().tutorialIndex = 0;
			PersistentDataCoordinator.StoreData(owner);
		}
	}
	void OnStartGame(GameMessage msg) {
		foreach (Owner owner in OwnersCoordinator.GetOwners()) {
			owner.GetPlayerProfile().SeeItem(KingItemType.hat, owner.GetPlayerProfile().selectedHat);
			owner.GetPlayerProfile().SeeItem(KingItemType.scepter, owner.GetPlayerProfile().selectedScepter);
			PersistentDataCoordinator.StoreData(owner);
		}
	}
	void OnDataLoaded(JToken data) {
		PersistentDataCoordinator.OnReceivedData(data);
	}
	void OnDataStored(string uid) {
		//OwnersCoordinator.GetOwner(uid).GetPlayerProfile().CleanNetworkDirty();
	}
	void OnDestroy() {
		if (AirConsole.instance != null) {
			AirConsole.instance.onPersistentDataStored -= OnDataStored;
			AirConsole.instance.onPersistentDataLoaded -= OnDataLoaded;
		}
		EventCoordinator.StopListening(EventName.System.Player.ProfileCreated(), OnProfileCreated);
		EventCoordinator.StopListening(EventName.UI.ShowScoreScreen(), OnShowScore);
		EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
	}
}*/