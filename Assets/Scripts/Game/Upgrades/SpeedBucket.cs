using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class SpeedBucket : Singleton<SpeedBucket> {
    public List<SpeedProperty> flySpeeds = new List<SpeedProperty>();
    public List<SpeedProperty> runSpeeds = new List<SpeedProperty>();
    public List<SpeedProperty> roamSpeeds = new List<SpeedProperty>();
    public List<SpeedProperty> swimSpeeds = new List<SpeedProperty>();
    public List<SpeedProperty> knockbackSpeeds = new List<SpeedProperty>();
    public List<SpeedProperty> fallSpeeds = new List<SpeedProperty>();
    public static float GetFlySpeed(SheepType sheepType) {
        return GetSpeed(SpeedType.fly, sheepType);
    }
    public static float GetRunSpeed(SheepType sheepType) {
        return GetSpeed(SpeedType.run, sheepType);
    }
    public static float GetRoamSpeed(SheepType sheepType) {
        return GetSpeed(SpeedType.roam, sheepType);
    }
    public static float GetSwimSpeed(SheepType sheepType) {
        return GetSpeed(SpeedType.swim, sheepType);
    }
    public static float GetFallSpeed(SheepType sheepType) {
        return GetSpeed(SpeedType.fall, sheepType);
    }
    public static float GetKnockbackSpeed(SheepType sheepType) {
        return GetSpeed(SpeedType.knockback, sheepType);
    }
    public static float GetSpeed(SpeedType speedType, SheepType sheepType) {
        switch (speedType) {
            case SpeedType.fly:
                return Instance.flySpeeds.Where(x => x.sheepType == sheepType).Select(x => x.speed).FirstOrDefault();
            case SpeedType.run:
                return Instance.runSpeeds.Where(x => x.sheepType == sheepType).Select(x => x.speed).FirstOrDefault();
            case SpeedType.roam:
                return Instance.roamSpeeds.Where(x => x.sheepType == sheepType).Select(x => x.speed).FirstOrDefault();
            case SpeedType.swim:
                return Instance.swimSpeeds.Where(x => x.sheepType == sheepType).Select(x => x.speed).FirstOrDefault();
            case SpeedType.knockback:
                return Instance.knockbackSpeeds.Where(x => x.sheepType == sheepType).Select(x => x.speed).FirstOrDefault();
            case SpeedType.fall:
                return Instance.fallSpeeds.Where(x => x.sheepType == sheepType).Select(x => x.speed).FirstOrDefault();
        }
        return 100;
    }
}
public enum SpeedType {
    fly,
    run,
    roam,
    swim,
    knockback,
    fall
}

[System.Serializable]
public class SpeedProperty {
    public float speed;
    public SheepType sheepType;
}