using System.Collections;
using System.Collections.Generic;
/*using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;
public class PlayAgainNetworkHandler : MonoBehaviour {
    void Awake() {
        if (AirConsole.instance != null)
            AirConsole.instance.onMessage += OnReady;
    }

    void OnReady(int from, JToken message) {
        if (!GameStateView.HasState(GameState.ended))
            return;
        if (message["element"] != null)
            if (message["element"].ToString() == "playAgain") {
                bool playAgain = (bool)(message["pressed"]);
                Owner readyOwner = OwnersCoordinator.GetOwner(from);
                if (readyOwner == null)
                    return;
                else {
                    readyOwner.playAgain = playAgain;
                }
                if (TryRestart(GameMessage.Write())) {
                    AirConsole.instance.ShowAd();
                    EventCoordinator.TriggerEvent(EventName.System.Environment.ScrollScoresOut(), GameMessage.Write());
                    NetworkCoordinator.SendShowViewAll("loading");
                };
            }
    }
    bool TryRestart(GameMessage msg) {
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            if (owner.connected)
                if (owner.playAgain == false) {
                    return false;
                }
        }
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            owner.playAgain = false;
        }
        return true;
    }
    private void OnDestroy() {
        if (AirConsole.instance != null) {
            AirConsole.instance.onMessage -= OnReady;
        }
    }
}*/