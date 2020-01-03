using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class BackButtonHandler : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.Network.View(), OnView);
    }

    void OnView(GameMessage msg)
    {
        if (ControllerView.currentViewStates[msg.owner.deviceId] == "back")
        {
            string viewToSet = "";
            string prev = ControllerView.previousViewStates[msg.owner.deviceId];
            if(prev == "upgrade")
                viewToSet = "match";
            NetworkCoordinator.SendShowView(msg.owner.deviceId, viewToSet);
        }
    }

    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.Input.Network.View(), OnView);
    }
}
