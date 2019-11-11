using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepRoamManager : MonoBehaviour
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
            EventManager.TriggerEvent(EventName.System.Sheep.Roam(), GameMessage.Write().WithFloatMessage(roamProbability));
        }
    }
}
