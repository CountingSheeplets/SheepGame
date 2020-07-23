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
    public static void RewardOnFieldLand(Owner owner) {
        RewardGold(owner, GoldRewardBucket.SheepLandFieldFlat);
    }
    public static int GetComboMultiplier(Owner owner) {
        return (int) (Mathf.Pow(2, GetComboLevel(owner)));
    }
    public static void RewardOnKingKingHit(Owner owner) {
        int comboMult = GetComboMultiplier(owner);
        RewardGold(owner, GoldRewardBucket.SheepKingHitFlat * comboMult);
        IncreaseCombo(owner);
    }
    public static void RewardOnSelfThrow(Owner owner, bool isGreedy) {
        float bonus = 0;
        if (isGreedy)
            bonus = GoldRewardBucket.GreedySheepBonusMoney;
        RewardGold(owner, GoldRewardBucket.SheepSelfThrow + bonus);
    }
    public static void RewardOnOtherThrow(Owner owner, bool isGreedy) {
        float bonus = 0;
        if (isGreedy)
            bonus = GoldRewardBucket.GreedySheepBonusMoney;
        RewardGold(owner, GoldRewardBucket.SheepOtherPlayerThrow + bonus);
    }

    static void RewardGold(PlayerProfile profile, float amount) {
        profile.AddMoney(amount * NoGrassMultipler(profile.owner));
    }
    static void RewardGold(Owner owner, float amount) {
        RewardGold(owner.GetPlayerProfile(), amount * NoGrassMultipler(owner));
    }
    static float NoGrassMultipler(Owner owner) {
        if (owner.GetPlayerProfile().GetGrass() > 0) {
            return 1f;
        } else {
            return GoldRewardBucket.IncomeMultiplierNoGrass;
        }
    }
}