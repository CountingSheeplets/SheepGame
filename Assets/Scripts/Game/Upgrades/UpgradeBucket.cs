using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class UpgradeBucket : Singleton<UpgradeBucket> {
    public List<UpgradeProperty> upgrades = new List<UpgradeProperty>();
    public static List<UpgradeProperty> GetUpgrades() {
        return Instance.upgrades;
    }
    public static UpgradeProperty GetCurrentUpgrade(SheepUnit sheep) {
        if (sheep == null)return null;
        UpgradeProperty upgrade = Instance.upgrades
            .Where(x => x.sheepTypeOutput == sheep.sheepType).FirstOrDefault();
        if (upgrade == null)return null;
        upgrade.priceUpgrade = PriceCoordinator.GetPrice(sheep.owner, upgrade.upgradeCodeName);
        return upgrade;
    }
    public static UpgradeProperty GetUpgradeByType(Owner owner, SheepType sheepType) {
        if (sheepType == SheepType.Base)return null;
        UpgradeProperty upgrade = Instance.upgrades
            .Where(x => x.sheepTypeOutput == sheepType).FirstOrDefault();
        if (upgrade == null)return null; // Instance.upgrades[Instance.upgrades.Count - 1]; //select last (empty) upgrade
        upgrade.priceUpgrade = PriceCoordinator.GetPrice(owner, upgrade.upgradeCodeName);
        return upgrade;
    }
    public static UpgradeProperty GetNextUpgradeA(SheepUnit sheep) {
        if (sheep == null)return null;
        UpgradeProperty upgrade = Instance.upgrades.Where(x => x.slot == UpgradeType.A)
            .Where(x => x.sheepTypeInput == sheep.sheepType).FirstOrDefault();
        if (upgrade == null)return null; // Instance.upgrades[Instance.upgrades.Count - 1]; //select last (empty) upgrade
        upgrade.priceUpgrade = PriceCoordinator.GetPrice(sheep.owner, upgrade.upgradeCodeName);
        return upgrade;
    }
    public static UpgradeProperty GetNextUpgradeB(SheepUnit sheep) {
        if (sheep == null)return null;
        UpgradeProperty upgrade = Instance.upgrades.Where(x => x.slot == UpgradeType.B)
            .Where(x => x.sheepTypeInput == sheep.sheepType).FirstOrDefault();
        if (upgrade == null)return null;
        upgrade.priceUpgrade = PriceCoordinator.GetPrice(sheep.owner, upgrade.upgradeCodeName);
        return upgrade;
    }
    public static string ToName(SheepType sheepType) {
        switch (sheepType) {
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
    public static string GetAttachmentSlot(SheepType sheepType) {
        switch (sheepType) {
            case SheepType.None:
                return " . ";
            case SheepType.Small:
                return "Small.Small";
            case SheepType.Bouncy:
                return "Divine.Divine";
            case SheepType.Greedy:
                return "Crown.Crown";
            case SheepType.Armored:
                return "Crown.Helmet";
            case SheepType.Trench:
                return "Shovel.Shovel";
            case SheepType.Tank:
                return "Shield.Shield";
            case SheepType.Base:
                return " . ";
        }
        return "";
    }
}
public enum UpgradeType {
    A,
    B
}

[System.Serializable]
public class UpgradeProperty {
    public float priceUpgrade = 0;
    public UpgradeType slot;
    public string upgradeDisplayName;
    [SerializeField]
#if UNITY_EDITOR
    [StringInList(typeof(PropertyDrawersHelper), "AllPriceNames")]
#endif
    public string upgradeCodeName;
    public string upgradeDescription;
    public SheepType sheepTypeInput;
    public SheepType sheepTypeOutput;
    public string enumStringName {
        get { return sheepTypeOutput.ToString(); }
    }
    public UpgradeProperty() {
        upgradeDisplayName = "No Sheep...";
        upgradeDescription = "Need a sheep ready to be launched to upgrade";
    }

    public override string ToString() {
        return "UpgradeProperty(): \n" +
            "upgradeCodeName=" + upgradeCodeName + " \n" +
            "upgradeDisplayName=" + upgradeDisplayName + " \n" +
            "enumStringName=" + enumStringName + " \n" +
            "in->out=" + sheepTypeInput + " -> " + sheepTypeOutput + " \n";
    }
}