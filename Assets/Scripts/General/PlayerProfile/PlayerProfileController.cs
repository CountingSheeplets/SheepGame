﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfileController : MonoBehaviour {
    void Awake() {
        EventCoordinator.StartListening(EventName.System.Environment.Initialized(), OnInitialized);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventCoordinator.StartListening(EventName.System.Player.Eliminated(), OnPlayerDefeated);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Environment.Initialized(), OnInitialized);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventCoordinator.StopListening(EventName.System.Player.Eliminated(), OnPlayerDefeated);
    }

    void OnInitialized(GameMessage msg) {
        foreach (Owner owner in OwnersCoordinator.GetOwners())
            owner.GetPlayerProfile().Modify(owner.GetKing(), owner.GetPlayfield());
    }

    void OnPlayerJoined(GameMessage msg) {
        PlayerProfile profile = PlayerProfileCoordinator.GetProfile(msg.owner);
        if (profile == null) {
            profile = PlayerProfileCoordinator.AddProfile(msg.owner);
            profile.AddMoney(ConstantsBucket.StartingGold);
            EventCoordinator.TriggerEvent(EventName.System.Player.ProfileCreated(), GameMessage.Write().WithPlayerProfile(profile));
        }
        //send assigned data
        //NetworkCoordinator.SendColor(msg.owner.deviceId, "#" + ColorUtility.ToHtmlStringRGBA(profile.playerColor).Substring(0, 6));
        //NetworkCoordinator.SendName(msg.owner.deviceId, profile.owner.ownerName);
    }

    void OnPlayerLeft(GameMessage msg) {
        if (!GameStateView.HasState(GameState.started))
            PlayerProfileCoordinator.RemoveProfile(msg.owner);
    }
    void OnPlayerDefeated(GameMessage msg) {
        msg.targetOwner.GetPlayerProfile().isAlive = false;
    }
}