using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCanvasController : MonoBehaviour {
    public Animator animator;

    void Awake() {
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StartListening(EventName.System.Environment.EndMatch(), OnMatchEnd);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StopListening(EventName.System.Environment.EndMatch(), OnMatchEnd);
    }

    void OnPlayerJoined(GameMessage msg) {
        CardCanvasCoordinator.CreateCard(msg.owner);
    }
    void OnPlayerLeft(GameMessage msg) {
        if (!GameStateView.HasState(GameState.started) && !GameStateView.HasState(GameState.ended)) {
            CardCanvasCoordinator.RemovePlayerCard(msg.owner);
        }
    }
    void OnStartGame(GameMessage msg) {
        animator.SetTrigger("started");
    }
    void OnMatchEnd(GameMessage msg) {
        animator.SetTrigger("ended");
    }
}