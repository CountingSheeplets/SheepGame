using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepEatWriter : MonoBehaviour
{
    public float eatInterval = 1f;
    public float baseEatValue = 0.05f;
    public float incrementalEatValue = 0.05f; // could do that base eating of all sheep increases over time
    //to increase game speed for late game
    float counter;

    // Update is called once per frame
    void Update()
    {
        counter+=Time.deltaTime;
        if(counter > eatInterval){
            counter = 0;
            EventCoordinator.TriggerEvent(EventName.System.Economy.EatGrass(), GameMessage.Write().WithFloatMessage(baseEatValue));
            //foreach(Playfield field in ArenaCoordinator.GetPlayfields){
                //float eatValue = SheepCoordinator.GetSheepInField(field, SheepType.None) * defaultEatValue;
                //EventCoordinator.TriggerEvent(EventName.System.Economy.EatGrass(), GameMessage.Write().WithPlayfield(field).WithFloatMessage(eatValue));
            //}
            //PlayerProfileCoordinator.ReduceGrass(float amount)
        }
    }
}
