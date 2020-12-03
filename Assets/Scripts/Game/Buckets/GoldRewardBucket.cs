using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldRewardBucket : Singleton<GoldRewardBucket> {
    [Header("Flat Rewards")]
    [SerializeField] float sheepLandFieldFlat = 1f;
    public static float SheepLandFieldFlat { get { return Instance.sheepLandFieldFlat; } }

    [SerializeField] float sheepKingHitFlat = 1f;
    public static float SheepKingHitFlat { get { return Instance.sheepKingHitFlat; } }

    [SerializeField] float sheepSelfThrow = 1f;
    public static float SheepSelfThrow { get { return Instance.sheepSelfThrow; } }

    [SerializeField] float sheepOtherPlayerThrow = 1f;
    public static float SheepOtherPlayerThrow { get { return Instance.sheepOtherPlayerThrow; } }

    [SerializeField] public float greedySheepBonusMoney = 2f;
    public static float GreedySheepBonusMoney { get { return Instance.greedySheepBonusMoney; } }

    [SerializeField] public float sheepEliminated = 1f;
    public static float SheepEliminated { get { return Instance.sheepEliminated; } }

    [Header("Multipliers")]
    [SerializeField] public float incomeMultiplierNoGrass = 0.5f;
    public static float IncomeMultiplierNoGrass { get { return Instance.incomeMultiplierNoGrass; } }
    //there is also an incremental elimination manager through price
    //gold is added in PlayerEliminationHandler()
    //PriceName.Player.KingElimGold()
}