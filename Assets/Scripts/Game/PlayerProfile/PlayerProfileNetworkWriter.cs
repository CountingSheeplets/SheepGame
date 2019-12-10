using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;

public class PlayerProfileNetworkWriter : MonoBehaviour
{
    public float sendInterval = 1f;
    float counter;

    void Update()
    {
        counter+=Time.deltaTime;
        if(counter > sendInterval){
            counter = 0;
            foreach(Owner owner in OwnersCoordinator.GetOwners()){
                PlayerProfile profile = owner.GetPlayerProfile();
                if(SendProfile(profile)){
                    EventCoordinator.TriggerEvent(EventName.System.Player.ProfileUpdate(), GameMessage.Write().WithPlayerProfile(profile));
                }else{
                    Debug.Log("Cant Send. profile not found for owner:"+owner);
                }
            }
            CardCanvasCoordinator.Sort();
            EventCoordinator.TriggerEvent(EventName.System.Player.PlayerCardsSorted(), GameMessage.Write());
        }
    }

    bool SendProfile(PlayerProfile profile){
        if(profile == null){
            return false;
        }
        var data = new Dictionary<string, float> { { "health", profile.GetHealth() },
                                                    { "money", profile.GetMoney() },
                                                    { "grass", Mathf.FloorToInt(profile.GetGrass())},
                                                    { "crowns", profile.GetStarCount()},

                                                    { "priceGrass", PriceCoordinator.GetPrice(profile.owner, PriceName.King.BuyGrass())},
                                                    { "priceSheep", PriceCoordinator.GetPrice(profile.owner, PriceName.King.BuySheep())},
                                                    { "priceCharge", PriceCoordinator.GetPrice(profile.owner, PriceName.King.Charge())},
                                                    { "priceUpgrade1", PriceCoordinator.GetPrice(profile.owner, PriceName.Sheep.ToMini())},
                                                    { "priceUpgrade2", PriceCoordinator.GetPrice(profile.owner, PriceName.Sheep.ToKnight())} };
        NetworkCoordinator.SendPlayerProfile(profile.owner.deviceId, data);
        return true;
    }
}
