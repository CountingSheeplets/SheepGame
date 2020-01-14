using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class KingItemChangeNetworkHandler : MonoBehaviour
{
    void Awake()
    {
        if(AirConsole.instance != null)
            AirConsole.instance.onMessage += OnButtonClick;
    }
    void OnButtonClick(int from, JToken message)
    {
        Owner triggerOwner = OwnersCoordinator.GetOwner(from);
        if(triggerOwner == null || !triggerOwner.GetPlayerProfile().isAlive)
            return;
        if   (message["element"].ToString().Contains("changeHat"))
        {
            EventCoordinator.TriggerEvent(EventName.Input.ChangeHat(), GameMessage.Write().WithOwner(triggerOwner).WithIntMessage((int)message["value"]));
        }
        if   (message["element"].ToString().Contains("changeScepter"))
        {
            EventCoordinator.TriggerEvent(EventName.Input.ChangeScepter(), GameMessage.Write().WithOwner(triggerOwner).WithIntMessage((int)message["value"]));
        }
    }
    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnButtonClick;
        }
    }
}
