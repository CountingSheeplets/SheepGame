using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingEat : MonoBehaviour
{
    KingUnit king;
    void Start()
    {
        king = GetComponent<KingUnit>();
        EventCoordinator.StartListening(EventName.System.Economy.EatGrass(), OnEat);
    }
    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.System.Economy.EatGrass(), OnEat);
    }
    void OnEat(GameMessage msg)
    {
        float eatAmount = CalculateEatAmount(msg.floatMessage);
        float totalGrass = PlayerProfileCoordinator.ModifyPlayerGrass(king.owner, -eatAmount);
        if(totalGrass <=0 )
            EventCoordinator.TriggerEvent(EventName.System.King.Hit(), GameMessage.Write().WithSheepUnit(msg.sheepUnit).WithKingUnit(king).WithOwner(king.owner));
            //if target owner == damaged owner. throw msg "king is starving!"
    }

    float CalculateEatAmount(float baseAmount){
        //do math here, like faster eating king, slower eating king etc.

        //then return final value:
        return baseAmount;
    }
    float CalculateMoneyReward(float eatAmount){
        //do math here how money income is calculated depending on king eating

        return eatAmount * 5f;
    }
}
