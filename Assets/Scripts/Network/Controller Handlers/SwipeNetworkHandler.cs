using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using NDream.AirConsole;

public class SwipeNetworkHandler : Singleton<SwipeNetworkHandler>
{
    void Awake()
    {
        if(AirConsole.instance != null)
            AirConsole.instance.onMessage += OnSwipe;
    }
    void OnSwipe(int from, JToken message)
    {
        if (message["element"].ToString() == "swipe"){
            Owner triggerOwner = OwnersCoordinator.GetOwner(from);
            if(triggerOwner == null || !triggerOwner.GetPlayerProfile().isAlive)
                return;
            Debug.Log("from: " + from + "   msg:" + message);
            Swipe newSwipe = new Swipe(message);
            Debug.Log(newSwipe.ToString());
            EventCoordinator.TriggerEvent(EventName.Input.Swipe(), GameMessage.Write().WithSwipe(newSwipe).WithOwner(triggerOwner));
        }
    }
    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnSwipe;
        }
    }
}
