using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGrassHandler : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.Input.KingAbilities.BuyGrass(), OnPlayerBuyGrass);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.KingAbilities.BuyGrass(), OnPlayerBuyGrass);
    }
    void OnPlayerBuyGrass(GameMessage msg) {
        PlayerProfile profile = PlayerProfileCoordinator.GetProfile(msg.owner);
        if (profile.GetGrass() > 90f)return;
        if (profile.Buy(PriceName.King.BuyGrass())) {
            profile.FillGrass();
            PriceCoordinator.IncreaseLevel(msg.owner, PriceName.King.BuyGrass());
        }
    }
}