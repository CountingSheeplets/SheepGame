using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfileController : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Environment.Initialized(), OnInitialized);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StartListening(EventName.Input.KingAbilities.BuyGrass(), OnPlayerBuyGrass);
        
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Environment.Initialized(), OnInitialized);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StopListening(EventName.Input.KingAbilities.BuyGrass(), OnPlayerBuyGrass);
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
    }

    void OnPlayerBuyGrass(GameMessage msg){
        //Use PriceCoordinator here to get price
        float price = 5f;
        PlayerProfile profile = PlayerProfileCoordinator.GetProfile(msg.owner);
        if(price < profile.GetMoney()){
            profile.AddMoney(-price);
            //send msg that money is lower now
            profile.ModifyGrass(2f);
            //send msg that grass is increased
            Debug.Log("Grasss bought!");
        } else {
            //Send Msg that not enough
            Debug.Log("Cannto buy, not anough money");
        }
    }
}
