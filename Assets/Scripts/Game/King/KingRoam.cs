using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingRoam : BaseUnitMove
{
    KingUnit king;
    void Awake()
    {
        king = GetComponent<KingUnit>();
        EventCoordinator.StartListening(EventName.System.Sheep.Roam(), OnRoam);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Sheep.Roam(), OnRoam);
    }
    public void StartWalking(float speed, Vector2 _destination){
        Debug.Log("King Roam...");
        destination = _destination;
        king.isRoaming = true;
        //walk animation;

        //move the transform to destination
        MoveToDestination(speed, 0f);
    }
    void OnRoam(GameMessage msg){
        if(!king.isRoaming && !king.isUsingAbility){
            float roll = Random.Range(0, 3f);//3x larger roll = 3x smaller probability
            if(roll < msg.floatMessage){
                Vector2 targetPosition = RoamTarget();
                if(targetPosition != Vector2.zero)
                    StartWalking(0.3f, targetPosition);
            }
        }
    }
    Vector2 RoamTarget(){
        Vector2 newVec = new Vector2(0, 1.5f * ArenaCoordinator.TileSize);
        newVec = Quaternion.AngleAxis(Random.Range(0, 359), Vector3.forward) * newVec;
        Vector2 targetPos = (Vector2)transform.position + newVec;
        if(king.myPlayfield.fieldCorners.IsWithinField(targetPos, king.radius)){
            return targetPos; 
        } else {
            targetPos = transform.position + Quaternion.AngleAxis(90, Vector3.forward) * newVec;
            if(king.myPlayfield.fieldCorners.IsWithinField(targetPos, king.radius)){
                return targetPos;
            } else {
                targetPos = transform.position + Quaternion.AngleAxis(180, Vector3.forward) * newVec;
                if(king.myPlayfield.fieldCorners.IsWithinField(targetPos, king.radius)){
                    return targetPos;
                }
            }
        }
        return Vector2.zero;
    }
    public override void PostMoveAction(){
        king.isRoaming = false;
        //trigger eat grass animation
        
    }
}
