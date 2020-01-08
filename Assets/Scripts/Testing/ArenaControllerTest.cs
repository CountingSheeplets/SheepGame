using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaControllerTest : MonoBehaviour
{
    void Update(){
/*         if((GameStateView.GetGameState() & GameState.arenaAnimating) != 0)
            Debug.Log("is animating..."); */

        if(Input.GetKeyDown(KeyCode.KeypadPlus)){
            Owner owner = OwnersCoordinator.CreateEmptyOwner();
            if(owner){
                EventCoordinator.TriggerEvent(EventName.Input.Network.PlayerJoined(), GameMessage.Write().WithOwner(owner));
                KingFactory.TryCreateHeroModel(owner);
            }
        }
        if(Input.GetKeyDown(KeyCode.KeypadMinus)){
            Owner owner = OwnersCoordinator.GetRandomOwner();
            if(owner){
                EventCoordinator.TriggerEvent(EventName.Input.Network.PlayerLeft(), GameMessage.Write().WithOwner(owner));
            }
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter)){
            EventCoordinator.TriggerEvent(EventName.Input.StartGame(), GameMessage.Write());
        }
    }
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
        ///remove field, animate its destruction;
        ArenaCoordinator.RemoveField(msg.owner);
        //drown all sheep which are swimming intantly:

        //wait for a delay, untill animations end (1f?) then Trigger a Rearrangement:
        ArenaCoordinator.RearrangeArena();
    }
}
