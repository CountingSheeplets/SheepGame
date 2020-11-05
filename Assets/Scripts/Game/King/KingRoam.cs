using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingRoam : BaseUnitMove {
    KingUnit king;
    KingCharge charge;
    void Awake() {
        king = GetComponent<KingUnit>();
        charge = GetComponent<KingCharge>();
        EventCoordinator.StartListening(EventName.System.Sheep.Roam(), OnRoam);
        EventCoordinator.StartListening(EventName.System.Environment.Initialized(), ForceRoam);
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnSmashed);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Roam(), OnRoam);
        EventCoordinator.StopListening(EventName.System.Environment.Initialized(), ForceRoam);
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnSmashed);
    }
    public void StartWalking(Vector2 _destination) {
        SetScale();
        SetDestination(_destination, true);
        king.isRoaming = true;
        MoveToDestination(SpeedBucket.GetRoamSpeed(SheepType.King), 0f);
        animator.WalkTo(_destination);
    }
    void OnSmashed(GameMessage msg) {
        if (msg.kingUnit == king)
            isMoving = true;
    }
    void OnRoam(GameMessage msg) {
        if (charge.isCharging)
            return;
        if (!king.isRoaming && !king.GetIsUsingAbility()) {
            float roll = Random.Range(0, 0.5f); //3x larger roll = 3x smaller probability
            if (roll < msg.floatMessage) {
                ForceRoam(GameMessage.Write());
            }
        }
    }
    void ForceRoam(GameMessage msg) {
        Vector2 targetPosition = RoamTarget();
        Debug.Log("roam to newVec: " + targetPosition);
        if (targetPosition != Vector2.zero)
            StartWalking(targetPosition);
    }
    Vector2 RoamTarget() {
        Vector2 newVec = new Vector2(0, ConstantsBucket.PlayfieldSize / 4f);
        newVec = Quaternion.AngleAxis(Random.Range(0, 359), Vector3.forward) * newVec;
        Vector2 targetPos = (Vector2)transform.position + newVec;
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