using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeNetworkWriter : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
    }
    void OnReadyToLaunch(GameMessage msg)
    {
        NetworkCoordinator.SendUpgradeButtons(msg.sheepUnit.owner.deviceId, msg.sheepUnit);
    }
}
