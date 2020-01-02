using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeNetworkWriter : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
        EventCoordinator.StartListening(EventName.System.Sheep.Launch(), OnLaunch);
        EventCoordinator.StartListening(EventName.System.Sheep.Upgraded(), OnUpgraded);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
        EventCoordinator.StopListening(EventName.System.Sheep.Launch(), OnLaunch);
        EventCoordinator.StopListening(EventName.System.Sheep.Upgraded(), OnUpgraded);
    }
    void OnUpgraded(GameMessage msg){
        NetworkCoordinator.SendUpgradeButtons(msg.sheepUnit.owner.deviceId, msg.sheepUnit);
    }
    void OnReadyToLaunch(GameMessage msg)
    {
        if(msg.sheepUnit.owner == msg.playfield.owner)
            NetworkCoordinator.SendUpgradeButtons(msg.sheepUnit.owner.deviceId, msg.sheepUnit);
    }
    void OnLaunch(GameMessage msg)
    {
        NetworkCoordinator.SendUpgradeButtons(msg.sheepUnit.owner.deviceId, null);
    }
}
