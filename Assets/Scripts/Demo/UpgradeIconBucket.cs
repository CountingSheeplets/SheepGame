using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeIconBucket : Singleton<UpgradeIconBucket> {

    public Sprite Small;
    public Sprite Bouncy;
    public Sprite Greedy;
    public Sprite Armored;
    public Sprite Trench;
    public Sprite Tank;
    public Sprite None;

    public static Sprite GetIcon(SheepType sheepType) {
        switch (sheepType) {
            case SheepType.Small:
                return Instance.Small;
            case SheepType.Bouncy:
                return Instance.Bouncy;
            case SheepType.Greedy:
                return Instance.Greedy;
            case SheepType.Armored:
                return Instance.Armored;
            case SheepType.Trench:
                return Instance.Trench;
            case SheepType.Tank:
                return Instance.Tank;
            default:
                return Instance.None;
        }
    }   
}
