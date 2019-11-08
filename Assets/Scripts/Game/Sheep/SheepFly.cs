using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepFly : BaseUnitMove
{

    public void StartFlying(float speed, Vector2 _destination){
        Debug.Log("StartFlying to:"+destination);
        destination = _destination;
        //run animation;

        MoveToDestination(speed, 2f);
 }

    public  override void PostMoveAction(){
        //trigger to play Land animation
        Debug.Log("fly eneded, landing at:"+(Vector2)(transform.position));

        //trigger Land game event, listened by sheep throw
        Playfield newPlayfield = ArenaManager.GetPlayfieldForPoint(transform.position);
        GetComponent<SheepUnit>().currentPlayfield = newPlayfield;
        EventManager.TriggerEvent(EventName.System.Sheep.Land(), GameMessage.Write().WithSheepUnit(GetComponent<SheepUnit>()).WithPlayfield(newPlayfield));
    }
}
