using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepRun : BaseUnitMove {
    public SheepUnit sheep;
    public void StartRunning(float speed, Vector2 _destination) {
        if (GameStateView.HasState(GameState.arenaAnimating))
            return;
        if (!sheep)
            sheep = GetComponent<SheepUnit>();
        SetDestination(_destination, true);
        sheep.isReadying = true;
        //move the transform to destination
        MoveToDestination(speed, 0f);
        //run animation;
        animator.WalkTo(_destination);
    }

    public override void PostMoveAction() {
        animator.StopWalking();

        sheep.isReadying = false;
        Playfield playfield = sheep.currentPlayfield;
        EventCoordinator.TriggerEvent(EventName.System.Sheep.ReadyToLaunch(), GameMessage.Write().WithSheepUnit(GetComponent<SheepUnit>()).WithPlayfield(playfield));
    }
}