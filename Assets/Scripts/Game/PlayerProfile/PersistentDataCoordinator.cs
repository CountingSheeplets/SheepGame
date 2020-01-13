using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using NDream.AirConsole;

public class PersistentDataCoordinator : Singleton<PersistentDataCoordinator>
{
    string key = "sheepGameData";
    private List<string> requestedNames = new List<string>();
    public static void StoreData(Owner owner){
        //NetworkObject newNetObj = new NetworkObject("changeView", view);
        JObject json = new JObject();
        json["ownerID"] = owner.ownerId;
        json["coinCount"] = owner.GetPlayerProfile().permanentCrownCount + owner.GetPlayerProfile().GetCrowns();
        json["selectedCrown"] = owner.GetPlayerProfile().selectedCrown;
        json["selectedScepter"] = owner.GetPlayerProfile().selectedScepter;
        json["token"] = owner.GetToken((int)json["coinCount"]);
        TryStore(json, owner);
    }

    public static void RequestData(Owner owner){
        if(AirConsole.instance != null){
            AirConsole.instance.RequestPersistentData(new List<string>() { owner.ownerId });
            Instance.requestedNames.Add(owner.ownerId);
        }
    }

    public static void OnReceivedData(JToken data){
        string match = "";
        foreach(string request in Instance.requestedNames){
            if(data[request] == null)
                continue;
            else{
                //Debug.Log("OnReceivedData: "+data);
                if(data[request].IsNullOrEmpty()){
                    continue;
                }
                match = request;
                data = data[match][Instance.key];
                Owner owner = OwnersCoordinator.GetOwner(data["ownerID"].ToString());
                if(owner)
                    if(HasReceivedTrueData(owner, data)){
                        PlayerProfile profile = owner.GetPlayerProfile();
                        profile.permanentCrownCount = (int)data["coinCount"];
                        profile.selectedCrown = (int)data["selectedCrown"];
                        profile.selectedScepter = (int)data["selectedScepter"];
                        EventCoordinator.TriggerEvent(EventName.System.Player.ProfileUpdate(), GameMessage.Write().WithPlayerProfile(profile).WithOwner(owner));
                        Debug.Log("data received and loaded");
                    }
            }
        }
        if(match != "")
            Instance.requestedNames.Remove(match);
    }

    public static bool HasReceivedTrueData(Owner owner, JToken data){
        if(data["token"] != null && data["coinCount"] != null){
        Debug.Log("comparing tokens: "+data["token"] + "     :vs:    "+owner.GetToken((int)data["coinCount"]));

            if((string)data["token"] == owner.GetToken((int)data["coinCount"])){
                return true;
            }
        }
        return false;
    }

    static void TryStore(JObject json, Owner owner){
        if(AirConsole.instance != null){
            AirConsole.instance.StorePersistentData(Instance.key, json, owner.ownerId);
            Debug.Log("AirConsole.instance.StorePersistentData:"+json);
        }
    }
}
