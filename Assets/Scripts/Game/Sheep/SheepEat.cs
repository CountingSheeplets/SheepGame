using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepEat : MonoBehaviour {
    SheepUnit sheep;

    void Start() {
        sheep = GetComponent<SheepUnit>();
        EventCoordinator.StartListening(EventName.System.Economy.EatGrass(), OnEat);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Economy.EatGrass(), OnEat);
    }
    void OnEat(GameMessage msg) {
        if (sheep.currentPlayfield == null)
            return;
        Owner playfieldOwner = sheep.currentPlayfield.owner;
        if (playfieldOwner.EqualsByValue(sheep.owner))
            return;
        float eatAmount = CalculateEatAmount();
        float totalGrass = PlayerProfileCoordinator.ModifyPlayerGrass(sheep.currentPlayfield.owner, -eatAmount);
        Debug.Log("sheep grass eaten " + eatAmount);

        float multiplier = 1;
        if (totalGrass <= 0)
            multiplier = ConstantsBucket.IncomeMultiplierNoGrass; //PriceManager multipliers.NoGrass()
        float moneyAmount = (eatAmount + ConstantsBucket.GreedySheepBonusMoney) * multiplier;
        PlayerProfileCoordinator.GetProfile(sheep.owner).AddMoney(moneyAmount);
    }

    float CalculateEatAmount() {
        //do math here, like faster eating sheep, slower eating sheep etc.
        if (sheep.sheepType == SheepType.Small) {
            return ConstantsBucket.BaseEatValue + ConstantsBucket.SmallSheepBonusEat;
        }
        //then return final value:
        return ConstantsBucket.BaseEatValue;
    }
    float CalculateMoneyReward(float eatAmount) {
        //do math here how money income is calculated depending on sheep eating

        return eatAmount * 5f;
    }
}