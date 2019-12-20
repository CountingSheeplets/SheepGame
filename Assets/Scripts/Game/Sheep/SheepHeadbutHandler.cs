using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHeadbutHandler : MonoBehaviour
{
    public float hitRange = 0.2f;

    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Sheep.Land(), OnLand);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Sheep.Land(), OnLand);
    }

    void OnLand(GameMessage msg){
        KingUnit king = KingCoordinator.GetKing(msg.playfield);
        if(king == null)
            Debug.Log("king == null");
        if(msg.sheepUnit == null)
            Debug.Log("msg.sheepUnit == null");                
        float distance = (king.transform.position - msg.sheepUnit.transform.position).magnitude;
        if(distance < king.radius + hitRange){
            EventCoordinator.TriggerEvent(EventName.System.King.Hit(), GameMessage.Write().WithSheepUnit(msg.sheepUnit).WithKingUnit(king).WithOwner(msg.sheepUnit.owner));
        }
        if(msg.playfield != msg.sheepUnit.owner.GetPlayfield())
            if(msg.sheepUnit.sheepType == SheepType.Armored){
                SheepFly fly = msg.sheepUnit.GetComponent<SheepFly>();
                fly.StartFlying(SpeedBucket.GetFlySpeed(msg.sheepUnit.sheepType), king.transform.position);

                //play bounce sound fx, bounce animation etc.:

                //play
            }
    }
}
