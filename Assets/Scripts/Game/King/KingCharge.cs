using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingCharge : BaseUnitMove {
    public delegate void OnAnimEnded(string parameterTheFunctionExpects);
    public event OnAnimEnded animEndedCallback;
    //AnimationClip attackAnimation;
    public void OnAttackAnimationEnded(string message) {}
    IEnumerator Attacking(float length) {
        yield return new WaitForSeconds(length);
        isCharging = false;
        animEndedCallback("AttackAnimationEnded");
    }
    KingUnit king;
    public bool isCharging = false; // this is falsed after animation has ended
    void Awake() {
        king = GetComponent<KingUnit>();
        //attackAnimation = ConstantsBucket.AttackSpeed;
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnSmashed);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnSmashed);
    }
    public void StartCharging(Vector2 _destination) {
        isCharging = true;
        SetScale();
        SetDestination(_destination, true);
        king.isRoaming = true;
        MoveToDestination(SpeedBucket.GetRunSpeed(SheepType.King), 0f);
        animator.WalkTo(_destination);
        //Debug.Log("charging started");
    }
    void OnSmashed(GameMessage msg) {
        if (msg.kingUnit == king)
            isMoving = true;
        isCharging = false;
    }

    public override void PostMoveAction() {
        king.isRoaming = false;
        animator.StopWalking();
        animator.Attack();
        StartCoroutine(Attacking(ConstantsBucket.AttackSpeed)); // change this when we swap to new animation
    }
}