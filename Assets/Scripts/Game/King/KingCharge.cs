using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingCharge : BaseUnitMove {
    KingUnit king;
    void Awake() {
        king = GetComponent<KingUnit>();
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnSmashed);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnSmashed);
    }
    public void StartCharging(Vector2 _destination) {
        SetScale();
        SetDestination(_destination, true);
        king.isRoaming = true;
        MoveToDestination(SpeedBucket.GetRunSpeed(SheepType.King), 0f);
        animator.WalkTo(_destination);
        Debug.Log("charging started");
    }
    void OnSmashed(GameMessage msg) {
        if (msg.kingUnit == king)
            isMoving = true;
    }

    public override void PostMoveAction() {
        isMoving = false;
        animator.StopWalking();
        animator.Attack();
        //then animate hit
    }
}