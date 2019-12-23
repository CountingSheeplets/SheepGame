using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;

public class PlayerProfileNetworkWriter : MonoBehaviour
{
    void Start(){
        EventCoordinator.StartListening(EventName.System.Player.ProfileUpdate(), OnProfileUpdate);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Player.ProfileUpdate(), OnProfileUpdate);
    }
    void OnProfileUpdate(GameMessage msg)
    {
        if(!SendProfile(msg.playerProfile)){
            Debug.Log("Cant Send. profile not found for owner:"+msg.owner);
        }
        CardCanvasCoordinator.Sort();
        EventCoordinator.TriggerEvent(EventName.System.Player.PlayerCardsSorted(), GameMessage.Write());
    }

    bool SendProfile(PlayerProfile profile){
        if(profile == null){
            return false;
        }
        var data = new Dictionary<string, float> { { "health", profile.GetHealth() },
                                                    { "money", profile.GetMoney() },
                                                    { "grass", Mathf.FloorToInt(profile.GetGrass())},
                                                    { "crowns", profile.GetStars()},

                                                    { "priceGrass", PriceCoordinator.GetPrice(profile.owner, PriceName.King.BuyGrass())},
                                                    { "priceSheep", PriceCoordinator.GetPrice(profile.owner, PriceName.King.BuySheep())},
                                                    { "priceSmash", PriceCoordinator.GetPrice(profile.owner, PriceName.King.Smash())},
                                                    { "priceUpgrade1", PriceCoordinator.GetPrice(profile.owner, PriceName.SheepUpgrade.Small())},
                                                    { "priceUpgrade2", PriceCoordinator.GetPrice(profile.owner, PriceName.SheepUpgrade.Armored())} };
        NetworkCoordinator.SendPlayerProfile(profile.owner.deviceId, data);
        return true;
    }
}
