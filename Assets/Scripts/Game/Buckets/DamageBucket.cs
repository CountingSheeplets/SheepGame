using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageBucket : Singleton<DamageBucket> {
    public List<DamageProperty> balistaDamages = new List<DamageProperty>();
    public List<DamageProperty> kickDamages = new List<DamageProperty>();
    public List<DamageProperty> eatDamages = new List<DamageProperty>();
    public static float GetBallistaLandDamage(SheepType sheepType) {
        return GetSpeed(DamageType.balistaLand, sheepType);
    }
    public static float GetKickLandDamage(SheepType sheepType) {
        return GetSpeed(DamageType.balistaLand, sheepType);
    }
    public static float GetEatDamage(SheepType sheepType) {
        return GetSpeed(DamageType.balistaLand, sheepType);
    }
    public static float GetSpeed(DamageType damageType, SheepType sheepType) {
        switch (damageType) {
            case DamageType.balistaLand:
                return Instance.balistaDamages.Where(x => x.sheepType == sheepType).Select(x => x.damage).FirstOrDefault();
            case DamageType.kickLand:
                return Instance.kickDamages.Where(x => x.sheepType == sheepType).Select(x => x.damage).FirstOrDefault();
            case DamageType.eat:
                return Instance.eatDamages.Where(x => x.sheepType == sheepType).Select(x => x.damage).FirstOrDefault();
        }
        return 100;
    }

    [System.Serializable]
    public class DamageProperty {
        public float damage;
        public SheepType sheepType;
    }
}
public enum DamageType {
    balistaLand,
    kickLand,
    eat
}