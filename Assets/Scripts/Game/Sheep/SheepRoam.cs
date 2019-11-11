using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepRoam : BaseUnitMove
{
    SheepUnit sheep;
    void Start()
    {
        sheep = GetComponent<SheepUnit>();
        EventManager.StartListening(EventName.System.Sheep.Roam(), OnRoam);
    }
    void OnDestroy(){
        EventManager.StopListening(EventName.System.Sheep.Roam(), OnRoam);
    }
    public void StartWalking(float speed, Vector2 _destination){
        Debug.Log("Roam");
        destination = _destination;
        sheep.isRoaming = true;
        //walk animation;

        //move the transform to destination
        MoveToDestination(speed, 0f);
    }
    void OnRoam(GameMessage msg){
        if(!sheep.isSwimming && !sheep.isReadying && !sheep.isReadyToFly && !sheep.isFlying){
            float roll = Random.Range(0, 1f);
            if(roll < msg.floatMessage){
                Vector2 targetPosition = RoamTarget();
                if(targetPosition != Vector2.zero)
                    StartWalking(0.3f, targetPosition);
            }
        }
    }
    Vector2 RoamTarget(){
        Vector2 newVec = new Vector2(0, 1.5f * ArenaManager.TileSize);
        newVec = Quaternion.AngleAxis(Random.Range(0, 359), Vector3.forward) * newVec;
        Vector2 targetPos = (Vector2)transform.position+newVec;
        if(sheep.currentPlayfield.fieldCorners.IsWithinField(targetPos, sheep.radius)){
            Debug.Log("taget vector ok, roaming to:"+newVec);
            return targetPos; 
        } else {
            targetPos = transform.position + Quaternion.AngleAxis(90, Vector3.forward) * newVec;
            if(sheep.currentPlayfield.fieldCorners.IsWithinField(targetPos, sheep.radius)){
                Debug.Log("turn once, roaming to:"+newVec);
                return targetPos;
            } else {
                targetPos = transform.position + Quaternion.AngleAxis(90, Vector3.forward) * newVec;
                if(sheep.currentPlayfield.fieldCorners.IsWithinField(targetPos, sheep.radius)){
                    Debug.Log("turn twice, roaming to:"+newVec);
                    return targetPos;
                }
            }
        }
        return Vector2.zero;
    }
    public override void PostMoveAction(){
        sheep.isRoaming = false;
        //trigger eat grass animation
        
    }
}
