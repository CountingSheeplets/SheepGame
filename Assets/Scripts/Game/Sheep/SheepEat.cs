using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepEat : MonoBehaviour
{
    SheepUnit sheep;
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
        Owner owner = sheep.currentPlayfield.owner;
        if(owner.EqualsByValue(GetComponentInParent<Playfield>().owner))
            return;
        float eatAmount = CalculateEatAmount(msg.floatMessage);
        PlayerProfileCoordinator.ModifyPlayerGrass(sheep.currentPlayfield.owner, -eatAmount);
        float moneyAmount = eatAmount / 2f;
        PlayerProfileCoordinator.GetProfile(owner).AddMoney(moneyAmount);
    }

    float CalculateEatAmount(float baseAmount){
        //do math here, like faster eating sheep, slower eating sheep etc.

        //then return final value:
        return baseAmount;
    }
    float CalculateMoneyReward(float eatAmount){
        //do math here how money income is calculated depending on sheep eating

        return eatAmount * 5f;
    }
}