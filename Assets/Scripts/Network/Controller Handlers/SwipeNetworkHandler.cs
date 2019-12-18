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
        if (message["element"].ToString() == "swipe"){
            var data = message["data"];
            Debug.Log("from: " + from + "   msg:" + message);
            if (data != null){
                Swipe newSwipe = new Swipe(data);
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
