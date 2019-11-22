using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepRun : BaseUnitMove
{
    public SheepUnit sheep;
    public void StartRunning(float speed, Vector2 _destination){
        if(!sheep)
            sheep = GetComponent<SheepUnit>();
        //Debug.Log("StartRunning");
        destination = _destination;
        sheep.isReadying = true;
        //run animation;
        sheep.armature.animation.FadeIn("Walk1", 0.25f, -1);

        //move the transform to destination
        MoveToDestination(speed, 0f);
}

    public  override void PostMoveAction(){
        //trigger ready to jump/launch animation
        Debug.Log("run eneded, stopping at:"+(Vector2)(transform.position));
        sheep.armature.animation.FadeIn("Idle1", 0.25f, -1);

        //trigger Land game event
        sheep.isReadying = false;
        Playfield playfield = sheep.currentPlayfield;
        EventCoordinator.TriggerEvent(EventName.System.Sheep.ReadyToLaunch(), GameMessage.Write().WithSheepUnit(GetComponent<SheepUnit>()).WithPlayfield(playfield));
        //this is listened by Sheep Throw!
    }
}
