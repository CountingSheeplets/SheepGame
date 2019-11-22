using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfileController : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Environment.Initialized(), OnInitialized);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Environment.Initialized(), OnInitialized);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
    }

    void OnInitialized(GameMessage msg){
        //On start game - Modify all profiles to include King and Playfield
        Debug.Log("OnInitialized");
        foreach(Owner owner in OwnersCoordinator.GetOwners())
            owner.GetPlayerProfile().Modify(owner.GetKing(), owner.GetPlayfield());
        foreach(Owner owner in OwnersCoordinator.GetOwners())
            Debug.Log(owner.GetPlayerProfile().Print());
    }

    void OnPlayerJoined(GameMessage msg){
        PlayerProfileCoordinator.AddProfile(msg.owner);
        PlayerProfileCoordinator.GetProfile(msg.owner).AddMoney(100f);
    }
}
