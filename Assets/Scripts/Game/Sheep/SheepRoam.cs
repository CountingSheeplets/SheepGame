using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepRoam : BaseUnitMove {
    SheepUnit sheep;
    void Start() {
        EventCoordinator.StartListening(EventName.System.Sheep.Roam(), OnRoam);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Roam(), OnRoam);
    }
    public void StartWalking(Vector2 _destination) {
        if (sheep == null)
            sheep = GetComponent<SheepUnit>();
        SetDestination(_destination, true);
        sheep.isRoaming = true;
        //move the transform to destination
        MoveToDestination(SpeedBucket.GetRoamSpeed(sheep.sheepType), 0f);
        animator.WalkTo(_destination);
    }
    void OnRoam(GameMessage msg) {
        if (sheep == null && gameObject.activeSelf)
            sheep = GetComponent<SheepUnit>();
        if (GameStateView.HasState(GameState.arenaAnimating))return;
        if (!sheep.isSwimming && !sheep.isReadying && !sheep.isReadyToFly && !sheep.isFlying && !sheep.isRoaming && !sheep.isTrenching) {
            float roll = Random.Range(0, 1f);
            if (roll < msg.floatMessage) {
                Vector2 targetPosition = RoamTarget();
                if (targetPosition != Vector2.zero) {
                    StartWalking(targetPosition);
                }
            }
        }
    }
    Vector2 RoamTarget() {
        Vector2 newVec = new Vector2(0, ConstantsBucket.PlayfieldSize / 3f);
        newVec = Quaternion.AngleAxis(Random.Range(0, 359), Vector3.forward) * newVec;
        Vector2 targetPos = (Vector2)transform.position + newVec;
        if (sheep.currentPlayfield.fieldCorners.IsWithinField(targetPos, sheep.radius)) {
            //Debug.Log("taget vector ok, roaming to:"+newVec);
            return targetPos;
        } else {
            targetPos = transform.position + Quaternion.AngleAxis(90, Vector3.forward) * newVec;
            if (sheep.currentPlayfield.fieldCorners.IsWithinField(targetPos, sheep.radius)) {
                //Debug.Log("turn once, roaming to:"+newVec);
                return targetPos;
            } else {
                targetPos = transform.position + Quaternion.AngleAxis(180, Vector3.forward) * newVec;
                if (sheep.currentPlayfield.fieldCorners.IsWithinField(targetPos, sheep.radius)) {
                    //Debug.Log("turn twice, roaming to:"+newVec);
                    return targetPos;
                }
            }
        }
        return Vector2.zero;
    }
    public override void PostMoveAction() {
        sheep.isRoaming = false;
        animator.StopWalking();
    }
}