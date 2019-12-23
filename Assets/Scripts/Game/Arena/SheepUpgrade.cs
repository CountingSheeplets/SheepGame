using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepUpgrade : MonoBehaviour
{
    public SheepThrow sheepThrow;
    void Start()
    {
        if(sheepThrow == null) sheepThrow = GetComponent<SheepThrow>();
        EventCoordinator.StartListening(EventName.Input.SheepUpgrades.A(), OnUpgradeA);
        EventCoordinator.StartListening(EventName.Input.SheepUpgrades.B(), OnUpgradeB);
    }
    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.Input.SheepUpgrades.A(), OnUpgradeA);
        EventCoordinator.StopListening(EventName.Input.SheepUpgrades.B(), OnUpgradeB);
    }
    void OnUpgradeA(GameMessage msg)
    {
        SheepUnit sheep = sheepThrow.sheepReadyToBeThrown;
        if(sheep != null){
            UpgradeProperty upgrade = UpgradeBucket.GetNextUpgradeA(sheep.sheepType);
            if(msg.owner.GetPlayerProfile().Buy(upgrade.upgradeCodeName)){
                sheep.sheepType = upgrade.sheepTypeOutput;
            }
        }
    }
    void OnUpgradeB(GameMessage msg)
    {
        SheepUnit sheep = sheepThrow.sheepReadyToBeThrown;
        if(sheep != null){
            UpgradeProperty upgrade = UpgradeBucket.GetNextUpgradeB(sheep.sheepType);
            if(msg.owner.GetPlayerProfile().Buy(upgrade.upgradeCodeName)){
                sheep.sheepType = upgrade.sheepTypeOutput;
            }
        }
    }
}
