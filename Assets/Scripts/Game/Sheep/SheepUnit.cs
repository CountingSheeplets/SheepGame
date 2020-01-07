using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class SheepUnit : MonoBehaviour
{
    public Owner owner;
    public Owner lastHandler;
    public bool canBeThrown {
        get{
            Debug.Log("isReadying:"+isReadying+"  isReadyToFly:"+isReadyToFly+"   isFlying:"+isFlying+"   "+isSwimming);
            return (!isReadying && !isReadyToFly && !isFlying && !isSwimming);
        }
    }
    public Playfield currentPlayfield;
    public bool isReadying = false;
    public bool isReadyToFly = false;
    public bool isFlying = false;
    public bool isSwimming = false;
    public bool isRoaming = false;
    public float radius = 0.5f;
    //[BitMask(typeof(SheepType))]
    public SheepType sheepType;
    public bool skippedByTrenching = false;

    void Start(){
        EventCoordinator.StartListening(EventName.System.Sheep.Kill(), OnKill);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Sheep.Kill(), OnKill);
    }
    void OnKill(GameMessage msg){
        if(msg.sheepUnit == this){
            //show drown animation

            //then remove sheep
            Destroy(gameObject, 1f);
        }
    }
    public void ResetContainer(){
        if(isFlying || isSwimming)
            transform.parent = SheepCoordinator.Instance.transform;
        else
            if(currentPlayfield != null)
                transform.parent = currentPlayfield.sheepParent;
    }
}
