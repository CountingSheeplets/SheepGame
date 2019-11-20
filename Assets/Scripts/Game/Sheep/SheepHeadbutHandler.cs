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
        foreach(KingUnit king in KingCoordinator.GetKings()){
            if(king == null)
                Debug.Log("king == null");
            if(msg.sheepUnit == null)
                Debug.Log("msg.sheepUnit == null");                
            float distance = (king.transform.position - msg.sheepUnit.transform.position).magnitude;
            if(distance < king.radius + hitRange){
                EventCoordinator.TriggerEvent(EventName.System.King.Hit(), GameMessage.Write().WithSheepUnit(msg.sheepUnit).WithKingUnit(king).WithOwner(msg.sheepUnit.owner));
            }
        }
    }
}
