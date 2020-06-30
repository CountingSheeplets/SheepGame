using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SheepFly : BaseUnitMove {
    SheepUnit sheep;
    SortingGroup sGroup;
    public void StartFlying(float speed, Vector2 _destination) {
        if (!sheep)
            sheep = GetComponent<SheepUnit>();
        if (!sGroup)
            sGroup = sheep.GetComponentInChildren<SortingGroup>();
        Debug.Log("StartFlying to:" + _destination);
        sheep.isFlying = true;
        sheep.isReadyToFly = false;
        SetDestination(_destination, false);
        MoveToDestination(speed, 1.5f);
        //run animation;
        animator.FlyTo(_destination);
        if (sGroup)
            sGroup.sortingOrder = 200;
    }

    public override void PostMoveAction() {
        if (sGroup)
            sGroup.sortingOrder = 100;
        GetComponent<SheepUnit>().isFlying = false;
        //trigger to play Land animation
        Debug.Log("fly eneded, landing at:" + (Vector2) (transform.position));
        animator.StopFlying();
        //trigger Land game event, listened by sheep throw
        Playfield newPlayfield = ArenaCoordinator.GetPlayfield(transform.position);
        sheep.currentPlayfield = newPlayfield;
        sheep.ResetContainer();
        EventCoordinator.TriggerEvent(EventName.System.Sheep.Land(), GameMessage.Write().WithSheepUnit(GetComponent<SheepUnit>()).WithPlayfield(newPlayfield));
    }
}