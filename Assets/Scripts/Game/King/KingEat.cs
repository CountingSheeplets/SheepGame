using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingEat : MonoBehaviour {
    KingUnit king;
    void Start() {
        king = GetComponent<KingUnit>();
        EventCoordinator.StartListening(EventName.System.Economy.EatGrass(), OnEat);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Economy.EatGrass(), OnEat);
    }
    void OnEat(GameMessage msg) {
        float eatAmount = CalculateEatAmount();
        float totalGrass = PlayerProfileCoordinator.ModifyPlayerGrass(king.owner, -eatAmount);
        //Debug.Log("KingUnit grass eaten "+eatAmount);
        //if(totalGrass <=0 )
        //    king.OnStarve();
        //EventCoordinator.TriggerEvent(EventName.System.King.Hit(), GameMessage.Write().WithSheepUnit(msg.sheepUnit).WithKingUnit(king).WithOwner(king.owner));
        //if target owner == damaged owner. throw msg "king is starving!"
    }

    float CalculateEatAmount() {
        //do math here, like faster eating king, slower eating king etc.
        float baseAmount = ConstantsBucket.BaseEatValue + ConstantsBucket.KingEatBonus;
        float amount = baseAmount + ConstantsBucket.KingEatIncrement * king.GetSuccesfullHits();
        //then return final value:
        return amount;
    }
    /*     float CalculateMoneyReward(float eatAmount){
            //do math here how money income is calculated depending on king eating

            return eatAmount * 5f;
        } */
}