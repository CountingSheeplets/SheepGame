using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this has to become a unique, to be owner/indifferent!
//just operate with owner object which came through message!
public class SheepController : MonoBehaviour //this is a SheepFactory + SheepController
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.KingAbilities.SpawnSheep(), OnSheepSpawn);
    }

    void OnSheepSpawn(GameMessage msg){
        //if there's money in player profile....
        
        // then do stuff:
        SheepUnit sheep = SheepCoordinator.SpawnSheep(msg.owner);
        EventCoordinator.TriggerEvent(EventName.System.Sheep.Spawned(), GameMessage.Write().WithSheepUnit(sheep));
    }
}
