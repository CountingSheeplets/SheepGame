using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldRewardController : MonoBehaviour {
    float counter = 0;
    void Start() {
        EventCoordinator.StartListening(EventName.System.Sheep.Land(), OnSheepLand);
        EventCoordinator.StartListening(EventName.System.Sheep.Launch(), OnSheepLaunch);
        EventCoordinator.StartListening(EventName.System.King.Hit(), OnKingHit);
        EventCoordinator.StartListening(EventName.System.Sheep.KingMissed(), OnKingMissed);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Land(), OnSheepLand);
        EventCoordinator.StopListening(EventName.System.Sheep.Launch(), OnSheepLaunch);
        EventCoordinator.StopListening(EventName.System.King.Hit(), OnKingHit);
        EventCoordinator.StopListening(EventName.System.Sheep.KingMissed(), OnKingMissed);
    }

    void OnSheepLand(GameMessage msg) {
        if (msg.playfield != null) {
            if (msg.sheepUnit.lastHandler != msg.playfield.owner)
                GoldRewardCoordinator.RewardOnFieldLand(msg.sheepUnit.owner);
            else {
                //reward for kicking sheep here:
                //...
            }
        }
    }
    void OnSheepLaunch(GameMessage msg) {
        bool isGreedy = (msg.sheepUnit.sheepType == SheepType.Greedy);
        if (msg.sheepUnit.owner == msg.playfield.owner) {
            GoldRewardCoordinator.RewardOnSelfThrow(msg.sheepUnit.owner, isGreedy);
        } else {
            GoldRewardCoordinator.RewardOnOtherThrow(msg.sheepUnit.owner, isGreedy);
        }
    }
    void OnKingHit(GameMessage msg) {
        GoldRewardCoordinator.RewardOnKingKingHit(msg.owner);
        EventCoordinator.TriggerEvent(EventName.System.Economy.ComboChanged(), GameMessage.Write().WithIntMessage(GoldRewardCoordinator.GetComboMultiplier(msg.owner)).WithOwner(msg.owner));
    }
    void OnKingMissed(GameMessage msg) {
        GoldRewardCoordinator.ResetCombo(msg.owner);
    }
    void Update() {
        counter += Time.deltaTime;
        if (counter > ConstantsBucket.GoldIncomePeriod) {
            counter = 0;
            foreach (Owner owner in OwnersCoordinator.GetOwners()) {
                GoldRewardCoordinator.RewardGold(owner, ConstantsBucket.BaseGoldIncome);
            }
        }
    }
}