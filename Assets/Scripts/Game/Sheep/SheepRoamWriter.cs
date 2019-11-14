using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepRoamWriter : MonoBehaviour
{
    public float roamInterval = 1f;
    float roamProbability = 0.25f;
    float counter;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter+=Time.deltaTime;
        if(counter > roamInterval){
            counter = 0;
            EventCoordinator.TriggerEvent(EventName.System.Sheep.Roam(), GameMessage.Write().WithFloatMessage(roamProbability));
        }
    }
}
