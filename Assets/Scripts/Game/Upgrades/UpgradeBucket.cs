using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class UpgradeBucket : Singleton<UpgradeBucket>
{
    public List<UpgradeProperty> upgrades = new List<UpgradeProperty>();
    public static UpgradeProperty GetCurrentUpgrade(SheepUnit sheep){
         UpgradeProperty upgrade = Instance.upgrades
            .Where(x=> x.sheepTypeOutput == sheep.sheepType).FirstOrDefault();
        if (upgrade == null) return null;
        upgrade.priceUpgrade = PriceCoordinator.GetPrice(sheep.owner, upgrade.upgradeCodeName);
        return upgrade;
    }
    public static UpgradeProperty GetNextUpgradeA(SheepUnit sheep){
        UpgradeProperty upgrade = Instance.upgrades.Where(x=>x.slot == UpgradeType.A)
            .Where(x=> x.sheepTypeInput == sheep.sheepType).FirstOrDefault();
        if (upgrade == null) return null;
        upgrade.priceUpgrade = PriceCoordinator.GetPrice(sheep.owner, upgrade.upgradeCodeName);
        return upgrade;
    }
    public static UpgradeProperty GetNextUpgradeB(SheepUnit sheep){
        UpgradeProperty upgrade = Instance.upgrades.Where(x=>x.slot == UpgradeType.B)
            .Where(x=> x.sheepTypeInput == sheep.sheepType).FirstOrDefault();
        if (upgrade == null) return null;
        upgrade.priceUpgrade = PriceCoordinator.GetPrice(sheep.owner, upgrade.upgradeCodeName);
        return upgrade;
    }
    public static string ToName(SheepType sheepType){
        switch(sheepType){
            case SheepType.None:
                return PriceName.King.BuySheep();
            case SheepType.Small:
                return PriceName.SheepUpgrade.Small();
            case SheepType.Bouncy:
                return PriceName.SheepUpgrade.Bouncy();
            case SheepType.Greedy:
                return PriceName.SheepUpgrade.Greedy();
            case SheepType.Armored:
                return PriceName.SheepUpgrade.Armored();
            case SheepType.Trench:
                return PriceName.SheepUpgrade.Trench();
            case SheepType.Tank:
                return PriceName.SheepUpgrade.Tank();
        }
        return "";
    }
}
public enum UpgradeType{
    A,B
}
[System.Serializable]
public class UpgradeProperty{
    public float priceUpgrade = 0;
    public UpgradeType slot;
    public string upgradeDisplayName;
    [SerializeField]
    [StringInList(typeof(PropertyDrawersHelper), "AllPriceNames")]
    public string upgradeCodeName;
    public string upgradeDescription;
    public SheepType sheepTypeInput;
    public SheepType sheepTypeOutput;
    public UpgradeProperty(){
        upgradeDisplayName = "No Sheep...";
        upgradeDescription = "Need a sheep ready to be launched to upgrade";
    }
}