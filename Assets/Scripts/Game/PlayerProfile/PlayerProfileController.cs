using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfileController : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
    }

    void OnStartGame(GameMessage msg){
        //On start game - Modify all profiles to include King and Playfield

    }

    void OnPlayerJoint(GameMessage msg){
        //here create profiles initially:
        PlayerProfile profile = new PlayerProfile().Create(msg.owner);
        //profileCoordinator.AddProfile(profile);
    }
}
