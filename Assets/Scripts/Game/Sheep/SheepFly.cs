using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepFly : BaseUnitMove
{
    SheepUnit sheep;
    public void StartFlying(float speed, Vector2 _destination){
        if(!sheep)
            sheep = GetComponent<SheepUnit>();
        Debug.Log("StartFlying to:"+_destination);
        sheep.isFlying = true;
        sheep.isReadyToFly = false;
        destination = _destination;
        //run animation;

        MoveToDestination(speed, 2f);
 }

    public  override void PostMoveAction(){
        GetComponent<SheepUnit>().isFlying = false;
        //trigger to play Land animation
        Debug.Log("fly eneded, landing at:"+(Vector2)(transform.position));

        //trigger Land game event, listened by sheep throw
        Playfield newPlayfield = ArenaCoordinator.GetPlayfield(transform.position);
        GetComponent<SheepUnit>().currentPlayfield = newPlayfield;
        EventCoordinator.TriggerEvent(EventName.System.Sheep.Land(), GameMessage.Write().WithSheepUnit(GetComponent<SheepUnit>()).WithPlayfield(newPlayfield));
    }
}
