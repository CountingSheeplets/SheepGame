using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SwipeNetworkHandler : Singleton<SwipeNetworkHandler> {
    void Awake() {
        if (AirConsole.instance != null)
            AirConsole.instance.onMessage += OnSwipe;
    }
    void OnSwipe(int from, JToken message) {
        if (message["element"].ToString() == "swipe") {
            Owner triggerOwner = OwnersCoordinator.GetOwner(from);
            if (triggerOwner == null)
                return;
            if (triggerOwner.GetPlayerProfile() == null)
                return;
            if (!triggerOwner.GetPlayerProfile().isAlive)
                return;
            Debug.Log("from: " + from + "   msg:" + message);
            Swipe newSwipe = new Swipe(message);
            Debug.Log(newSwipe.ToString());
            EventCoordinator.TriggerEvent(EventName.Input.Swipe(), GameMessage.Write().WithSwipe(newSwipe).WithOwner(triggerOwner));
        }
    }
    private void OnDestroy() {
        if (AirConsole.instance != null) {
            AirConsole.instance.onMessage -= OnSwipe;
        }
    }
}