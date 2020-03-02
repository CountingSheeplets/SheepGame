using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class KingItemBucket : Singleton<KingItemBucket>
{
    [SerializeField]
    List<KingItem> kingHats = new List<KingItem>();
    [SerializeField]
    List<KingItem> kingScepters = new List<KingItem>();
    void Start(){
        kingHats.OrderBy(x => x.crownRequirement);
        kingScepters.OrderBy(x => x.crownRequirement);
    }
    public static int ItemCount(KingItemType itemType){
        if(itemType == KingItemType.hat){
            return Instance.kingHats.Count;
        }
        if(itemType == KingItemType.scepter){
            return Instance.kingScepters.Count;
        }
        return 0;
    }

    public static KingItem GetItem(int index, KingItemType itemType){
        if(itemType == KingItemType.hat){
            if(Instance.kingHats.Count > index && index >= 0)
                return Instance.kingHats[index];
        }
        if(itemType == KingItemType.scepter){
            if(Instance.kingScepters.Count > index && index >= 0)
                return Instance.kingScepters[index];
        }
        return null;
    }
    public static bool IsCrownRequirementMet(Owner owner, int index, KingItemType itemType){
        KingItem item = GetItem(index, itemType);
        if(item == null)
            return false;
        //Debug.Log(owner.GetPlayerProfile().permanentCrownCount+" count vs req "+item.crownRequirement);
        if(owner.GetPlayerProfile().permanentCrownCount > item.crownRequirement) {
            return true;
        } return false;
    }
    public static bool IsPremiumRequirementMet(Owner owner, int index, KingItemType itemType){
        KingItem item = GetItem(index, itemType);
        if(item == null)
            return false;
        if(!item.premiumRequirement)
            return true;
        else if(owner.GetPlayerProfile().isPremium)
            return true;
        return false;
    }
    public static bool IsItemAvailable(Owner owner, int index, KingItemType itemType){
        if(IsPremiumRequirementMet(owner, index, itemType) && IsCrownRequirementMet(owner, index, itemType))
            return true;
        return false;
    }
}
