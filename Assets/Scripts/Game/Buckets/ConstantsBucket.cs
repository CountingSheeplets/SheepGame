using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantsBucket : Singleton<ConstantsBucket> {
    [Header("Arena Destruction")]
    [Tooltip("Time between 0 and whatever. Higher number - longer time.")]
    [SerializeField] float playfieldFadeTime = 2f;
    public static float PlayfieldFadeTime { get { return Instance.playfieldFadeTime; } }

    [Tooltip("This determines how coarse is the noise factor when playfield background fades")]
    [SerializeField] Vector2 playfieldFadeNoiseTiling = new Vector2(1f, 1f);
    public static Vector2 PlayfieldFadeNoiseTiling { get { return Instance.playfieldFadeNoiseTiling; } }

    [Range(0, 1)]
    [SerializeField] float playfieldFadeProportion = 1f;
    public static float PlayfieldFadeProportion { get { return Instance.playfieldFadeProportion; } }

    [SerializeField] float playfieldFloatTime = 1.5f;
    public static float PlayfieldFloatTime { get { return Instance.playfieldFloatTime; } }

    [Header("Arena Generation")]
    [SerializeField] float playfieldSize = 3f;
    public static float PlayfieldSize { get { return Instance.playfieldSize; } }

    [SerializeField] float playfieldArrangeFloatSpeed = 5f;
    public static float PlayfieldFloatSpeed { get { return Instance.playfieldArrangeFloatSpeed; } }

    [SerializeField] int emptySpacesBetweenFields = 1;
    public static float EmptySpacesBetweenFields { get { return Instance.emptySpacesBetweenFields; } }

    [SerializeField] float fenceFieldSubdivision = 2;
    public static float FenceFieldSubdivision { get { return Instance.fenceFieldSubdivision; } }

    [Header("Sheep")]
    [SerializeField] int sheepSpawnCapBase = 10;
    public static int SheepSpawnCapBase { get { return Instance.sheepSpawnCapBase; } }

    [SerializeField] int sheepSpawnCapIncrement = 2;
    public static int SheepSpawnCapIncrement { get { return Instance.sheepSpawnCapIncrement; } }

    [SerializeField] float sheepSpawnPeriod = 5f;
    public static float SheepSpawnPeriod { get { return Instance.sheepSpawnPeriod; } }

    [SerializeField] float sheepSpawnUpgradeDecrement = 0.05f;
    public static float SheepSpawnUpgradeDecrement { get { return Instance.sheepSpawnUpgradeDecrement; } }

    [SerializeField] float sheepThrowStrength = 1f;
    public static float SheepThrowStrength { get { return Instance.sheepThrowStrength; } }

    [SerializeField] float eatInterval = 1f;
    public static float EatInterval { get { return Instance.eatInterval; } }

    [SerializeField] float incrementalEatValue = 0.05f;
    public static float IncrementalEatValue { get { return Instance.incrementalEatValue; } }

    [SerializeField] float roamInterval = 1f;
    public static float RoamInterval { get { return Instance.roamInterval; } }

    [SerializeField] float roamProbability = 0.25f;
    public static float RoamProbability { get { return Instance.roamProbability; } }

    [SerializeField] public float hitRange = 0.2f;
    public static float HitRange { get { return Instance.hitRange; } }

    [SerializeField] public float hitDamage = 7.0f;
    public static float HitDamage { get { return Instance.hitDamage; } }

    [SerializeField] public float smallUpgradeShrinkSize = 0.7f;
    public static float SmallUpgradeShrinkSize { get { return Instance.smallUpgradeShrinkSize; } }

    [Header("Sheep upgrades")]
    [SerializeField] public float smallSheepBonusEat = 0.1f;
    public static float SmallSheepBonusEat { get { return Instance.smallSheepBonusEat; } }

    [Header("King")]
    [SerializeField] float kingSmiteRange = 1f;
    public static float KingSmiteRange { get { return Instance.kingSmiteRange; } }

    [SerializeField] float kingHitRadius = 0.5f;
    public static float KingHitRadius { get { return Instance.kingHitRadius; } }

    [SerializeField] float kingHitRadiusIncrement = 0.05f;
    public static float KingHitRadiusIncrement { get { return Instance.kingHitRadiusIncrement; } }

    [SerializeField] float sheepKickStrength = 1f;
    public static float SheepKickStrength { get { return Instance.sheepKickStrength; } }

    [SerializeField] float kingScaleChangeTime = 0.5f;
    public static float KingScaleChangeTime { get { return Instance.kingScaleChangeTime; } }

    [SerializeField] float kingEatBonus = 0.15f;
    public static float KingEatBonus { get { return Instance.kingEatBonus; } }

    [SerializeField] float kingEatIncrement = 0.05f;
    public static float KingEatIncrement { get { return Instance.kingEatIncrement; } }

    [SerializeField] float starveDamage = 1.25f;
    public static float StarveDamage { get { return Instance.starveDamage; } }

    [SerializeField] float smashEffectDelay = 0f;
    [SerializeField] AnimationClip smashAnimationClip;
    public static float SmashSpeed { get { return Instance.smashAnimationClip.length / (1f - Instance.smashEffectDelay); } }

    [SerializeField] float attackEffectDelay = 0f;
    [SerializeField] AnimationClip attackAnimationClip;
    public static float AttackSpeed { get { return Instance.attackAnimationClip.length / (2f - Instance.attackEffectDelay); } }

    [SerializeField] float headbutStarsTimer = 2f;
    public static float HeadbutStarsTimer { get { return Instance.headbutStarsTimer; } }

    [Header("Profile")]
    [SerializeField] float startingGold = 100f;
    public static float StartingGold { get { return Instance.startingGold; } }

    [SerializeField] List<Color> playerColors = new List<Color>();
    public static List<Color> PlayerColors { get { return Instance.playerColors; } }

    [SerializeField] float profileUpdateInterval = 0.5f;
    public static float ProfileUpdateInterval { get { return Instance.profileUpdateInterval; } }

    [Header("Economy")]
    [SerializeField] public float maxPlayfieldGrass = 100.0f;
    public static float MaxPlayfieldGrass { get { return Instance.maxPlayfieldGrass; } }

    [SerializeField] public int maxPlayerCombo = 3;
    public static int MaxPlayerCombo { get { return Instance.maxPlayerCombo; } }

    [SerializeField] public float baseGoldIncome = 2f;
    public static float BaseGoldIncome { get { return Instance.baseGoldIncome; } }

    [SerializeField] public float goldIncomePeriod = 3f;
    public static float GoldIncomePeriod { get { return Instance.goldIncomePeriod; } }

    [SerializeField] public float goldIncomeIncrement = 1f;
    public static float GoldIncomeIncrement { get { return Instance.goldIncomeIncrement; } }

    [Header("Other")]
    [SerializeField] public float tipLoopTimer = 10.0f;
    public static float TipLoopTimer { get { return Instance.tipLoopTimer; } }
}