using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArenaController : MonoBehaviour
{
    bool gameStarted = false;

    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StartListening(EventName.System.Player.Defeated(), OnPlayerDefeated);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StopListening(EventName.System.Player.Defeated(), OnPlayerDefeated);
    }

    void OnStartGame(GameMessage msg){
        gameStarted = true;
       ArenaCoordinator.RearrangeArena();
    }
    void OnPlayerLeft(GameMessage msg){
        //destroy a playfield if game has not started yet, if started, leave?
        if(!gameStarted){
            ArenaCoordinator.RemoveField(msg.owner);
        }
    }
    void OnPlayerDefeated(GameMessage msg){
        ArenaCoordinator.RemoveField(msg.owner);
        ArenaCoordinator.RearrangeArena();

        if(PlayerProfileCoordinator.GetAliveTeamCount() == 1)
            EventCoordinator.TriggerEvent(EventName.System.Player.Victorious(), GameMessage.Write());
    }

    void OnPlayerJoined(GameMessage msg){
        ArenaCoordinator.GetOrCreateField(msg.owner);
    }
}
