using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldFloat : BaseUnitMove
{
    public Playfield playfield;
    public void StartFloating(float speed, Vector2 _destination){
        if(!playfield)
            playfield = GetComponent<Playfield>();
        //Debug.Log("StartRunning");
        destination = _destination;
        playfield.isAnimating = true;
        //move the transform to destination
        MoveToDestination(speed, -0.5f);
        //run animation;
        //animator.FadeIn(destination, AnimatorContainer.Animation.Walk); //DragonBones container
        //animator.WalkTo(destination); // SpineContainer
    }

    public  override void PostMoveAction(){
        //Debug.Log("float eneded, stopping at:"+(Vector2)(transform.position));
        //animator.StopWalking();

        //trigger Land game event
        playfield.isAnimating = false;
        EventCoordinator.TriggerEvent(EventName.System.Environment.PlayfieldAnimated(), GameMessage.Write().WithPlayfield(playfield));
    }
}