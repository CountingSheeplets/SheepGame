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
        EventCoordinator.StartListening(EventName.System.Environment.ArenaAnimating(), OnArenaAnimating);
        EventCoordinator.StartListening(EventName.System.Environment.ArenaAnimated(), OnArenaAnimated);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StopListening(EventName.System.Environment.ArenaAnimating(), OnArenaAnimating);
        EventCoordinator.StopListening(EventName.System.Environment.ArenaAnimated(), OnArenaAnimated);
    }
    void OnStartGame(GameMessage msg){
        // Set a bit at position to 1.
        FlagsHelper.Set(ref currentState, GameState.started);
        //currentState |= GameState.started;
    }
    void OnArenaAnimated(GameMessage msg){
        // Set a bit at position to 0.
        FlagsHelper.Unset(ref currentState, GameState.arenaAnimating);
    }
    void OnArenaAnimating(GameMessage msg){
        FlagsHelper.Set(ref currentState, GameState.arenaAnimating);
        //currentState |= GameState.arenaAnimating;
    }
}
