using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepUpgrade : MonoBehaviour
{
    public SheepThrow sheepThrow;
    public SheepType typeA;
    public SheepType typeB;
    void Start()
    {
        if(sheepThrow == null) sheepThrow = GetComponent<SheepThrow>();
        EventCoordinator.StartListening(EventName.Input.SheepUpgrades.A(), OnUpgradeA);
        EventCoordinator.StartListening(EventName.Input.SheepUpgrades.B(), OnUpgradeB);
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
    }
    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.Input.SheepUpgrades.A(), OnUpgradeA);
        EventCoordinator.StopListening(EventName.Input.SheepUpgrades.B(), OnUpgradeB);
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
    }
    void OnUpgradeA(GameMessage msg)
    {
        SheepUnit sheep = sheepThrow.sheepReadyToBeThrown;
        if(sheep != null){
            UpgradeProperty upgrade = UpgradeBucket.GetNextUpgradeA(sheep);
            if(msg.owner.GetPlayerProfile().Buy(upgrade.upgradeCodeName)){
                sheep.sheepType = upgrade.sheepTypeOutput;
            }
        }
    }
    void OnUpgradeB(GameMessage msg)
    {
        SheepUnit sheep = sheepThrow.sheepReadyToBeThrown;
        if(sheep != null){
            UpgradeProperty upgrade = UpgradeBucket.GetNextUpgradeB(sheep);
            if(msg.owner.GetPlayerProfile().Buy(upgrade.upgradeCodeName)){
                sheep.sheepType = upgrade.sheepTypeOutput;
            }
        }
    }
    void OnReadyToLaunch(GameMessage msg){
        typeA = UpgradeBucket.GetNextUpgradeA(msg.sheepUnit).sheepTypeOutput;
        typeB = UpgradeBucket.GetNextUpgradeB(msg.sheepUnit).sheepTypeOutput;
    }
}
