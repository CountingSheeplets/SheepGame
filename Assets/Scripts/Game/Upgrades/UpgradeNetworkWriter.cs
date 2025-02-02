using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class UpgradeNetworkWriter : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
        EventCoordinator.StartListening(EventName.System.Sheep.Launch(), OnLaunch);
        EventCoordinator.StartListening(EventName.System.Sheep.Upgraded(), OnUpgraded);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
        EventCoordinator.StopListening(EventName.System.Sheep.Launch(), OnLaunch);
        EventCoordinator.StopListening(EventName.System.Sheep.Upgraded(), OnUpgraded);
    }
    void OnUpgraded(GameMessage msg) {
        NetworkCoordinator.SendUpgradeButtons(msg.sheepUnit.owner.deviceId, msg.sheepUnit);
        NetworkCoordinator.SendUpgradeIcon(msg.sheepUnit.owner.deviceId, UpgradeBucket.GetCurrentUpgrade(msg.sheepUnit));
    }
    void OnReadyToLaunch(GameMessage msg) {
        if (msg.sheepUnit.owner == msg.playfield.owner) {
            NetworkCoordinator.SendUpgradeButtons(msg.sheepUnit.owner.deviceId, msg.sheepUnit);
            NetworkCoordinator.SendUpgradeIcon(msg.sheepUnit.owner.deviceId, UpgradeBucket.GetCurrentUpgrade(msg.sheepUnit));
        }
    }
    void OnPlayerJoined(GameMessage msg) {
        if (!GameStateView.HasState(GameState.started) || !msg.owner.GetPlayerProfile().isAlive)
            return;
        Playfield pl = msg.owner.GetPlayfield();
        SheepUnit sheep = pl.GetComponent<SheepThrow>().sheepReadyToBeThrown;

        NetworkCoordinator.SendUpgradeButtons(sheep.owner.deviceId, sheep);
        NetworkCoordinator.SendUpgradeIcon(sheep.owner.deviceId, UpgradeBucket.GetCurrentUpgrade(sheep));

    }
    void OnLaunch(GameMessage msg) {
        NetworkCoordinator.SendUpgradeButtons(msg.sheepUnit.owner.deviceId, null);
        NetworkCoordinator.SendUpgradeIcon(msg.sheepUnit.owner.deviceId, null);
    }
}*/