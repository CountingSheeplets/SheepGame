using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SheepSwim : BaseUnitMove
{
    void Start()
    {
        EventManager.StartListening(EventName.System.Sheep.Land(), OnLand);
    }
    void OnDestroy(){
        EventManager.StopListening(EventName.System.Sheep.Land(), OnLand);
    }
    public void StartSwiming(float speed, Vector2 _destination){
        Debug.Log("StartSwiming");
        destination = _destination;
        GetComponent<SheepUnit>().isSwimming = true;
        //swim animation;

        //move the transform to destination
        MoveToDestination(speed, 0.25f);
    }
    void OnLand(GameMessage msg){
        Debug.Log("OnLand:StartSwiming:"+gameObject.name);
        if(msg.sheepUnit == GetComponent<SheepUnit>()){
            if(msg.playfield == null){
                Transform nearestVortex = FindObjectsOfType<Vortex>().Select(x=>x.transform).ToList().FindNearest(transform);
                GetComponent<SheepSwim>().StartSwiming(0.25f, nearestVortex.position);
            }
        }
    }
    public override void PostMoveAction(){
        //trigger ready to jump/launch animation
        Debug.Log("post swimming, drowning at:"+(Vector2)(transform.position));

        //GetComponent<SheepUnit>().isSwimming = false; //uncomment this, if unit would not die, but do smth else
        Playfield playfield = GetComponent<SheepUnit>().currentPlayfield;
        EventManager.TriggerEvent(EventName.System.Sheep.Kill(), GameMessage.Write().WithSheepUnit(GetComponent<SheepUnit>()));
    }
}
