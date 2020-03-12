using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
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
        Debug.Log("AirConsole-Send:" + json);
        TrySend(deviceId, json);
    }
    public static void SendShowViewAll(string view) {
        NetworkObject newNetObj = new NetworkObject("changeView", view);
        TrySendObjectAll(newNetObj);
    }

    public static void SendUpgradeData(int deviceId, UpgradeProperty upgrade) {
        JObject json = new JObject();
        json["type"] = "upgradeData";
        json["upgrade"] = JToken.FromObject(upgrade);
        TrySend(deviceId, json);
    }
    public static void SendKingItems(Owner owner) {
        JObject json = new JObject();
        PlayerProfile profile = owner.GetPlayerProfile();
        json["type"] = "kingItems";
        json["crowns"] = profile.permanentCrownCount;

        JObject hatJson = new JObject();
        hatJson["ID"] = profile.selectedHat;
        hatJson["itemName"] = KingItemBucket.GetItem(profile.selectedHat, KingItemType.hat).itemName;
        hatJson["spriteName"] = KingItemBucket.GetItem(profile.selectedHat, KingItemType.hat).spriteName;
        Debug.Log("sending hat spriteName: " + hatJson["spriteName"]);

        JObject hCrownsJson = new JObject();
        hCrownsJson["required"] = KingItemBucket.GetItem(profile.selectedHat, KingItemType.hat).crownRequirement;
        hCrownsJson["requirementMet"] = KingItemBucket.IsCrownRequirementMet(owner, profile.selectedHat, KingItemType.hat);
        hatJson["crowns"] = hCrownsJson;

        JObject hPremiumJson = new JObject();
        hPremiumJson["required"] = KingItemBucket.GetItem(profile.selectedHat, KingItemType.hat).premiumRequirement;
        hPremiumJson["requirementMet"] = KingItemBucket.IsPremiumRequirementMet(owner, profile.selectedHat, KingItemType.hat);
        hatJson["premium"] = hPremiumJson;

        hatJson["unlocked"] = KingItemBucket.IsItemAvailable(owner, profile.selectedHat, KingItemType.hat);

        json["newHat"] = hatJson;

        JObject scepterJson = new JObject();
        scepterJson["ID"] = profile.selectedScepter;
        scepterJson["itemName"] = KingItemBucket.GetItem(profile.selectedScepter, KingItemType.scepter).itemName;
        scepterJson["spriteName"] = KingItemBucket.GetItem(profile.selectedScepter, KingItemType.scepter).spriteName;
        Debug.Log("sending scepter spriteName: " + scepterJson["spriteName"]);

        JObject sCrownsJson = new JObject();
        sCrownsJson["required"] = KingItemBucket.GetItem(profile.selectedScepter, KingItemType.scepter).crownRequirement;
        sCrownsJson["requirementMet"] = KingItemBucket.IsCrownRequirementMet(owner, profile.selectedScepter, KingItemType.scepter);
        scepterJson["crowns"] = sCrownsJson;

        JObject sPremiumJson = new JObject();
        sPremiumJson["required"] = KingItemBucket.GetItem(profile.selectedScepter, KingItemType.scepter).premiumRequirement;
        sPremiumJson["requirementMet"] = KingItemBucket.IsPremiumRequirementMet(owner, profile.selectedScepter, KingItemType.scepter);
        scepterJson["premium"] = sPremiumJson;

        scepterJson["unlocked"] = KingItemBucket.IsItemAvailable(owner, profile.selectedScepter, KingItemType.scepter);

        json["newScepter"] = scepterJson;
        TrySend(owner.deviceId, json);
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
        NetworkObject newNetObj = new NetworkObject("playerColor", colorHex);
        TrySendObject(deviceId, newNetObj);
    }
    public static void SendName(int deviceId, string playerName) {
        NetworkObject newNetObj = new NetworkObject("playerName", playerName);
        TrySendObject(deviceId, newNetObj);
    }
    public static void SendConfirmReady(int deviceId, bool value) {
        JObject json = new JObject();
        json["type"] = "ready";
        int valInt = value ? 1 : 0;
        json["value"] = valInt;
        TrySend(deviceId, json);
    }
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
        json["total"] = totalScore;
        TrySend(deviceId, json);
    }

    public static bool SendProfile(PlayerProfile profile) {
        if (profile == null)
            return false;
        if (!profile.isAlive) //also should be if(profile.isDirty)
            return false;
        var data = new Dictionary<string, float> { { "health", profile.GetHealth() },
                { "money", profile.GetMoney() },
                { "grass", Mathf.FloorToInt(profile.GetGrass()) },
                { "crowns", profile.GetCrowns() },
                { "permanentCrowns", profile.permanentCrownCount },

                { "priceGrass", PriceCoordinator.GetPrice(profile.owner, PriceName.King.BuyGrass()) },
                { "priceSheep", PriceCoordinator.GetPrice(profile.owner, PriceName.King.BuySheep()) },
                { "priceSmash", PriceCoordinator.GetPrice(profile.owner, PriceName.King.Smash()) },
                { "priceUpgrade1", PriceCoordinator.GetPrice(profile.owner, UpgradeBucket.ToName(profile.playfield.GetComponent<SheepUpgrade>().typeA)) },
                { "priceUpgrade2", PriceCoordinator.GetPrice(profile.owner, UpgradeBucket.ToName(profile.playfield.GetComponent<SheepUpgrade>().typeB)) }
            };
        NetworkCoordinator.SendPlayerProfile(profile.owner.deviceId, data);
        return true;
    }

    //general functions:
    static void TrySendObject(int deviceId, NetworkObject networkObject) {
        if (AirConsole.instance != null) {
            JToken data = JToken.FromObject(networkObject.PrepairedNetworkObject());
            AirConsole.instance.Message(deviceId, data);
        }
    }
    static void TrySendObjectAll(NetworkObject networkObject) {
        if (AirConsole.instance != null) {
            JToken data = JToken.FromObject(networkObject.PrepairedNetworkObject());
            AirConsole.instance.Broadcast(data);
        }
    }

    static void TrySend(int deviceId, JObject json) {
        if (AirConsole.instance != null)
            AirConsole.instance.Message(deviceId, json);
    }
}