using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepRun : BaseUnitMove
{
    public void StartRunning(float speed, Vector2 _destination){
        Debug.Log("StartRunning");
        destination = _destination;
        GetComponent<SheepUnit>().isReadying = true;
        //run animation;

        //move the transform to destination
        MoveToDestination(speed, 0f);
}

    public  override void PostMoveAction(){
        //trigger ready to jump/launch animation
        Debug.Log("run eneded, stopping at:"+(Vector2)(transform.position));

        //trigger Land game event
        GetComponent<SheepUnit>().isReadying = false;
        Playfield playfield = GetComponent<SheepUnit>().currentPlayfield;
        EventCoordinator.TriggerEvent(EventName.System.Sheep.ReadyToLaunch(), GameMessage.Write().WithSheepUnit(GetComponent<SheepUnit>()).WithPlayfield(playfield));
        //this is listened by Sheep Throw!
    }
}
