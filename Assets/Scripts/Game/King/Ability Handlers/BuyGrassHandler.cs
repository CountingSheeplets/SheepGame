using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGrassHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.KingAbilities.BuyGrass(), OnPlayerBuyGrass);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.Input.KingAbilities.BuyGrass(), OnPlayerBuyGrass);
    }
    void OnPlayerBuyGrass(GameMessage msg){
        //Use PriceCoordinator here to get price
        float price = PriceCoordinator.GetPrice(msg.owner, PriceName.King.BuyGrass());
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
