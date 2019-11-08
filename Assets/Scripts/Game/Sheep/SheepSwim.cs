using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSwim : BaseUnitMove
{
    public void StartSwiming(float speed, Vector2 _destination){
        Debug.Log("StartRunning");
        destination = _destination;
        GetComponent<SheepUnit>().isSwimming = true;
        //swim animation;

        //move the transform to destination
        MoveToDestination(speed, 0.25f);
    }

    public  override void PostMoveAction(){
        //trigger ready to jump/launch animation
        Debug.Log("run swimming, drowning at:"+(Vector2)(transform.position));

        GetComponent<SheepUnit>().isSwimming = false;
        Playfield playfield = GetComponent<SheepUnit>().currentPlayfield;
        EventManager.TriggerEvent(EventName.System.Sheep.Kill(), GameMessage.Write().WithSheepUnit(GetComponent<SheepUnit>()));
    }
}
