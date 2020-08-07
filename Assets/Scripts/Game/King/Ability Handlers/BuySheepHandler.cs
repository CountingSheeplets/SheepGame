using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuySheepHandler : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.Input.KingAbilities.SpawnSheep(), OnSheepSpawn);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.KingAbilities.SpawnSheep(), OnSheepSpawn);
    }

    void OnSheepSpawn(GameMessage msg) {
        PlayerProfile profile = PlayerProfileCoordinator.GetProfile(msg.owner);
        if (profile.Buy(PriceName.King.BuySheep())) {
            int level = SheepCoordinator.IncreaseSpawnRateLevel(msg.owner);
            PriceCoordinator.IncreaseLevel(msg.owner, PriceName.King.BuySheep());
            //Debug.Log("level: " + level);
            //EventCoordinator.TriggerEvent(EventName.System.Sheep.Spawned(), GameMessage.Write().WithSheepUnit(sheep));
        }
    }
}