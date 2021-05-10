using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSpeedCoordinator : Singleton<KingSpeedCoordinator> {
    public Dictionary<Owner, int> kingUpgradeLevels = new Dictionary<Owner, int>();
    public static int GetLevel(Owner owner) {
        if (Instance.kingUpgradeLevels.ContainsKey(owner)) {
            return Instance.kingUpgradeLevels[owner];
        }
        return 0;
    }
    public static int IncreaseLevel(Owner owner) {
        if (Instance.kingUpgradeLevels.ContainsKey(owner)) {
            Instance.kingUpgradeLevels[owner]++;
            SetAnimationSpeed(owner, GetSpeedScale(owner));
            return Instance.kingUpgradeLevels[owner];
        } else {
            Instance.kingUpgradeLevels.Add(owner, 1);
            SetAnimationSpeed(owner, GetSpeedScale(owner));
            return 1;
        }
    }

    public static float GetMoveSpeed(Owner owner) {
        return SpeedBucket.GetRunSpeed(SheepType.King) * Mathf.Pow(1.1f, GetLevel(owner));
    }
    public static float GetSpeedScale(Owner owner) {
        return Mathf.Pow(1.1f, GetLevel(owner));
    }

    public static void SetAnimationSpeed(Owner owner, float speed) {
        Animator anim = owner.GetKing().GetModel().GetComponent<Animator>();
        anim.SetFloat("kingSpeed", 1f / speed);
    }
}