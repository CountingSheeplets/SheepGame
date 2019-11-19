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
        PlayerProfileCoordinator.ModifyPlayerGrass(sheep.currentPlayfield.owner, CalculateEatAmount(msg.floatMessage));
    }

    float CalculateEatAmount(float baseAmount){
        //do math here, like faster eating sheep, slower eating sheep etc.

        //then return final value:
        return baseAmount;
    }
}