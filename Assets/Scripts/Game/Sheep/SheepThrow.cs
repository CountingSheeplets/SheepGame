using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepThrow : MonoBehaviour
{
    public List<SheepUnit> throwableSheep = new List<SheepUnit>();
    public float throwStrength;
    public float flySpeed = 1f;
    public SheepUnit sheepReadyToBeThrown;
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.Swipe(), OnSwipe);
        EventCoordinator.StartListening(EventName.System.Sheep.Spawned(), OnSpawn);
        EventCoordinator.StartListening(EventName.System.Sheep.Land(), OnLand);
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnReady);
        EventCoordinator.StartListening(EventName.System.Sheep.Kill(), OnKill);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.Input.Swipe(), OnSwipe);
        EventCoordinator.StopListening(EventName.System.Sheep.Spawned(), OnSpawn);
        EventCoordinator.StopListening(EventName.System.Sheep.Land(), OnLand);
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnReady);
        EventCoordinator.StopListening(EventName.System.Sheep.Kill(), OnKill);
    }

    void OnSwipe(GameMessage msg)
    {
        if(msg.owner.EqualsByValue(GetComponent<Owner>())){//because this is input message
            if(sheepReadyToBeThrown){
                SheepFly fly = sheepReadyToBeThrown.gameObject.GetComponent<SheepFly>();
                Debug.Log(msg.swipe);
                Vector2 destination = msg.swipe.vector * msg.swipe.distance * throwStrength / 10f + (Vector2)fly.transform.position;
                Debug.Log("fly.destination:"+destination);
                throwableSheep.Remove(sheepReadyToBeThrown);
                fly.StartFlying(SpeedBucket.GetFlySpeed(sheepReadyToBeThrown.sheepType), destination);
                sheepReadyToBeThrown.lastHandler = msg.owner;
                sheepReadyToBeThrown.currentPlayfield = null;
                sheepReadyToBeThrown = null;
            } else {
                //show animation/sign that no sheep ready to be thrown
                Debug.Log("no sheep could be found that is ready to be thrown!");
            }
            TryReadyNewSheep();
        }
    }
    void OnSpawn(GameMessage msg){
        Debug.Log("sheepSpawned:"+msg.sheepUnit);
        if(msg.sheepUnit){
            Debug.Log("msg.sheepUnit.owner:"+msg.sheepUnit.owner+" vs "+GetComponent<Owner>());
            if(msg.sheepUnit.owner.EqualsByValue(GetComponent<Owner>())){
                throwableSheep.Add(msg.sheepUnit);
                TryReadyNewSheep();
            }
        }
    }
    void OnKill(GameMessage msg){
        if(msg.sheepUnit && throwableSheep.Contains(msg.sheepUnit)){
            throwableSheep.Remove(msg.sheepUnit);
        }
    }
    void OnLand(GameMessage msg){
        if(msg.playfield == GetComponent<Playfield>()){
            Debug.Log(msg.playfield+"-adding new sheep on Land:"+msg.sheepUnit.gameObject.name);
            throwableSheep.Add(msg.sheepUnit);
            TryReadyNewSheep();
        }
    }
    void OnReady(GameMessage msg){
        if(msg.sheepUnit != null){
            if(msg.sheepUnit.currentPlayfield == GetComponent<Playfield>()){
                sheepReadyToBeThrown = msg.sheepUnit;
                sheepReadyToBeThrown.isReadyToFly = true;
            }
        }
    } 
    void TryReadyNewSheep(){
        Debug.Log("throwableSheep:"+throwableSheep.Count);
        if(!SheepIsReadying() && sheepReadyToBeThrown == null){
            SheepUnit availableSheep = GetNextSheep();
            Debug.Log("GetNextSheep:"+availableSheep);
            if(availableSheep != null){
                Debug.Log("availableSheep.currentPlayfield:"+availableSheep.currentPlayfield);
                if(availableSheep.currentPlayfield != null){
                    availableSheep.GetComponent<SheepRun>().StartRunning(SpeedBucket.GetRunSpeed(sheepReadyToBeThrown.sheepType), availableSheep.currentPlayfield.fieldCorners.Center);
                }
            } else
                Debug.Log("cant run, non null");
        } else {
            Debug.Log("IsReadying or already ready...");
        }
    }
    SheepUnit GetNextSheep(){
        foreach(SheepUnit sheep in throwableSheep){
            if(sheep.canBeThrown)
                if(!sheep.skippedByTrenching && sheep.sheepType == SheepType.Trench){
                    sheep.skippedByTrenching = true;
                    //here animate sheep trenching!!! burrow sheep
                    
                    //trigger animation
                    continue;
                }
                if(sheep.skippedByTrenching && sheep.sheepType == SheepType.Trench){
                    sheep.skippedByTrenching = false;
                    return sheep;
                }
                return sheep;
        }
        return null;
    }
    bool SheepIsReadying(){
        foreach(SheepUnit sheep in throwableSheep){
            if(sheep.isReadying)
                return true;
        }
        return false;
    }
}
