using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class PersistentDataCoordinator : Singleton<PersistentDataCoordinator> {
    string key = "sheepGameData";
    private List<string> requestedNames = new List<string>();
    public bool IsRequestedNamesEmpty {
        get { return requestedNames.Count > 0; }
    }
    public static void StoreData(Owner owner) {
        //NetworkObject newNetObj = new NetworkObject("changeView", view);
        JObject json = new JObject();
        json["ownerID"] = owner.ownerId;
        json["coinCount"] = owner.GetPlayerProfile().permanentCrownCount + owner.GetPlayerProfile().GetCrowns();
        json["selectedHat"] = KingCoordinator.GetSourceKingModel(owner).HatIndex;
        json["selectedScepter"] = KingCoordinator.GetSourceKingModel(owner).ScepterIndex;
        json["token"] = owner.GetToken((int)json["coinCount"]);
        TryStore(json, owner);
    }

    public static void RequestData(Owner owner) {
        if (AirConsole.instance != null) {
            AirConsole.instance.RequestPersistentData(new List<string>() { owner.ownerId });
            Instance.requestedNames.Add(owner.ownerId);
        }
    }

    public static void OnReceivedData(JToken data) {
        string match = "";
        foreach (string request in Instance.requestedNames) {
            if (data[request].IsNullOrEmpty()) {
                Debug.Log("OnReceivedData: IsNullOrEmpty()!: " + data);
                continue;
            }
            match = request;
            data = data[match][Instance.key];
            Owner owner = OwnersCoordinator.GetOwner(data["ownerID"].ToString());
            if (owner && data.ContainsPlayerData()) {
                Debug.Log("OnReceivedData: contains all data!: " + data);
                PlayerProfile profile = owner.GetPlayerProfile();
                if (profile != null)
                    if (HasReceivedTrueData(owner, data)) {
                        Debug.Log("OnReceivedData: data is true!");
                        if (!data["coinCount"].IsNullOrEmpty())
                            profile.permanentCrownCount = (int)data["coinCount"];
                        if (!data["selectedHat"].IsNullOrEmpty())
                            profile.selectedHat = (int)data["selectedHat"];
                        if (!data["selectedScepter"].IsNullOrEmpty())
                            profile.selectedScepter = (int)data["selectedScepter"];
                        EventCoordinator.TriggerEvent(EventName.System.Player.ProfileUpdate(), GameMessage.Write().WithPlayerProfile(profile).WithOwner(owner));
                        EventCoordinator.TriggerEvent(EventName.Input.SetKingItem(), GameMessage.Write()
                            .WithOwner(owner)
                            .WithIntMessage(profile.selectedHat)
                            .WithKingItemType(KingItemType.hat));
                        EventCoordinator.TriggerEvent(EventName.Input.SetKingItem(), GameMessage.Write()
                            .WithOwner(owner)
                            .WithIntMessage(profile.selectedScepter)
                            .WithKingItemType(KingItemType.scepter));
                        Debug.Log("data received and loaded!");
                    }
            }
            if (match != "")
                Instance.requestedNames.Remove(match);
        }
    }

    public static bool HasReceivedTrueData(Owner owner, JToken data) {
        if (data["token"] != null && data["coinCount"] != null) {
            //Debug.Log("comparing tokens: " + data["token"] + "     :vs:    " + owner.GetToken((int) data["coinCount"]));
            if ((string)data["token"] == owner.GetToken((int)data["coinCount"])) {
                return true;
            }
        }
        return false;
    }

    static void TryStore(JObject json, Owner owner) {
        if (AirConsole.instance != null) {
            AirConsole.instance.StorePersistentData(Instance.key, json, owner.ownerId);
            Debug.Log("AirConsole.instance.StorePersistentData:" + json);
        }
    }
}