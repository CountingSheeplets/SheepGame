﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using NDream.AirConsole;
public class UpgradeInfoNetworkHandler : MonoBehaviour
{
    UpgradeType upgradeType;
    void Awake()
    {
        if(AirConsole.instance != null)
            AirConsole.instance.onMessage += OnOpenUpgradeInfo;
    }

    void OnOpenUpgradeInfo(int from, JToken message)
    {
        if (message["element"] != null && message["pressed"] != null)
        {
            Owner triggerOwner = OwnersCoordinator.GetOwner(from);
            if(triggerOwner == null || !triggerOwner.GetPlayerProfile().isAlive)
                return;

            if((bool)message["pressed"] == false && message["element"].ToString().Contains("upgrade")){
                SheepUnit sheep = triggerOwner.GetPlayfield().GetComponent<SheepThrow>().sheepReadyToBeThrown;
                if(sheep){
                    if (message["element"].ToString() == "upgrade1")
                    {
                        upgradeType = UpgradeType.A;
                        NetworkCoordinator.SendUpgradeData(from, UpgradeBucket.GetNextUpgradeA(sheep));
                    }
                    if (message["element"].ToString() == "upgrade2")
                    {
                        upgradeType = UpgradeType.B;
                        NetworkCoordinator.SendUpgradeData(from, UpgradeBucket.GetNextUpgradeB(sheep));
                    }
                    //send to show view for info:
                    NetworkCoordinator.SendShowView(from, "upgrade");
                }
            } else {
                if(message["element"].ToString() == "upgrade" && (bool)message["pressed"] == true){
                    EventCoordinator.TriggerEvent(EventName.Input.SheepUpgrade(), GameMessage.Write().WithOwner(triggerOwner).WithUpgradeType(upgradeType));
                    NetworkCoordinator.SendShowView(from, "match");
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnOpenUpgradeInfo;
        }
    }
}
