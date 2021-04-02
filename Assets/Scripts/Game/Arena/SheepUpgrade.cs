using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepUpgrade : MonoBehaviour {
    public SheepThrow sheepThrow;
    public SheepType typeA;
    public SheepType typeB;
    public int tier2UpgradeCount;
    Owner owner;
    void Start() {
        if (sheepThrow == null)sheepThrow = GetComponent<SheepThrow>();
        if (owner == null)owner = GetComponent<Playfield>().owner;
        EventCoordinator.StartListening(EventName.Input.SheepUpgrade(), OnUpgrade);
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.SheepUpgrade(), OnUpgrade);
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
    }
    void OnUpgrade(GameMessage msg) {
        if (!owner.EqualsByValue(msg.owner))
            return;
        SheepUnit sheep = sheepThrow.sheepReadyToBeThrown;
        if (sheep != null) {
            UpgradeProperty upgrade = UpgradeBucket.GetUpgradeByType(msg.owner, msg.sheepType);
            if (upgrade == null)return;
            if (msg.owner.GetPlayerProfile().Buy(upgrade.upgradeCodeName)) {
                //Debug.Log("upgrading to:" + upgrade.sheepTypeOutput.ToString());
                SheepCoordinator.UpgradeSheep(sheep, upgrade.sheepTypeOutput);
                if (upgrade.sheepTypeOutput != (SheepType.Small) && upgrade.sheepTypeOutput != (SheepType.Armored)) {
                    tier2UpgradeCount++;
                }
                EventCoordinator.TriggerEvent(EventName.System.Sheep.Upgraded(), msg.WithSheepUnit(sheep));
            }
        }
    }
    void OnReadyToLaunch(GameMessage msg) {
        UpgradeProperty upgA = UpgradeBucket.GetNextUpgradeA(msg.sheepUnit);
        if (upgA != null)
            typeA = upgA.sheepTypeOutput;
        else typeA = 0;
        UpgradeProperty upgB = UpgradeBucket.GetNextUpgradeB(msg.sheepUnit);
        if (upgB != null)
            typeB = upgB.sheepTypeOutput;
        else typeB = 0;
    }
}