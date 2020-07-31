using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KnockDistanceBucket : Singleton<KnockDistanceBucket> {
    public List<DistanceProperty> strengths = new List<DistanceProperty>();
    public static float GetKnockStrength(SheepType sheepType) {
        return Instance.strengths.Where(x => x.sheepType == sheepType).Select(x => x.knockStrength).FirstOrDefault();
    }
}

[System.Serializable]
public class DistanceProperty {
    public float knockStrength;
    public SheepType sheepType;
}