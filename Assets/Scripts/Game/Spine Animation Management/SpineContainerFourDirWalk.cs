using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineContainerFourDirWalk : MonoBehaviour, IAnimatableDirection {
    public Animator anim;
    public float prevAngle;
    public string currentPoint = "to1";
    List<string> pathPoints = new List<string> {
        "to1",
        "to2",
        "to3",
        "to4"
    };
    void Start() {
        if (anim == null) anim = GetComponent<Animator>();
    }
    public void FlyTo(Vector2 target) { }
    public void StopFlying() { }
    public void Die() {
        anim.SetTrigger("die");
    }
    public void WalkTo(Vector2 target) {
        anim.ResetTrigger("stop");
        string dirNum = EnumToAnimNum(GetAnimEnum(target));
        string animation = "to" + dirNum.ToString();

        //Debug.Log("new anim set to:"+animation);
        foreach (string animName in GetAnimPath(animation)) {
            //Debug.Log("anim trigger:"+animName);
            anim.SetTrigger(animName);
        }
        currentPoint = animation;
    }
    public void Attack() {
        ResetTriggers();
        anim.SetTrigger("attack");
    }
    void ResetTriggers() {
        anim.ResetTrigger("smash");
        anim.ResetTrigger("die");
        anim.ResetTrigger("stop");
        anim.ResetTrigger("to1");
        anim.ResetTrigger("to2");
        anim.ResetTrigger("to3");
        anim.ResetTrigger("to4");
        anim.ResetTrigger("attack");
    }
    public void StopWalking() {
        //Debug.Log("stopping");
        ResetTriggers();
        anim.SetTrigger("stop");
    }

    List<string> GetAnimPath(string nextAnim) {
        List<string> output = new List<string>();
        int currentIndex = pathPoints.FindIndex(a => a == currentPoint);
        int nextIndex = pathPoints.FindIndex(a => a == nextAnim);
        int counterClock = clockDir(currentIndex, nextIndex, pathPoints.Count);
        //Debug.Log("current:"+currentIndex+" next:"+nextIndex+" dir:"+counterClock);
        if (currentIndex == nextIndex) {
            output.Add(pathPoints[currentIndex]);
            return output;
        }
        if (counterClock > 0) {
            int i = currentIndex;
            while (true) {
                if (i == pathPoints.Count - 1)
                    i = 0;
                else
                    i++;
                if (i != nextIndex) {
                    output.Add(pathPoints[i]);

                } else {
                    output.Add(pathPoints[i]);
                    break;
                }
            }
        } else {
            int i = currentIndex;
            while (true) {
                if (i == 0)
                    i = pathPoints.Count - 1;
                else
                    i--;
                if (i != nextIndex) {
                    output.Add(pathPoints[i]);

                } else {
                    output.Add(pathPoints[i]);
                    break;
                }
            }
        }

        return output;
    }
    int clockDir(int first, int second, int length) {
        int clockDir = 0;
        if (first < second)
            if (second - first < length / 2)
                clockDir = 1;
            else
                clockDir = -1;
        else
        if (first - second < length / 2)
            clockDir = -1;
        else
            clockDir = 1;

        return clockDir;
    }
    int getClockDist(int clockDir, int first, int second, int length) {
        if (clockDir > 0) {
            if (first - second < 0) {
                return second - first;
            } else {
                return first + length - second;
            }
        } else {
            if (first - second < 0) {
                return second + length - first;
            } else {
                return first - second;
            }
        }
    }
    FacingDirection GetAnimEnum(Vector2 target) {
        Vector2 direction = target - (Vector2) transform.position;
        float angle = 0;
        if (direction.magnitude > 0.05f) { //this fixes Idle dir keeping same as walking dir
            angle = Vector2.SignedAngle(direction, Vector2.up);
            prevAngle = angle;
        } else {
            angle = prevAngle;
            //Debug.Log("idling angle:"+angle);
        }
        //Debug.Log("rawDir:"+direction+" RawAngle:"+ Vector2.SignedAngle(Vector2.up, direction));
        if (angle < 0)
            angle += 360f;
        angle += 45f;
        //Debug.Log("calculated angle:"+angle);
        FacingDirection dir = 0;
        for (int i = 0; i < 4; i++) {
            if (angle > i * 90f) {
                dir = (FacingDirection) i;
            } else break;
        }
        //Debug.Log("Output Dir Name:"+dir.ToString());
        return dir;
    }
    public string EnumToAnimNum(FacingDirection dir) {
        string output = "";
        switch (dir) {
            case FacingDirection.Top:
                output = "3";
                break;
            case FacingDirection.Right:
                output = "4";
                break;
            case FacingDirection.Bottom:
                output = "1";
                break;
            case FacingDirection.Left:
                output = "2";
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