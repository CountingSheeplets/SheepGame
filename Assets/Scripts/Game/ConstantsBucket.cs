using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantsBucket : Singleton<ConstantsBucket>
{
    [Header("Arena Destruction")]
    [Tooltip("Time between 0 and whatever. Higher number - longer time.")]
    [SerializeField] float playfieldFadeTime = 2f;
    public static float PlayfieldFadeTime{        get{return Instance.playfieldFadeTime;}    }
    [Tooltip("This determines how coarse is the noise factor when playfield background fades")]
    [SerializeField] Vector2 playfieldFadeNoiseTiling = new Vector2(1f, 1f);
    public static Vector2 PlayfieldFadeNoiseTiling{        get{return Instance.playfieldFadeNoiseTiling;}    }
    [Range(0,1)]
    [SerializeField] float playfieldFadeProportion = 1f;
    public static float PlayfieldFadeProportion{get{return Instance.playfieldFadeProportion;}}
    [Header("Arena Generation")]
    [SerializeField] int playfieldGridSize = 11;
    public static int GridSize {get {return Instance.playfieldGridSize;}}
    [SerializeField] float playfieldTileSize = 0.5f;
    public static float PlayfieldTileSize {get {return Instance.playfieldTileSize;}}
    [SerializeField] Vector2 playfieldTileScale = new Vector2(2.5f,2.5f);
    public static Vector2 PlayfieldTileScale {get {return Instance.playfieldTileScale;}}
    [SerializeField] float playfieldArrangeFloatSpeed = 5f;
    public static float PlayfieldFloatSpeed {get {return Instance.playfieldArrangeFloatSpeed;}}
    [SerializeField] int emptySpacesBetweenFields = 1;
    public static float EmptySpacesBetweenFields {get {return Instance.emptySpacesBetweenFields;}}

    [Header("Sheep")]
    [SerializeField] float sheepThrowStrength = 1f;
    public static float SheepThrowStrength{get{return Instance.sheepThrowStrength;}}
    [SerializeField] float eatInterval = 1f;
    public static float EatInterval{        get{return Instance.eatInterval;}    }
    [SerializeField] float baseEatValue = 0.05f;
    public static float BaseEatValue{        get{return Instance.baseEatValue;}    }
    [SerializeField] float incrementalEatValue = 0.05f;
    public static float IncrementalEatValue{        get{return Instance.incrementalEatValue;}    }

    [SerializeField] float roamInterval = 1f;
    public static float RoamInterval{        get{return Instance.roamInterval;}    }
    [SerializeField] float roamProbability = 0.25f;
    public static float RoamProbability{        get{return Instance.roamProbability;}    }
    [SerializeField] public float hitRange = 0.2f;
    public static float HitRange{        get{return Instance.hitRange;}    }

    [Header("King")]
    [SerializeField] float kingSmiteRange = 1f;
    public static float KingSmiteRange{        get{return Instance.kingSmiteRange;}    }
    [SerializeField] float kingHitRadius = 0.5f;
    public static float KingHitRadius{        get{return Instance.kingHitRadius;}    }
    [SerializeField] float kingHitRadiusIncrement = 0.05f;
    public static float KingHitRadiusIncrement{        get{return Instance.kingHitRadiusIncrement;}    }
    [Header("Profile")]
    [SerializeField] List<Color> playerColors = new List<Color>();
    public static List<Color> PlayerColors{        get{return Instance.playerColors;}    }
    [SerializeField] float profileUpdateInterval = 0.5f;
    public static float ProfileUpdateInterval{        get{return Instance.profileUpdateInterval;}    }
}
