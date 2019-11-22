﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using NDream.AirConsole;
using System.Linq;
public class KingButtonsNetworkHandler : MonoBehaviour
{
    void Awake()
    {
        AirConsole.instance.onMessage += OnButtonClick;
    }
    void OnButtonClick(int from, JToken message)
    {
        if (message["data"] != null)
            if   (message["element"].ToString().Contains("king-upgrade"))
            {
                if (message["data"]["upgrade"] != null)
                {
                    Owner triggerOwner = OwnersCoordinator.GetOwner(from);
                    if(triggerOwner == null)
                        return;
                    int upgrade = 0;
                    int.TryParse((string)message["data"]["upgrade"], out upgrade);
                    switch(upgrade){
                        case 1:
                            EventCoordinator.TriggerEvent(EventName.Input.KingAbilities.SpawnSheep(), GameMessage.Write().WithOwner(triggerOwner));
                            break;
                        case 2:
                            EventCoordinator.TriggerEvent(EventName.Input.KingAbilities.BuyGrass(), GameMessage.Write().WithOwner(triggerOwner));
                        break;
                        case 3:
                        break;
                        case 4:
                        break;
                        case 5:
                        break;
                        case 6:
                        break;
                    }
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