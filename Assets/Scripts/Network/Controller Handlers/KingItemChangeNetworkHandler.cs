using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class KingItemChangeNetworkHandler : MonoBehaviour {
    void Awake() {
        if (AirConsole.instance != null)
            AirConsole.instance.onMessage += OnButtonClick;
    }
    void OnButtonClick(int from, JToken message) {
        if (GameStateView.HasState(GameState.started))
            return;
        Owner triggerOwner = OwnersCoordinator.GetOwner(from);
        if (triggerOwner == null)
            return;
        if (triggerOwner.GetPlayerProfile() == null)
            return;
        if (!triggerOwner.GetPlayerProfile().isAlive)
            return;
        if (message["element"].ToString().Contains("changeHat")) {
            EventCoordinator.TriggerEvent(EventName.Input.ChangeKingItem(),
                GameMessage.Write()
                .WithOwner(triggerOwner)
                .WithIntMessage((int)message["value"])
                .WithKingItemType(KingItemType.hat));
        }
        if (message["element"].ToString().Contains("changeScepter")) {
            EventCoordinator.TriggerEvent(EventName.Input.ChangeKingItem(),
                GameMessage.Write()
                .WithOwner(triggerOwner)
                .WithIntMessage((int)message["value"])
                .WithKingItemType(KingItemType.scepter));
        }
    }
    private void OnDestroy() {
        if (AirConsole.instance != null) {
            AirConsole.instance.onMessage -= OnButtonClick;
        }
    }
}