using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuySheepHandler : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.KingAbilities.SpawnSheep(), OnSheepSpawn);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.Input.KingAbilities.SpawnSheep(), OnSheepSpawn);
    }

    void OnSheepSpawn(GameMessage msg){
        PlayerProfile profile = PlayerProfileCoordinator.GetProfile(msg.owner);
        if(profile.Buy(PriceName.King.BuySheep())){
            SheepUnit sheep = SheepCoordinator.SpawnSheep(msg.owner);
            EventCoordinator.TriggerEvent(EventName.System.Sheep.Spawned(), GameMessage.Write().WithSheepUnit(sheep));
        }
    }
}
