using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeNetworkWriter : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
        EventCoordinator.StartListening(EventName.System.Sheep.Launch(), OnLaunch);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
        EventCoordinator.StopListening(EventName.System.Sheep.Launch(), OnLaunch);
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
