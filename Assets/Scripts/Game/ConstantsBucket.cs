using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantsBucket : Singleton<ConstantsBucket>
{
    [Header("Arena")]
    public float playfieldFadeTime = 2f;
    public static float PlayfieldFadeTime{
        get{return Instance.playfieldFadeTime;}
    }

    public float playfieldFadeProportion = 1f;
    public static float PlayfieldFadeProportion{
        get{return Instance.playfieldFadeProportion;}
    }

    [Header("Sheep")]
    public float sheepThrowStrength = 1f;
    public static float SheepThrowStrength{
        get{return Instance.sheepThrowStrength;}
    }

}
