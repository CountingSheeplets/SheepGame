using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArenaController : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StartListening(EventName.System.Player.Eliminated(), OnPlayerDefeated);
        EventCoordinator.StartListening(EventName.System.Environment.EndMatch(), OnMatchEnd);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StopListening(EventName.System.Player.Eliminated(), OnPlayerDefeated);
        EventCoordinator.StopListening(EventName.System.Environment.EndMatch(), OnMatchEnd);
    }

    void OnStartGame(GameMessage msg){
       ArenaCoordinator.RearrangeArena();
    }
    void OnPlayerLeft(GameMessage msg){
        //destroy a playfield if game has not started yet, if started, leave?
        if((GameStateView.GetGameState() & GameState.started) == 0)
            ArenaCoordinator.RemoveField(msg.owner);
    }
    void OnPlayerDefeated(GameMessage msg){
        ArenaCoordinator.RemoveField(msg.targetOwner);
        ArenaCoordinator.RearrangeArena();
    }

    void OnPlayerJoined(GameMessage msg){
        ArenaCoordinator.GetOrCreateField(msg.owner);
    }

    void OnMatchEnd(GameMessage msg){
        ArenaCoordinator.RemoveField(msg.owner);
        ArenaCoordinator.RearrangeArena();
    }
}
