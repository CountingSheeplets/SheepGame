using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepEat : MonoBehaviour
{
    SheepUnit sheep;
    public float bonusEatIfSheepIsSmall = 0.1f;
    public float bonusMoneyIfIsGreedy = 0.15f;
    void Start()
    {
        sheep = GetComponent<SheepUnit>();
        EventCoordinator.StartListening(EventName.System.Economy.EatGrass(), OnEat);
    }
    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.System.Economy.EatGrass(), OnEat);
    }
    void OnEat(GameMessage msg)
    {
        if(sheep.currentPlayfield == null)
            return;
        Owner owner = sheep.currentPlayfield.owner;
        if(owner.EqualsByValue(GetComponentInParent<Playfield>().owner))
            return;
        float eatAmount = CalculateEatAmount(msg.floatMessage);
        float totalGrass = PlayerProfileCoordinator.ModifyPlayerGrass(sheep.currentPlayfield.owner, -eatAmount);
        
        float multiplier = 1;
        if(totalGrass <=0)
            multiplier = 2;//PriceManager multipliers.NoGrass()
        float moneyAmount = (eatAmount + bonusMoneyIfIsGreedy) / 2f * multiplier;
        PlayerProfileCoordinator.GetProfile(owner).AddMoney(moneyAmount);
    }

    float CalculateEatAmount(float baseAmount){
        //do math here, like faster eating sheep, slower eating sheep etc.
        if(sheep.sheepType == SheepType.Small){
            return baseAmount + bonusEatIfSheepIsSmall;
        }
        //then return final value:
        return baseAmount;
    }
    float CalculateMoneyReward(float eatAmount){
        //do math here how money income is calculated depending on sheep eating

        return eatAmount * 5f;
    }
}