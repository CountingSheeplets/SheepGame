﻿using System.Collections;
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
        EventCoordinator.StartListening(EventName.Input.SheepUpgrade(), OnUpgrade);
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
    }
    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.Input.SheepUpgrade(), OnUpgrade);
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
    }
    void OnUpgrade(GameMessage msg)
    {
        SheepUnit sheep = sheepThrow.sheepReadyToBeThrown;
        if(sheep != null){
            UpgradeProperty upgrade = new UpgradeProperty();
            switch(msg.upgradeType){
                case UpgradeType.A:
                    upgrade = UpgradeBucket.GetNextUpgradeA(sheep);
                    break;
                case UpgradeType.B:
                    upgrade = UpgradeBucket.GetNextUpgradeB(sheep);
                    break;
            }
            if (upgrade == null) return;
            if(msg.owner.GetPlayerProfile().Buy(upgrade.upgradeCodeName)){
                Debug.Log("upgrading to:"+upgrade.sheepTypeOutput.ToString());
                sheep.sheepType = upgrade.sheepTypeOutput;
            }
        }
    }
    void OnReadyToLaunch(GameMessage msg){
        typeA = UpgradeBucket.GetNextUpgradeA(msg.sheepUnit).sheepTypeOutput;
        typeB = UpgradeBucket.GetNextUpgradeB(msg.sheepUnit).sheepTypeOutput;
    }
}
