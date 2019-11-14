using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHeadbutHandler : MonoBehaviour
{
    public List<KingUnit> kings = new List<KingUnit>();
    public float hitRange = 0.2f;

    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Sheep.Land(), OnLand);
        EventCoordinator.StartListening(EventName.System.King.Spawned(), OnSpawn);
        EventCoordinator.StartListening(EventName.System.King.Killed(), OnKill);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Sheep.Land(), OnLand);
        EventCoordinator.StopListening(EventName.System.King.Spawned(), OnSpawn);
        EventCoordinator.StopListening(EventName.System.King.Killed(), OnKill);
    }
    void OnSpawn(GameMessage msg){
        kings.Add(msg.kingUnit);
    }
    void OnKill(GameMessage msg){
        kings.Remove(msg.kingUnit);
    }
    void OnLand(GameMessage msg){
        foreach(KingUnit king in kings){
            if(king == null)
                Debug.Log("king == null:"+kings.Count);
            if(msg.sheepUnit == null)
                Debug.Log("msg.sheepUnit == null");                
            float distance = (king.transform.position - msg.sheepUnit.transform.position).magnitude;
            if(distance < king.radius + hitRange){
                EventCoordinator.TriggerEvent(EventName.System.King.Hit(), GameMessage.Write().WithSheepUnit(msg.sheepUnit).WithKingUnit(king).WithOwner(msg.sheepUnit.owner));
            }
        }
    }
}
