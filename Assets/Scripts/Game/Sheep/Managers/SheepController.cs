using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Environment.EndMatch(), OnEndMatch);
    }
    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.System.Environment.EndMatch(), OnEndMatch);
    }
    void OnEndMatch(GameMessage msg)
    {
        foreach(SheepUnit sheep in SheepCoordinator.GetSheeps()){
            if(sheep != null)
                if(sheep.isSwimming || sheep.isFlying)
                    EventCoordinator.TriggerEvent(EventName.System.Sheep.Kill(), GameMessage.Write().WithSheepUnit(sheep));
        }
    }
}
