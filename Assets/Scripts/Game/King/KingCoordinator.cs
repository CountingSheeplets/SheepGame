using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingCoordinator : Singleton<KingCoordinator>
{
    public List<KingUnit> kings = new List<KingUnit>();

    void Start()
    {
        EventCoordinator.StartListening(EventName.System.King.Spawned(), OnSpawn);
        EventCoordinator.StartListening(EventName.System.King.Killed(), OnKill);
        
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.King.Spawned(), OnSpawn);
        EventCoordinator.StopListening(EventName.System.King.Killed(), OnKill);
    }

    void OnSpawn(GameMessage msg){
        kings.Add(msg.kingUnit);
    }
    void OnKill(GameMessage msg){
        kings.Remove(msg.kingUnit);
    }
    public static KingUnit GetKing(Owner owner){
        foreach(KingUnit king in Instance.kings){
            if(king.owner == owner)
                return king;
        }
        return null;
    }
    public static List<KingUnit> GetKings(){
        return Instance.kings;
    }
}
