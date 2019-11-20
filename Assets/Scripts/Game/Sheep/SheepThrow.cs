using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepThrow : MonoBehaviour
{
    public List<SheepUnit> sheeps = new List<SheepUnit>();
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
                sheeps.Remove(sheepReadyToBeThrown);
                fly.StartFlying(flySpeed, destination);
                sheepReadyToBeThrown = null;
            } else {
                //show animation/sign that no sheep ready to be thrown
                Debug.Log("no sheep could be found that is ready to be thrown!");
            }
            TryReadyNewSheep();
        }
    }
    void OnSpawn(GameMessage msg){
        if(msg.sheepUnit)
            if(msg.sheepUnit.owner == GetComponent<Owner>()){
                sheeps.Add(msg.sheepUnit);
                TryReadyNewSheep();
            }
    }
    void OnKill(GameMessage msg){
        if(msg.sheepUnit && sheeps.Contains(msg.sheepUnit)){
            sheeps.Remove(msg.sheepUnit);
        }
    }
    void OnLand(GameMessage msg){
        if(msg.playfield == GetComponentInParent<Playfield>()){
            Debug.Log(msg.playfield+"-adding new sheep on Land:"+msg.sheepUnit.gameObject.name);
            sheeps.Add(msg.sheepUnit);
            TryReadyNewSheep();
        }
    }
    void OnReady(GameMessage msg){
        if(msg.sheepUnit != null){
            if(msg.sheepUnit.currentPlayfield == GetComponentInParent<Playfield>()){
                sheepReadyToBeThrown = msg.sheepUnit;
                sheepReadyToBeThrown.isReadyToFly = true;
            }
        }
    } 
    void TryReadyNewSheep(){
        Debug.Log("TryReadyNewSheep");
        if(!SheepIsReadying() && sheepReadyToBeThrown == null){
            SheepUnit availableSheep = GetNextSheep();
            Debug.Log("GetNextSheep:"+availableSheep);
            if(availableSheep != null){
                Debug.Log("availableSheep.currentPlayfield:"+availableSheep.currentPlayfield);
                if(availableSheep.currentPlayfield != null){
                    availableSheep.GetComponent<SheepRun>().StartRunning(1f, availableSheep.currentPlayfield.fieldCorners.Center);
                }
            } else
                Debug.Log("cant run, non null");
        } else {
            Debug.Log("IsReadying or already ready...");
        }
    }
    SheepUnit GetNextSheep(){
        foreach(SheepUnit sheep in sheeps){
            if(sheep.canBeThrown)
                return sheep;
        }
        return null;
    }
    bool SheepIsReadying(){
        foreach(SheepUnit sheep in sheeps){
            if(sheep.isReadying)
                return true;
        }
        return false;
    }
}
