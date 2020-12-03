using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GoldRewardCoordinator : Singleton<GoldRewardCoordinator> {
    Dictionary<Owner, int> combos = new Dictionary<Owner, int>();
    public static int GetComboLevel(Owner owner) {
        if (Instance.combos.ContainsKey(owner))
            return Mathf.Clamp(Instance.combos[owner], 0, ConstantsBucket.MaxPlayerCombo);
        else
            return 0;
    }

    public static void IncreaseCombo(Owner owner) {
        if (Instance.combos.ContainsKey(owner))
            Instance.combos[owner]++;
        else {
            Instance.combos[owner] = 1;
        }
    }
    public static void ResetCombo(Owner owner) {
        Instance.combos[owner] = 0;
    }
    public static void RewardOnFieldLand(Owner owner, Transform tr) {
        RewardGold(owner, GoldRewardBucket.SheepLandFieldFlat, tr);
    }
    public static int GetComboMultiplier(Owner owner) {
        return (int)(Mathf.Pow(2, GetComboLevel(owner)));
    }
    public static void RewardOnKingKingHit(Owner owner, Transform tr) {
        int comboMult = GetComboMultiplier(owner);
        RewardGold(owner, GoldRewardBucket.SheepKingHitFlat * comboMult, tr);
        IncreaseCombo(owner);
    }
    public static void RewardOnSelfThrow(Owner owner, Transform tr) {
        RewardGold(owner, GoldRewardBucket.SheepSelfThrow, tr);
    }
    public static void RewardOnOtherThrow(Owner owner, Transform tr) {
        RewardGold(owner, GoldRewardBucket.SheepOtherPlayerThrow, tr);
    }
    public static void RewardOnSheepElimination(Owner owner, Transform tr) {
        RewardGold(owner, GoldRewardBucket.SheepEliminated, tr);
    }
    public static void RewardGold(PlayerProfile profile, float amount) {
        RewardGold(profile, amount, null);
    }
    public static void RewardGold(PlayerProfile profile, float amount, Transform source) {
        float current = profile.AddMoney(amount); // * NoGrassMultipler(profile.owner));
        EventCoordinator.TriggerEvent(EventName.System.Economy.GoldChanged(), GameMessage.Write().WithDeltaFloat(amount).WithTargetFloat(current).WithOwner(profile.owner).WithTransform(source));
    }
    public static void RewardGold(Owner owner, float amount) {
        RewardGold(owner.GetPlayerProfile(), amount); // * NoGrassMultipler(owner));
    }
    public static void RewardGold(Owner owner, float amount, Transform tr) {
        RewardGold(owner.GetPlayerProfile(), amount, tr); // * NoGrassMultipler(owner));
    }
    /*     static float NoGrassMultipler(Owner owner) {
            if (owner.GetPlayerProfile().GetGrass() > 0) {
                return 1f;
            } else {
                return GoldRewardBucket.IncomeMultiplierNoGrass;
            }
        } */
}