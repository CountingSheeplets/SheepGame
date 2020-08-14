using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class PersistentDataCoordinator : Singleton<PersistentDataCoordinator> {
    string key = "sheepGameData";
    public List<string> requestedNames = new List<string>();
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
        json["token"] = owner.GetToken((int) json["coinCount"]);
        TryStore(json, owner);
        owner.GetPlayerProfile().permanentCrownCount = (int) json["coinCount"];
    }

    public static void RequestData(Owner owner) {
        Debug.Log("RequestData");
        if (AirConsole.instance != null) {
            AirConsole.instance.RequestPersistentData(new List<string>() { owner.ownerId });
            Instance.requestedNames.Add(owner.ownerId);
            Debug.Log("RequestData:" + owner.ownerId);
        }
    }

    public static void OnReceivedData(JToken receivedToken) {
        string match = "";
        string[] names = Instance.requestedNames.ToArray();
        foreach (string request in names) {
            Debug.Log("OnReceivedData: request!: " + request);
            Debug.Log("OnReceivedData: data!: " + receivedToken);
            if (receivedToken[request].IsNullOrEmpty()) {
                Debug.Log("OnReceivedData: IsNullOrEmpty()!: " + receivedToken);
                continue;
            }
            match = request;
            JToken data = receivedToken[match][Instance.key];
            if (data.IsNullOrEmpty())
                continue;
            if (!data.ContainsPlayerData())
                continue;
            Owner owner = OwnersCoordinator.GetOwner(data["ownerID"].ToString());
            if (owner) {
                Debug.Log("OnReceivedData: contains all data!: " + data);
                PlayerProfile profile = owner.GetPlayerProfile();
                if (profile != null)
                    if (HasReceivedTrueData(owner, data)) {
                        Debug.Log("OnReceivedData: data is true!");
                        if (!data["coinCount"].IsNullOrEmpty())
                            profile.permanentCrownCount = (int) data["coinCount"];
                        if (!data["selectedHat"].IsNullOrEmpty())
                            profile.selectedHat = (int) data["selectedHat"];
                        if (!data["selectedScepter"].IsNullOrEmpty())
                            profile.SelectItem(KingItemType.scepter, (int) data["selectedScepter"]);
                        EventCoordinator.TriggerEvent(EventName.Input.SetKingItem(), GameMessage.Write()
                            .WithOwner(owner)
                            .WithIntMessage(profile.selectedHat)
                            .WithKingItemType(KingItemType.hat));
                        EventCoordinator.TriggerEvent(EventName.Input.SetKingItem(), GameMessage.Write()
                            .WithOwner(owner)
                            .WithIntMessage(profile.selectedScepter)
                            .WithKingItemType(KingItemType.scepter));
                        EventCoordinator.TriggerEvent(EventName.System.Player.ProfileUpdate(), GameMessage.Write().WithPlayerProfile(profile).WithOwner(owner));
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
            if ((string) data["token"] == owner.GetToken((int) data["coinCount"])) {
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
    public static void DeleteAllStoredData() {
        if (AirConsole.instance != null) {
            foreach (Owner owner in OwnersCoordinator.GetOwners())
                AirConsole.instance.StorePersistentData(Instance.key, "", owner.ownerId);
            Debug.Log("AirConsole.instance.StorePersistentData:" + "deleted");
        }
    }
}