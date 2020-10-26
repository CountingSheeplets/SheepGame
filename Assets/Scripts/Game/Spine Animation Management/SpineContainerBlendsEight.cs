using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SpineContainerBlendsEight : MonoBehaviour, IAnimatableDirection {
    public float prevAngle;
    public Animator anim;
    SkeletonMecanim mecanim;

    public void SetInitialRandomDirection() {
        if (anim == null)anim = GetComponent<Animator>();
        if (mecanim == null)mecanim = GetComponent<SkeletonMecanim>();
        Vector2 direction = Random.insideUnitCircle.normalized + (Vector2)transform.position;
        prevAngle = Vector2.SignedAngle(direction, Vector2.up);
        SetAnimatorDirections(direction);
    }

    public void WalkTo(Vector2 target) {
        if (anim == null)anim = GetComponent<Animator>();
        if (mecanim == null)mecanim = GetComponent<SkeletonMecanim>();
        ResetAllTriggers();
        Vector2 prevDir = new Vector2(anim.GetFloat("dirX_blend"), anim.GetFloat("dirY_blend"));
        Vector2 newDir = SetAnimatorDirections(target);
        float turn = GetAngle(prevDir, newDir);
        if (turn > 0) {
            anim.SetTrigger("clockwise");
        } else {
            if (turn < 0) {
                anim.SetTrigger("counterClockwise");
            } else {
                anim.SetTrigger("straight");
            }
        }
    }

    public void FlyTo(Vector2 target) {
        ResetAllTriggers();
        anim.SetTrigger("fly");
        SetAnimatorDirections(target);
    }
    public void Die() {
        if (anim == null)anim = GetComponent<Animator>();
        anim.SetTrigger("die");
    }
    Vector2 SetAnimatorDirections(Vector2 target) {
        Vector2 newDir = EnumToAnimVec(GetAnimEnum(target));
        anim.SetFloat("dirX_blend", newDir.x);
        anim.SetFloat("dirY_blend", newDir.y);
        return newDir;
    }
    public void Attack() {}
    public void StopWalking() {
        anim.SetTrigger("stopWalk");
    }
    public void StopFlying() {
        anim.SetTrigger("stopFly");
    }
    float GetAngle(Vector2 v1, Vector2 v2) {
        var sign = Mathf.Sign(v1.x * v2.y - v1.y * v2.x);
        return Vector2.Angle(v1, v2) * sign;
    }
    void OnDestroy() {
        if (!GameStateView.HasState(GameState.ended))
            SkeletonRendererController.RemoveFromSets(mecanim);
    }
    void ResetAllTriggers() {
        SkeletonRendererController.MakeSheepActive(mecanim);
        //mecanim.enabled = true;
        anim.ResetTrigger("stopWalk");
        anim.ResetTrigger("stopFly");
        anim.ResetTrigger("clockwise");
        anim.ResetTrigger("counterClockwise");
        anim.ResetTrigger("straight");
        anim.ResetTrigger("fly");
    }
    FacingDirection GetAnimEnum(Vector2 target) {
        Vector2 direction = target - (Vector2)transform.position;
        float angle = 0;
        if (direction.magnitude > 0.05f) { //this fixes Idle dir keeping same as walking dir
            angle = Vector2.SignedAngle(direction, Vector2.up);
            prevAngle = angle;
        } else {
            angle = prevAngle;
        }
        if (angle < 0)
            angle += 360f;
        angle += 22.5f;
        FacingDirection dir = 0;
        for (int i = 0; i < 8; i++) {
            if (angle > i * 45f) {
                dir = (FacingDirection)i;
            } else break;
        }
        //Debug.Log("Output Dir Name:"+dir.ToString());
        return dir;
    }
    public Vector2 EnumToAnimVec(FacingDirection dir) {
        Vector2 output = Vector2.zero;
        switch (dir) {
            case FacingDirection.Top:
                output = new Vector2(0, 1);
                break;
            case FacingDirection.TopRight:
                output = new Vector2(1, 1);
                break;
            case FacingDirection.Right:
                output = new Vector2(1, 0);
                break;
            case FacingDirection.BottomRight:
                output = new Vector2(1, -1);
                break;
            case FacingDirection.Bottom:
                output = new Vector2(0, -1);
                break;
            case FacingDirection.BottomLeft:
                output = new Vector2(-1, -1);
                break;
            case FacingDirection.Left:
                output = new Vector2(-1, 0);
                break;
            case FacingDirection.TopLeft:
                output = new Vector2(-1, 1);
                break;
        }
        return output;
    }
    public enum FacingDirection {
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left,
        TopLeft
    }
}