using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using NDream.AirConsole;

public class SwipeNetworkHandler : Singleton<SwipeNetworkHandler>
{
    void Awake()
    {
        AirConsole.instance.onMessage += OnSwipe;
    }
    void OnSwipe(int from, JToken message)
    {
        if (message["data"] != null)
            if (message["data"]["direction"] != null)
            {
                //Debug.Log("int from: " + from + "   " + message);
                if (message["element"].ToString() == "swipe-field")
                {
                    Swipe newSwipe = new Swipe(message["data"]["direction"]);
                    EventCoordinator.TriggerEvent(EventName.Input.Swipe(), GameMessage.Write().WithSwipe(newSwipe).WithOwner(OwnersCoordinator.GetOwner(from)));
                }
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
