using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;
public class KingSmashNetworkHandler : MonoBehaviour {
    void Awake() {
        if (AirConsole.instance != null)
            AirConsole.instance.onMessage += OnButtonClick;
    }
    void OnButtonClick(int from, JToken message) {
        if (message["element"].ToString().Contains("smash")) {
            Owner triggerOwner = OwnersCoordinator.GetOwner(from);
            if (triggerOwner == null)
                return;
            if (triggerOwner.GetPlayerProfile() == null)
                return;
            if (!triggerOwner.GetPlayerProfile().isAlive)
                return;
            EventCoordinator.TriggerEvent(EventName.Input.KingAbilities.Smash(), GameMessage.Write().WithOwner(triggerOwner));
        }
    }

    private void OnDestroy() {
        if (AirConsole.instance != null) {
            AirConsole.instance.onMessage -= OnButtonClick;
        }
    }
}