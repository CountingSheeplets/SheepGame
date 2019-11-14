using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepEatWriter : MonoBehaviour
{
    public float eatInterval = 1f;
    float defaultEatValue = 0.05f;
    float counter;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter+=Time.deltaTime;
        if(counter > eatInterval){
            counter = 0;
            //EventCoordinator.TriggerEvent(EventName.System.Sheep.Roam(), GameMessage.Write().WithFloatMessage(roamProbability));

            //float amount = 
            //PlayerProfileCoordinator.ReduceGrass(float amount)
        }
    }
}
