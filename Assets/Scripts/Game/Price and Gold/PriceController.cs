using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceController : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Sheep.Spawned(), OnSheepSpawn);
        EventCoordinator.StartListening(EventName.System.Sheep.Kill(), OnSheepKill);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Sheep.Spawned(), OnSheepSpawn);
        EventCoordinator.StopListening(EventName.System.Sheep.Kill(), OnSheepKill);
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnSheepSpawn(GameMessage msg){
        PriceCoordinator.IncreaseLevel(msg.sheepUnit.owner, PriceName.King.BuySheep());
    }
    void OnSheepKill(GameMessage msg){
        PriceCoordinator.DecreaseLevel(msg.sheepUnit.owner, PriceName.King.BuySheep());
    }
    void OnStartGame(GameMessage msg){
        List<Owner> owners = OwnersCoordinator.GetOwners();
        foreach(Owner owner in owners)
            PriceCoordinator.AddPriceAttribute(owner);
    }
}
