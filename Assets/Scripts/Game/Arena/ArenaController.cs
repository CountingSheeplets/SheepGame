using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArenaController : MonoBehaviour {
    public bool testScene = false;
    void Update() {
        if (!EventCoordinator.Instance.enableDebugging)return;
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            if (GameStateView.HasState(GameState.started)) {
                Debug.LogWarning("New cannot join, game already started");
                return;
            }
            Owner owner = OwnersCoordinator.CreateEmptyOwner();
            if (owner) {
                EventCoordinator.TriggerEvent(EventName.Input.Network.PlayerJoined(), GameMessage.Write().WithOwner(owner));

            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            Owner owner = OwnersCoordinator.GetRandomOwner();
            OwnersCoordinator.DisconnectOwner(owner.deviceId);
            if (!owner)
                Debug.LogWarning("OnDisconnect returned null Owner! Nothing to disconnect...");
            EventCoordinator.TriggerEvent(EventName.Input.Network.PlayerLeft(), GameMessage.Write().WithOwner(owner));
            foreach (Owner stayingOwner in OwnersCoordinator.GetOwners()) {
                stayingOwner.ready = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            if (!GameStateView.HasState(GameState.started))
                EventCoordinator.TriggerEvent(EventName.Input.StartGame(), GameMessage.Write());
        }
    }
    void Awake() {
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StartListening(EventName.System.Environment.ArenaDestroyed(), OnArenaDestroyed);
        EventCoordinator.StartListening(EventName.System.Environment.EndMatch(), OnMatchEnd);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StopListening(EventName.System.Environment.ArenaDestroyed(), OnArenaDestroyed);
        EventCoordinator.StopListening(EventName.System.Environment.EndMatch(), OnMatchEnd);
    }

    void OnStartGame(GameMessage msg) {
        ArenaCoordinator.RearrangeArena(false);
    }
    void OnPlayerLeft(GameMessage msg) {
        if (!GameStateView.HasState(GameState.started))
            ArenaCoordinator.RemoveField(msg.owner);
    }
    void OnArenaDestroyed(GameMessage msg) {
        foreach (Owner owner in msg.targetOwners) {
            ArenaCoordinator.RemoveField(owner);
        }
        if (ArenaCoordinator.GetPlayfields.Count > 1)
            ArenaCoordinator.RearrangeArena(true);
    }

    void OnPlayerJoined(GameMessage msg) {
        if (!GameStateView.HasState(GameState.started))
            ArenaCoordinator.GetOrCreateField(msg.owner);
    }

    void OnMatchEnd(GameMessage msg) {
        //ArenaCoordinator.RemoveField(msg.owner);
        //ArenaCoordinator.RearrangeArena(true);
    }
}