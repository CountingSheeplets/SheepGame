using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class BuyResourceNetworkHandler : MonoBehaviour
{
    void Awake()
    {
        AirConsole.instance.onMessage += OnButtonClick;
    }
    void OnButtonClick(int from, JToken message)
    {
        if (message["data"] != null){
            Owner triggerOwner = OwnersCoordinator.GetOwner(from);
            if(triggerOwner == null)
                return;
            if   (message["element"].ToString().Contains("buy-sheep"))
            {
                EventCoordinator.TriggerEvent(EventName.Input.KingAbilities.SpawnSheep(), GameMessage.Write().WithOwner(triggerOwner));
            }
            if   (message["element"].ToString().Contains("buy-grass"))
            {
                EventCoordinator.TriggerEvent(EventName.Input.KingAbilities.BuyGrass(), GameMessage.Write().WithOwner(triggerOwner));
            }
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
