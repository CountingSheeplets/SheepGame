using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this has to become a unique, to be owner/indifferent!
//just operate with owner object which came through message!
public class BuySheepHandler : MonoBehaviour //this is a SheepFactory + SheepController
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.KingAbilities.SpawnSheep(), OnSheepSpawn);
    }

    void OnSheepSpawn(GameMessage msg){
        //if there's money in player profile.... buy sheep;
        float price = PriceCoordinator.GetPrice(msg.owner, PriceName.King.BuySheep());
        PlayerProfile profile = PlayerProfileCoordinator.GetProfile(msg.owner);
        if(price < profile.GetMoney()){
            profile.AddMoney(-price);
            SheepUnit sheep = SheepCoordinator.SpawnSheep(msg.owner);
            EventCoordinator.TriggerEvent(EventName.System.Sheep.Spawned(), GameMessage.Write().WithSheepUnit(sheep));
            Debug.Log("Sheeeep bought!");
        } else {
            //Send Msg that not enough
            Debug.Log("Cannto buy sheep, not anough money");
        }
    }
}
