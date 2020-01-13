using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(GameStateView.GetGameState() != GameState.started) return;
        if(!SendProfile(msg.playerProfile)){
            Debug.Log("Cant Send. profile not found for owner:"+msg.owner);
        }
        CardCanvasCoordinator.Sort();
        EventCoordinator.TriggerEvent(EventName.System.Player.PlayerCardsSorted(), GameMessage.Write());
    }

    bool SendProfile(PlayerProfile profile){
        if(profile == null)
            return false;
        if(!profile.isAlive) //also should be if(profile.isDirty)
            return false;
        var data = new Dictionary<string, float>{ { "health", profile.GetHealth() },
                                                    { "money", profile.GetMoney() },
                                                    { "grass", Mathf.FloorToInt(profile.GetGrass())},
                                                    { "crowns", profile.GetCrowns()},
                                                    { "permanentCrowns", profile.permanentCrownCount},

                                                    { "priceGrass", PriceCoordinator.GetPrice(profile.owner, PriceName.King.BuyGrass())},
                                                    { "priceSheep", PriceCoordinator.GetPrice(profile.owner, PriceName.King.BuySheep())},
                                                    { "priceSmash", PriceCoordinator.GetPrice(profile.owner, PriceName.King.Smash())},
                                                    { "priceUpgrade1", PriceCoordinator.GetPrice(profile.owner, UpgradeBucket.ToName(profile.playfield.GetComponent<SheepUpgrade>().typeA))},
                                                    { "priceUpgrade2", PriceCoordinator.GetPrice(profile.owner, UpgradeBucket.ToName(profile.playfield.GetComponent<SheepUpgrade>().typeB))}
                                                };
        NetworkCoordinator.SendPlayerProfile(profile.owner.deviceId, data);
        return true;
    }
}
