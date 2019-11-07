using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepThrow : MonoBehaviour
{
    public List<SheepUnit> sheeps = new List<SheepUnit>();
    public float throwStrength;
    public float flySpeed = 1f;
    void Start()
    {
        EventManager.StartListening(EventName.Input.Swipe(), OnSwipe);
        EventManager.StartListening(EventName.System.Sheep.Spawned(), OnSpawn);
        EventManager.StartListening(EventName.System.Sheep.Land(), OnLand);
    }

    void OnSwipe(GameMessage msg)
    {
        if(msg.owner.EqualsByValue(GetComponent<Owner>())){
            SheepUnit sheepReadyToBeThrown = GetNextSheep();
            if(sheepReadyToBeThrown){
                SheepFly fly = sheepReadyToBeThrown.gameObject.GetComponent<SheepFly>();
                Debug.Log(msg.swipe);
                fly.destination = msg.swipe.vector * msg.swipe.distance * throwStrength / 10f + (Vector2)fly.transform.position;
                Debug.Log("fly.destination:"+fly.destination);
                sheeps.Remove(sheepReadyToBeThrown);
                fly.StartFlying(flySpeed);
            } else {
                //show animation/sign that no sheep ready to be thrown
                Debug.Log("no sheep could be found that is ready to be thrown!");
            }
        }
    }
    void OnSpawn(GameMessage msg){
        if(msg.sheepUnit)
            if(msg.sheepUnit.owner == GetComponent<Owner>())
                sheeps.Add(msg.sheepUnit);
    }
    void OnLand(GameMessage msg){
        if(msg.playfield)
            if(msg.playfield == GetComponentInParent<Playfield>())
                sheeps.Add(msg.sheepUnit);
    }   
    SheepUnit GetNextSheep(){
        foreach(SheepUnit sheep in sheeps){
            if(sheep.canBeThrown)
                return sheep;
        }
        return null;
    }
}
