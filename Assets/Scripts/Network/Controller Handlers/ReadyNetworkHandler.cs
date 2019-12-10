using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System.Linq;

public class ReadyNetworkHandler : MonoBehaviour
{
    void Awake()
    {
        AirConsole.instance.onMessage += OnReady;
    }

    void OnReady(int from, JToken message)
    {
        if (message["element"] != null)
            if (message["element"].ToString() == "ready-button")
            {
                bool ready = (bool)(message["pressed"]);
                Owner readyOwner = OwnersCoordinator.GetOwner(from);
                if (readyOwner == null)
                    return;
                else
                    readyOwner.ready = ready;
                NetworkCoordinator.SendConfirmReady(from);
                TryStart();
                Debug.Log("Ready:" + readyOwner);
            }
    }
    void TryStart()
    {
        foreach (Owner owner in OwnersCoordinator.GetOwners())
        {
            if (owner.ready == false)
            {
                return;
            }
        }
        EventCoordinator.TriggerEvent(EventName.Input.StartGame(), GameMessage.Write());
        NetworkCoordinator.SendShowViewAll("match");
    }
    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnReady;
        }
    }
}
