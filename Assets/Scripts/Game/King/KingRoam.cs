using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingRoam : BaseUnitMove {
    KingUnit king;
    void Awake() {
        king = GetComponent<KingUnit>();
        EventCoordinator.StartListening(EventName.System.Sheep.Roam(), OnRoam);
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnSmited);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Roam(), OnRoam);
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnSmited);
    }
    public void StartWalking(Vector2 _destination) {
        SetScale();
        SetDestination(_destination, true);
        king.isRoaming = true;
        MoveToDestination(SpeedBucket.GetRoamSpeed(SheepType.King), 0f);
        animator.WalkTo(_destination);
    }
    void OnSmited(GameMessage msg) {
        if (msg.kingUnit == king)
            isMoving = true;
    }
    void OnRoam(GameMessage msg) {
        if (!king.isRoaming && !king.GetIsUsingAbility()) {
            float roll = Random.Range(0, 3f); //3x larger roll = 3x smaller probability
            if (roll < msg.floatMessage) {
                Vector2 targetPosition = RoamTarget();
                if (targetPosition != Vector2.zero)
                    StartWalking(targetPosition);
            }
        }
    }
    Vector2 RoamTarget() {
        Vector2 newVec = new Vector2(0, 1.5f * ConstantsBucket.PlayfieldTileSize);
        newVec = Quaternion.AngleAxis(Random.Range(0, 359), Vector3.forward) * newVec;
        Vector2 targetPos = (Vector2) transform.position + newVec;
        if (king.myPlayfield.fieldCorners.IsWithinField(targetPos, king.GetRadius())) {
            return targetPos;
        } else {
            targetPos = transform.position + Quaternion.AngleAxis(90, Vector3.forward) * newVec;
            if (king.myPlayfield.fieldCorners.IsWithinField(targetPos, king.GetRadius())) {
                return targetPos;
            } else {
                targetPos = transform.position + Quaternion.AngleAxis(180, Vector3.forward) * newVec;
                if (king.myPlayfield.fieldCorners.IsWithinField(targetPos, king.GetRadius())) {
                    return targetPos;
                }
            }
        }
        return Vector2.zero;
    }
    public override void PostMoveAction() {
        king.isRoaming = false;
        animator.StopWalking();
    }
}