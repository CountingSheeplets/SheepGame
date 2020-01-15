/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class AnimatorContainer : MonoBehaviour
{
    public UnityArmatureComponent armature;
    public float prevAngle;

    public void FadeIn(Vector2 target, Animation animationEnum){
        
        string dirNum = EnumToAnimNum(GetAnimEnum(target));
        string animation = animationEnum.ToString();
       // Debug.Log("anim:"+animation+dirNum);
        armature.animation.FadeIn(animation+dirNum, 0.0f, -1);

    }
    public void ChangeHat(int index){
        var displays = armature.armature.GetSlot("head7").displayList;
        int currentIndex = armature.armature.GetSlot("head7").displayIndex;
        armature.armature.GetSlot("head7")._SetDisplayIndex(index);
        armature.armature.GetSlot("head7")._SetColor(new ColorTransform());
    }

    FacingDirection GetAnimEnum(Vector2 target){
        Vector2 direction = target - (Vector2)transform.position;
        float angle = 0;
        if(direction.magnitude > 0.05f ){ //this fixes Idle dir keeping same as walking dir
            angle = Vector2.SignedAngle(direction, Vector2.up);
            prevAngle = angle;
        } else {
            angle = prevAngle;
            //Debug.Log("idling angle:"+angle);
        }
        //Debug.Log("rawDir:"+direction+" RawAngle:"+ Vector2.SignedAngle(Vector2.up, direction));
        if(angle<0)
            angle+=360f;
        angle+=22.5f;
        //Debug.Log("calculated angle:"+angle);
        FacingDirection dir = 0;
        for(int i = 0; i < 8; i++){
            if(angle > i * 45f){
                dir = (FacingDirection)i;
            } else break;
        }
        //Debug.Log("Output Dir Name:"+dir.ToString());
        return dir;
    }
    public string EnumToAnimNum(FacingDirection dir){
        string output = "";
        switch(dir){
            case FacingDirection.Top:
                output = "6";
                break;
            case FacingDirection.TopRight:
                output = "5";
                break;
            case FacingDirection.Right:
                output = "4";
                break;                
            case FacingDirection.BottomRight:
                output = "3";
                break;
            case FacingDirection.Bottom:
                output = "2";
                break;
            case FacingDirection.BottomLeft:
                output = "1";
                break;
            case FacingDirection.Left:
                output = "8";
                break;
            case FacingDirection.TopLeft:
                output = "7";
                break;
        }
        return output;
    }
    public enum FacingDirection{
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left,
        TopLeft
    }
    public enum Animation{//mach name exactly, then convert to str
        Walk,
        Idle
    }
}
 */