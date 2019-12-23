using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class UpgradeBucket : Singleton<UpgradeBucket>
{
    public List<UpgradeProperty> upgrades = new List<UpgradeProperty>();
    public static UpgradeProperty GetCurrentUpgrade(SheepType sheepType){
        return Instance.upgrades
            .Where(x=> x.sheepTypeOutput == sheepType).FirstOrDefault();
    }
    public static UpgradeProperty GetNextUpgradeA(SheepType sheepType){
        return Instance.upgrades.Where(x=>x.slot == UpgradeType.A)
            .Where(x=> x.sheepTypeInput == sheepType).FirstOrDefault();
    }
    public static UpgradeProperty GetNextUpgradeB(SheepType sheepType){
        return Instance.upgrades.Where(x=>x.slot == UpgradeType.B)
            .Where(x=> x.sheepTypeInput == sheepType).FirstOrDefault();
    }
}
public enum UpgradeType{
    A,B
}
[System.Serializable]
public class UpgradeProperty{
    public UpgradeType slot;
    public string upgradeDisplayName;
    [SerializeField]
    [StringInList(typeof(PropertyDrawersHelper), "AllPriceNames")]
    public string upgradeCodeName;
    public string upgradeDescription;
    public SheepType sheepTypeInput;
    public SheepType sheepTypeOutput;
    public UpgradeProperty(){
        UpgradeProperty upgrade = new UpgradeProperty();
        upgradeDisplayName = "No Sheep...";
        upgradeDescription = "Need a sheep ready to be launched to upgrade";
    }
}