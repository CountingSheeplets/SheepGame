using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System.Linq;

public class ReadyNetworkManager : MonoBehaviour
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
                bool ready = (bool)(message["data"]["pressed"]);
                Owner readyOwner = OwnersManager.GetOwner(from);
                if (readyOwner == null)
                    return;
                else
                    readyOwner.ready = ready;
                TryStart();
                Debug.Log("Ready:" + readyOwner);
            }
    }
    void TryStart()
    {
        foreach (Owner owner in OwnersManager.GetOwners())
        {
            if (owner.ready == false)
            {
                return;
            }
        }
        EventManager.TriggerEvent(EventName.Input.StartGame(), GameMessage.Write());
        var data = new Dictionary<string, string> { { "show_view_id", "view-0" } };
        AirConsole.instance.Broadcast(data);
    }
    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnReady;
        }
    }
}
