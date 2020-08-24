using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class KingItemBucket : Singleton<KingItemBucket> {
    [SerializeField]
    List<KingItem> kingHats = new List<KingItem>();
    [SerializeField]
    List<KingItem> kingScepters = new List<KingItem>();
    void Start() {
        kingHats = CSVReader.GetHats();
        kingScepters = CSVReader.GetScepters();
        kingHats = kingHats.OrderBy(x => x.crownRequirement).ToList();
        kingScepters = kingScepters.OrderBy(x => x.crownRequirement).ToList();
    }
    public static int ItemCount(KingItemType itemType) {
        if (itemType == KingItemType.hat) {
            return Instance.kingHats.Count;
        }
        if (itemType == KingItemType.scepter) {
            return Instance.kingScepters.Count;
        }
        return 0;
    }

    public static KingItem GetItem(int index, KingItemType itemType) {
        if (itemType == KingItemType.hat) {
            if (Instance.kingHats.Count > index && index >= 0)
                return Instance.kingHats[index];
        }
        if (itemType == KingItemType.scepter) {
            if (Instance.kingScepters.Count > index && index >= 0)
                return Instance.kingScepters[index];
        }
        return null;
    }
    public static bool IsCrownRequirementMet(Owner owner, KingItem item) {
        if (owner.GetPlayerProfile().permanentCrownCount > item.crownRequirement) {
            return true;
        }
        return false;
    }
    public static bool IsCrownRequirementMet(Owner owner, int index, KingItemType itemType) {
        KingItem item = GetItem(index, itemType);
        if (item == null)
            return false;
        return IsCrownRequirementMet(owner, item);
    }
    public static bool IsPremiumRequirementMet(Owner owner, KingItem item) {
        if (owner.GetPlayerProfile().isPremium)
            return true;
        if (!item.premiumRequirement)
            return true;
        return false;
    }
    public static bool IsPremiumRequirementMet(Owner owner, int index, KingItemType itemType) {
        KingItem item = GetItem(index, itemType);
        if (item == null)
            return false;
        return IsPremiumRequirementMet(owner, item);
    }
    public static bool IsItemAvailable(Owner owner, int index, KingItemType itemType) {
        KingItem item = GetItem(index, itemType);
        return IsItemAvailable(owner, item);
    }
    public static bool IsItemAvailable(Owner owner, KingItem item) {
        if (IsPremiumRequirementMet(owner, item) && IsCrownRequirementMet(owner, item))
            return true;
        return false;
    }
}