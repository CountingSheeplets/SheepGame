using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepEatWriter : MonoBehaviour
{
 // could do that base eating of all sheep increases over time
    //to increase game speed for late game
    float counter;

    // Update is called once per frame
    void Update()
    {
        counter+=Time.deltaTime;
        if(counter > ConstantsBucket.EatInterval){
            counter = 0;
            EventCoordinator.TriggerEvent(EventName.System.Economy.EatGrass(), GameMessage.Write().WithFloatMessage(ConstantsBucket.BaseEatValue));
            //foreach(Playfield field in ArenaCoordinator.GetPlayfields){
                //float eatValue = SheepCoordinator.GetSheepInField(field, SheepType.None) * defaultEatValue;
                //EventCoordinator.TriggerEvent(EventName.System.Economy.EatGrass(), GameMessage.Write().WithPlayfield(field).WithFloatMessage(eatValue));
            //}
            //PlayerProfileCoordinator.ReduceGrass(float amount)
        }
    }
}
