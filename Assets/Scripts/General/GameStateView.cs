using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateView : Singleton<GameStateView>
{
    [BitMask(typeof(GameState))]
    public GameState currentState;
    public static GameState GetGameState(){return Instance.currentState;}
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnStartGame(GameMessage msg){
        // Set a bit at position to 1.
        currentState |= GameState.started;
    }

        // Set a bit at position to 0.
        //return value & ~(1 << position);
}
