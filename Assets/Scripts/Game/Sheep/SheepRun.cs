using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepRun : BaseUnitMove {
    public SheepUnit sheep;
    public void StartRunning(float speed, Vector2 _destination) {
        if (!sheep)
            sheep = GetComponent<SheepUnit>();
        //Debug.Log("StartRunning");
        SetDestination(_destination, true);
        sheep.isReadying = true;
        //move the transform to destination
        MoveToDestination(speed, 0f);
        //run animation;
        //animator.FadeIn(destination, AnimatorContainer.Animation.Walk);
        animator.WalkTo(_destination);
    }

    public override void PostMoveAction() {
        //trigger ready to jump/launch animation
        //Debug.Log("run eneded, stopping at:"+(Vector2)(transform.position));
        //animator.FadeIn(destination, AnimatorContainer.Animation.Idle);
        animator.StopWalking();

        //trigger Land game event
        sheep.isReadying = false;
        Playfield playfield = sheep.currentPlayfield;
        EventCoordinator.TriggerEvent(EventName.System.Sheep.ReadyToLaunch(), GameMessage.Write().WithSheepUnit(GetComponent<SheepUnit>()).WithPlayfield(playfield));
        //this is listened by Sheep Throw!
    }
}