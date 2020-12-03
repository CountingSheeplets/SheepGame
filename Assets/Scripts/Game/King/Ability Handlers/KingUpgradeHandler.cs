using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingUpgradeHandler : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.Input.KingAbilities.KingUpgrade(), OnKingUpgrade);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.KingAbilities.KingUpgrade(), OnKingUpgrade);
    }

    void OnKingUpgrade(GameMessage msg) {
        PlayerProfile profile = PlayerProfileCoordinator.GetProfile(msg.owner);
        if (profile.Buy(PriceName.King.Upgrade())) {
            int level = KingSpeedCoordinator.IncreaseLevel(msg.owner);
            PriceCoordinator.IncreaseLevel(msg.owner, PriceName.King.Upgrade());
            //Debug.Log("level: " + level);
            //EventCoordinator.TriggerEvent(EventName.System.Sheep.Spawned(), GameMessage.Write().WithSheepUnit(sheep));
        }
    }
}