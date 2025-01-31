using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;
public class ReadyNetworkHandler : MonoBehaviour {
    void Start() {
        if (AirConsole.instance != null)
            AirConsole.instance.onMessage += OnReady;
        EventCoordinator.StartListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
    }
    void Awake() {

    }
    void OnPlayerLeft(GameMessage msg) {
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            owner.ready = false;
        }
    }
    void OnReady(int from, JToken message) {
        if (GameStateView.HasState(GameState.started))
            return;
        if (message["element"] != null)
            if (message["element"].ToString() == "ready-button") {
                bool ready = (bool)(message["pressed"]);
                Owner readyOwner = OwnersCoordinator.GetOwner(from);
                if (TryStart(readyOwner))
                    return;
                if (readyOwner == null)
                    return;
                else {
                    readyOwner.ready = true;
                }
            }
    }

    bool TryStart(Owner readyingOwner) {
        int count = OwnersCoordinator.GetOwners().Where(x => x.connected).ToList().Count;
        if (count < 2)
            if (GameStateView.HasState(GameState.ended)) {
                return true;
            } else return false;
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            if (owner.EqualsByValue(readyingOwner))
                continue;
            if (owner.ready == false) {
                return false;
            }
        }
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            owner.ready = false;
        }
        //EventCoordinator.TriggerEvent(EventName.Input.PlayersReady(), GameMessage.Write());
        EventCoordinator.TriggerEvent(EventName.Input.StartGame(), GameMessage.Write());
        NetworkCoordinator.SendShowViewAll("match");
        return true;
    }

    private void OnDestroy() {
        if (AirConsole.instance != null) {
            AirConsole.instance.onMessage -= OnReady;
        }
        EventCoordinator.StopListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
    }
}