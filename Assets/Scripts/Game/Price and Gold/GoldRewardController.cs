using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldRewardController : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.System.Sheep.Land(), OnSheepLand);
        EventCoordinator.StartListening(EventName.System.King.Hit(), OnKingHit);
        EventCoordinator.StartListening(EventName.System.Sheep.KingMissed(), OnKingMissed);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Land(), OnSheepLand);
        EventCoordinator.StopListening(EventName.System.King.Hit(), OnKingHit);
        EventCoordinator.StopListening(EventName.System.Sheep.KingMissed(), OnKingMissed);
    }

    void OnSheepLand(GameMessage msg) {
        if (msg.sheepUnit.lastHandler != msg.sheepUnit.owner)
            if (msg.playfield != null) {
                GoldRewardCoordinator.RewardOnFieldLand(msg.sheepUnit.owner, msg.sheepUnit.transform);
            } else {
                GoldRewardCoordinator.RewardOnSheepElimination(msg.sheepUnit.lastHandler, msg.sheepUnit.transform);
            }
    }
    void OnSheepLaunch(GameMessage msg) {
        bool isGreedy = (msg.sheepUnit.sheepType == SheepType.Greedy);
        if (msg.sheepUnit.owner == msg.playfield.owner) {
            GoldRewardCoordinator.RewardOnSelfThrow(msg.sheepUnit.owner, msg.sheepUnit.transform);
        } else {
            GoldRewardCoordinator.RewardOnOtherThrow(msg.sheepUnit.owner, msg.sheepUnit.transform);
        }
    }
    void OnKingHit(GameMessage msg) {
        GoldRewardCoordinator.RewardOnKingKingHit(msg.owner, msg.sheepUnit.transform);
        EventCoordinator.TriggerEvent(EventName.System.Economy.ComboChanged(), GameMessage.Write().WithIntMessage(GoldRewardCoordinator.GetComboMultiplier(msg.owner)).WithOwner(msg.owner));
    }
    void OnKingMissed(GameMessage msg) {
        GoldRewardCoordinator.ResetCombo(msg.owner);
        EventCoordinator.TriggerEvent(EventName.System.Economy.ComboChanged(), GameMessage.Write().WithIntMessage(GoldRewardCoordinator.GetComboMultiplier(msg.owner)).WithOwner(msg.owner));
    }

}