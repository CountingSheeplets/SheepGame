using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCanvasController : MonoBehaviour
{
    public Animator animator;
    public bool gameStarted = false;

    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventCoordinator.StartListening(EventName.System.Environment.EndMatch(), OnMatchEnd);
    }
    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventCoordinator.StopListening(EventName.System.Environment.EndMatch(), OnMatchEnd);
    }

    void OnPlayerJoined(GameMessage msg){
        CardCanvasCoordinator.CreateCard(msg.owner);
    }
    void OnPlayerLeft(GameMessage msg){
        if(!gameStarted){
            CardCanvasCoordinator.RemovePlayerCard(msg.owner);
        }
    }
    void OnStartGame(GameMessage msg){
        gameStarted = true;
        animator.SetTrigger("started");
    }
    void OnMatchEnd(GameMessage msg){
        gameStarted = false;
        animator.SetTrigger("ended");  
    }
}
