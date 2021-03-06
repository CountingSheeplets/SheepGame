﻿using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SpineContainerBlendsFour : MonoBehaviour, IAnimatableDirection {
    public float prevAngle;
    public Animator anim;
    SkeletonMecanim mecanim;
    Color color;
    //string ownerName = "";
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
        //if (ownerName == "")
        //ownerName = GetComponentInParent<KingUnit>().owner.ownerName;

        Vector2 prevDir = new Vector2(anim.GetFloat("dirX_blend"), anim.GetFloat("dirY_blend"));
        Vector2 newDir = SetAnimatorDirections(target);
        float turn = GetAngle(prevDir, newDir);
        if (turn > 0) {
            anim.SetTrigger("clockwise");
            //Debug.Log("clockwise:" + ownerName);
        } else {
            if (turn < 0) {
                anim.SetTrigger("counterClockwise");
                //Debug.Log("counterClockwise:" + ownerName);
            } else {
                anim.SetTrigger("straight");
                //Debug.Log("straight:" + ownerName);
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
        //Debug.Log("setting dir_blend: " + newDir + " : " + ownerName);
        anim.SetFloat("dirX_blend", newDir.x);
        anim.SetFloat("dirY_blend", newDir.y);
        return newDir;
    }
    public void Attack() {
        ResetAllTriggers();
        anim.SetTrigger("attack");
    }
    public void StopWalking() {
        anim.SetTrigger("stopWalk");
    }
    public void StopFlying() {
        anim.SetTrigger("stopFly");
    }
    float GetAngle(Vector2 v2, Vector2 v1) {
        Vector2 vec1Rotated90 = new Vector2(-v1.y, v1.x);
        float sign = (Vector2.Dot(vec1Rotated90, v2) < 0) ? -1.0f : 1.0f;
        return Vector2.Angle(v1, v2) * sign;
    }
    void ResetAllTriggers() {
        anim.ResetTrigger("smash");
        anim.ResetTrigger("stopWalk");
        anim.ResetTrigger("clockwise");
        anim.ResetTrigger("counterClockwise");
        anim.ResetTrigger("straight");
        anim.ResetTrigger("attack");
        anim.ResetTrigger("die");
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
        angle += 45f;
        if (angle < 0)
            angle += 360f;
        FacingDirection dir = (FacingDirection)Mathf.FloorToInt(angle / 90f);
        return dir;
    }
    public Vector2 EnumToAnimVec(FacingDirection dir) {
        Vector2 output = Vector2.zero;
        switch (dir) {
            case FacingDirection.Top:
                output = new Vector2(0, 1);
                break;
            case FacingDirection.Right:
                output = new Vector2(1, 0);
                break;
            case FacingDirection.Bottom:
                output = new Vector2(0, -1);
                break;
            case FacingDirection.Left:
                output = new Vector2(-1, 0);
                break;
        }
        return output;
    }
    public enum FacingDirection {
        Top,
        Right,
        Bottom,
        Left
    }
}