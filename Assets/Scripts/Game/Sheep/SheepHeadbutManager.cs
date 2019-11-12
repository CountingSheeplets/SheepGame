using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHeadbutManager : MonoBehaviour
{
    public List<KingUnit> kings = new List<KingUnit>();
    public float hitRange = 0.2f;

    void Start()
    {
        EventManager.StartListening(EventName.System.Sheep.Land(), OnLand);
        EventManager.StartListening(EventName.System.King.Spawned(), OnSpawn);
        EventManager.StartListening(EventName.System.King.Killed(), OnKill);
    }
    void OnDestroy(){
        EventManager.StopListening(EventName.System.Sheep.Land(), OnLand);
        EventManager.StopListening(EventName.System.King.Spawned(), OnSpawn);
        EventManager.StopListening(EventName.System.King.Killed(), OnKill);
    }
    void OnSpawn(GameMessage msg){
        kings.Add(msg.kingUnit);
    }
    void OnKill(GameMessage msg){
        kings.Remove(msg.kingUnit);
    }
    void OnLand(GameMessage msg){
        foreach(KingUnit king in kings){
            float distance = (king.transform.position - msg.sheepUnit.transform.position).magnitude;
            if(distance < king.radius + hitRange){
                EventManager.TriggerEvent(EventName.System.King.Hit(), GameMessage.Write().WithSheepUnit(msg.sheepUnit).WithKingUnit(king).WithOwner(msg.sheepUnit.owner));
            }
        }
    }
}
