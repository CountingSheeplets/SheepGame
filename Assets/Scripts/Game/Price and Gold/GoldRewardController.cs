﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldRewardController : MonoBehaviour {
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
            GoldRewardCoordinator.RewardOnFieldLand(msg.sheepUnit.owner);
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
    }
    void OnKingMissed(GameMessage msg) {
        GoldRewardCoordinator.ResetCombo(msg.owner);
    }
}