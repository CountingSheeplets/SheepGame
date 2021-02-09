using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SwipeNetworkHandler : Singleton<SwipeNetworkHandler> {
    void Awake() {
        if (AirConsole.instance != null) {
            AirConsole.instance.onMessage += OnSwipe;
            AirConsole.instance.onMessage += OnTap;
        }
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
            //Debug.Log("from: " + from + "   msg:" + message);
            Swipe newSwipe = new Swipe(message);
            //Debug.Log(newSwipe.ToString());
            EventCoordinator.TriggerEvent(EventName.Input.Swipe(), GameMessage.Write().WithSwipe(newSwipe).WithOwner(triggerOwner));
        }
    }
    void OnTap(int from, JToken message) {
        if (message["element"].ToString() == "tap") {
            //Debug.Log("clicked.ToString():" + message["clicked"].ToString());
            Owner triggerOwner = OwnersCoordinator.GetOwner(from);
            if (triggerOwner == null)
                return;
            if (triggerOwner.GetPlayerProfile() == null)
                return;
            if (!triggerOwner.GetPlayerProfile().isAlive)
                return;
            //Debug.Log("from: " + from + "   msg:" + message);
            Swipe newSwipe = new Swipe(message);
            //Debug.Log(newSwipe.ToString());
            if (message["clicked"].ToString() == "True") {
                EventCoordinator.TriggerEvent(EventName.Input.Tap(), GameMessage.Write().WithSwipe(newSwipe).WithOwner(triggerOwner).WithState(true));
            }
            if (message["clicked"].ToString() == "False") {
                EventCoordinator.TriggerEvent(EventName.Input.Tap(), GameMessage.Write().WithSwipe(newSwipe).WithOwner(triggerOwner).WithState(false));
            }
        }
    }
    private void OnDestroy() {
        if (AirConsole.instance != null) {
            AirConsole.instance.onMessage -= OnSwipe;
            AirConsole.instance.onMessage -= OnTap;
        }
    }
}