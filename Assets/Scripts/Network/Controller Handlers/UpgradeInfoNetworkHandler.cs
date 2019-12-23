using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using NDream.AirConsole;
public class UpgradeInfoNetworkHandler : MonoBehaviour
{
    void Awake()
    {
        AirConsole.instance.onMessage += OnOpenUpgradeInfo;
    }

    void OnOpenUpgradeInfo(int from, JToken message)
    {
        if (message["element"] != null)
        {
            if((bool)message["pressed"] == false){
                SheepUnit sheep = OwnersCoordinator.GetOwner(from).GetPlayfield().GetComponent<SheepThrow>().sheepReadyToBeThrown;
                if(sheep){
                    if (message["element"].ToString() == "upgrade1")
                    {
                        NetworkCoordinator.SendUpgradeData(from, UpgradeBucket.GetNextUpgradeA(sheep.sheepType));
                    }
                    if (message["element"].ToString() == "upgrade2")
                    {
                        NetworkCoordinator.SendUpgradeData(from, UpgradeBucket.GetNextUpgradeB(sheep.sheepType));
                    }
                } else 
                    NetworkCoordinator.SendUpgradeData(from, new UpgradeProperty());
                //send to show view for info:
                NetworkCoordinator.SendShowView(from, "upgrade");
            } else {
                Owner owner = OwnersCoordinator.GetOwner(from);
                if(owner != null){
                    if (message["element"].ToString() == "upgrade1")
                    {
                        EventCoordinator.TriggerEvent(EventName.Input.SheepUpgrades.A(), GameMessage.Write().WithOwner(owner));
                    }
                    if (message["element"].ToString() == "upgrade2")
                    {
                        EventCoordinator.TriggerEvent(EventName.Input.SheepUpgrades.B(), GameMessage.Write().WithOwner(owner));
                    }
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
