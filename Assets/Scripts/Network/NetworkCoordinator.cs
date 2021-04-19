using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
public class NetworkCoordinator : Singleton<NetworkCoordinator> {
    public static void SendPlayerProfile(int deviceId, Dictionary<string, float> profileData) {
        NetworkObject newNetObj = new NetworkObject("playerProfile", profileData);
        TrySendObject(deviceId, newNetObj);
    }

    public static void SendShowView(int deviceId, string view) {
        //NetworkObject newNetObj = new NetworkObject("changeView", view);
        JObject json = new JObject();
        json["type"] = "changeView";
        json["value"] = view;
        json["deviceId"] = deviceId;
        //Debug.Log("AirConsole-Send:" + json);
        NetworkImportantCoordinator.SendImportant(deviceId, json);
    }
    public static void SendShowViewAll(string view) {
        //NetworkObject newNetObj = new NetworkObject("changeView", view);
        //TrySendObjectAll(newNetObj);
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            SendShowView(owner.deviceId, view);
        }
    }

    public static void SendUpgradeData() {

        JObject json = new JObject();
        json["type"] = "upgradeData";
        List<UpgradeProperty> props = UpgradeBucket.GetUpgrades();

        json["upgrade"] = JsonConvert.SerializeObject(props);

        /*         string test = "";
                foreach (UpgradeProperty prop in props) {
                    test += prop.enumStringName;
                }
                Debug.Log("enumStringName: " + test); */
        TrySendObjectAll(json);
    }
    public static void SendUpgradeIcon(int deviceId, UpgradeProperty upgrade) {
        JObject json = new JObject();
        json["type"] = "currentUpgradeIcon";
        if (upgrade != null)
            json["icon"] = upgrade.sheepTypeOutput.ToString();
        else
            json["icon"] = "";
        TrySend(deviceId, json);
    }
    public static void SendKingItems(Owner owner) {
        JObject json = new JObject();
        PlayerProfile profile = owner.GetPlayerProfile();
        json["type"] = "kingItems";
        json["permanentCrownCount"] = profile.permanentCrownCount;

        JObject hatJson = new JObject();
        hatJson["ID"] = profile.seenHat;
        KingItem hat = KingItemBucket.GetItem(profile.seenHat, KingItemType.hat);
        hatJson["itemName"] = hat.itemName;
        hatJson["spriteName"] = hat.spriteName;
        //Debug.Log("sending hat spriteName: " + hatJson["spriteName"]);

        JObject hCrownsJson = new JObject();
        hCrownsJson["required"] = hat.crownRequirement;
        hCrownsJson["requirementMet"] = KingItemBucket.IsCrownRequirementMet(owner, profile.seenHat, KingItemType.hat);
        hatJson["crowns"] = hCrownsJson;

        JObject hPremiumJson = new JObject();
        hPremiumJson["required"] = hat.premiumRequirement;
        hPremiumJson["requirementMet"] = KingItemBucket.IsPremiumRequirementMet(owner, hat);
        hatJson["premium"] = hPremiumJson;

        hatJson["unlocked"] = KingItemBucket.IsItemAvailable(owner, profile.seenHat, KingItemType.hat);
        Debug.Log("sending hPremiumJson: " + hPremiumJson);
        json["newHat"] = hatJson;

        JObject scepterJson = new JObject();
        scepterJson["ID"] = profile.seenScepter;
        KingItem scepter = KingItemBucket.GetItem(profile.seenScepter, KingItemType.scepter);
        scepterJson["itemName"] = scepter.itemName;
        scepterJson["spriteName"] = scepter.spriteName;
        //Debug.Log("sending scepter spriteName: " + scepterJson["spriteName"]);

        JObject sCrownsJson = new JObject();
        sCrownsJson["required"] = scepter.crownRequirement;
        sCrownsJson["requirementMet"] = KingItemBucket.IsCrownRequirementMet(owner, profile.seenScepter, KingItemType.scepter);
        scepterJson["crowns"] = sCrownsJson;

        JObject sPremiumJson = new JObject();
        sPremiumJson["required"] = scepter.premiumRequirement;
        sPremiumJson["requirementMet"] = KingItemBucket.IsPremiumRequirementMet(owner, scepter);
        scepterJson["premium"] = sPremiumJson;

        scepterJson["unlocked"] = KingItemBucket.IsItemAvailable(owner, profile.seenScepter, KingItemType.scepter);

        json["newScepter"] = scepterJson;
        TrySend(owner.deviceId, json);
        profile.CleanNetworkDirty();
    }
    public static void SendUpgradeButtons(int deviceId, SheepUnit sheep) {
        JObject json = new JObject();
        json["type"] = "upgradeButtons";
        //čia bucket neturi None sheeptype ir duoda null paprastai aviai
        UpgradeProperty upgradeA = UpgradeBucket.GetNextUpgradeA(sheep);
        UpgradeProperty upgradeB = UpgradeBucket.GetNextUpgradeB(sheep);
        string iconA = "";
        float priceA = 0;
        string iconB = "";
        float priceB = 0;
        if (upgradeA != null) {
            iconA = upgradeA.sheepTypeOutput.ToString();
            priceA = PriceCoordinator.GetPrice(sheep.owner, upgradeA.upgradeCodeName);
        }
        if (upgradeB != null) {
            iconB = upgradeB.sheepTypeOutput.ToString();
            priceB = PriceCoordinator.GetPrice(sheep.owner, upgradeB.upgradeCodeName);
        }
        json["upgradeA_icon"] = iconA;
        json["upgradeB_icon"] = iconB;
        json["upgradeA_price"] = priceA;
        json["upgradeB_price"] = priceB;
        TrySend(deviceId, json);
    }
    public static void SendColor(int deviceId, string colorHex) {
        JObject json = new JObject();
        json["type"] = "playerColor";
        json["value"] = colorHex;
        NetworkImportantCoordinator.SendImportant(deviceId, json);
    }
    public static void SendTutorialIndex(PlayerProfile profile) {
        JObject json = new JObject();
        json["type"] = "tutorialIndex";
        json["value"] = profile.tutorialIndex;
        NetworkImportantCoordinator.SendImportant(profile.owner.deviceId, json);
    }
    public static void SendName(int deviceId, string playerName) {
        JObject json = new JObject();
        json["type"] = "playerName";
        json["value"] = playerName;
        NetworkImportantCoordinator.SendImportant(deviceId, json);
    }
    public static void SendConfirmReady(int deviceId, bool value) {
        JObject json = new JObject();
        json["type"] = "ready";
        int valInt = value ? 1 : 0;
        json["value"] = valInt;
        json["deviceId"] = deviceId;
        NetworkImportantCoordinator.SendImportant(deviceId, json, 3);
    }
    //////////////////////// Audio
    public static void SendFirstOwner(int deviceId, bool value) {
        JObject json = new JObject();
        json["type"] = "firstOwner";
        int valInt = value ? 1 : 0;
        json["value"] = valInt;
        json["deviceId"] = deviceId;
        TrySend(deviceId, json);
    }
    public static void SendAudioState(int deviceId, int value) {
        JObject json = new JObject();
        json["type"] = "audio";
        json["value"] = value;
        json["deviceId"] = deviceId;
        TrySend(deviceId, json);
    }
    ////**************************
    public static void SendConfirmPlayAgain(int deviceId, bool value) {
        JObject json = new JObject();
        json["type"] = "playAgain";
        int valInt = value ? 1 : 0;
        json["value"] = valInt;
        TrySend(deviceId, json);
    }
    public static void SendPlayerScores(int deviceId, bool win, List<Score> scores, int totalScore) {
        JObject json = new JObject();
        json["type"] = "playerScores";
        json["value"] = win;
        json["scores"] = JToken.FromObject(scores);
        json["totalScore"] = totalScore;
        TrySend(deviceId, json);
    }

    public static bool SendProfile(PlayerProfile profile) {
        if (profile == null)
            return false;
        if (!profile.owner.connected)
            return false;
        if (!profile.isAlive) //also should be if(profile.isDirty)
            return false;
        var data = new Dictionary<string, float> { //{ "health", profile.GetHealth() },
                { "money", profile.GetMoney() },
                { "grass", Mathf.FloorToInt(profile.GetGrass()) },
                { "crowns", profile.GetCrowns() },
                { "permanentCrowns", profile.permanentCrownCount },

                { "priceGrass", PriceCoordinator.GetPrice(profile.owner, PriceName.King.BuyGrass()) },
                { "priceSheep", PriceCoordinator.GetPrice(profile.owner, PriceName.King.Upgrade()) },
                { "priceSmash", PriceCoordinator.GetPrice(profile.owner, PriceName.King.Smash()) },
                { "priceUpgrade1", PriceCoordinator.GetPrice(profile.owner, UpgradeBucket.ToName(profile.playfield.GetComponent<SheepUpgrade>().typeA)) },
                { "priceUpgrade2", PriceCoordinator.GetPrice(profile.owner, UpgradeBucket.ToName(profile.playfield.GetComponent<SheepUpgrade>().typeB)) }
            };
        NetworkCoordinator.SendPlayerProfile(profile.owner.deviceId, data);
        profile.CleanNetworkDirty();
        return true;
    }

    //general functions:
    static void TrySendObject(int deviceId, NetworkObject networkObject) {
        if (AirConsole.instance != null) {
            JToken data = JToken.FromObject(networkObject.PrepairedNetworkObject());
            AirConsole.instance.Message(deviceId, data);
        }
    }
    static void TrySendObjectAll(JObject json) {
        if (AirConsole.instance != null) {
            AirConsole.instance.Broadcast(json);
        }
    }
    static void TrySendObjectAll(NetworkObject networkObject) {
        if (AirConsole.instance != null) {
            JToken data = JToken.FromObject(networkObject.PrepairedNetworkObject());
            AirConsole.instance.Broadcast(data);
        }
    }
    public static void TrySend(int deviceId, JObject json) {
        if (AirConsole.instance != null)
            AirConsole.instance.Message(deviceId, json);
    }
}