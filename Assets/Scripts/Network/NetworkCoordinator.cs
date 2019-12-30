using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using NDream.AirConsole;

public class NetworkCoordinator : Singleton<NetworkCoordinator>
{
    public static void SendPlayerProfile(int deviceId, Dictionary<string, float> profileData){
        NetworkObject newNetObj = new NetworkObject("playerProfile", profileData);
        SendObject(deviceId, newNetObj);
    }

    public static void SendShowView(int deviceId, string view){
        NetworkObject newNetObj = new NetworkObject("changeView", view);
        SendObject(deviceId, newNetObj);
    }
    public static void SendShowViewAll(string view){
        NetworkObject newNetObj = new NetworkObject("changeView", view);
        SendObjectAll(newNetObj);
    }

    public static void SendUpgradeData(int deviceId, UpgradeProperty upgrade){
        JObject json = new JObject();
        json["type"] = "upgradeData";
        json["upgrade"] = JToken.FromObject(upgrade);
        AirConsole.instance.Message(deviceId, json);
    }
    public static void SendUpgradeButtons(int deviceId, SheepUnit sheep){
        JObject json = new JObject();
        json["type"] = "upgradeButtons";
        //čia bucket neturi None sheeptype ir duoda null paprastai aviai
        UpgradeProperty upgradeA = UpgradeBucket.GetNextUpgradeA(sheep);
        UpgradeProperty upgradeB = UpgradeBucket.GetNextUpgradeB(sheep);
        string iconA = "";
        float priceA = 0;
        string iconB = "";
        float priceB = 0;
        if(upgradeA != null){
            iconA = upgradeA.sheepTypeOutput.ToString();
            priceA = PriceCoordinator.GetPrice(sheep.owner, upgradeA.upgradeCodeName);
        }
        if(upgradeB != null){
            iconB = upgradeB.sheepTypeOutput.ToString();
            priceB = PriceCoordinator.GetPrice(sheep.owner, upgradeB.upgradeCodeName);
        }
        json["upgradeA_icon"] = iconA;
        json["upgradeB_icon"] = iconB;
        json["upgradeA_price"] = priceA;
        json["upgradeB_price"] = priceB;
        AirConsole.instance.Message(deviceId, json);
    }
    public static void SendColor(int deviceId, string colorHex){
        NetworkObject newNetObj = new NetworkObject("playerColor", colorHex);
        SendObject(deviceId, newNetObj);
    }
    public static void SendName(int deviceId, string playerName){
        NetworkObject newNetObj = new NetworkObject("playerName", playerName);
        SendObject(deviceId, newNetObj);
    }
    public static void SendConfirmReady(int deviceId){
        NetworkObject newNetObj = new NetworkObject("ready", "1");
        SendObject(deviceId, newNetObj);
    }
    public static void SendPlayerScores(int deviceId, bool win, List<Score> scores){
        JObject json = new JObject();
        json["type"] = "playerScores";
        json["value"] = win.ToString();
        json["scores"] = JToken.FromObject(scores);
        AirConsole.instance.Message(deviceId, json);
    }
//general functions:
    static void SendObject(int deviceId, NetworkObject networkObject){
        JToken data = JToken.FromObject(networkObject.PrepairedNetworkObject());
        AirConsole.instance.Message(deviceId, data);
    }
    static void SendObjectAll(NetworkObject networkObject){
        JToken data = JToken.FromObject(networkObject.PrepairedNetworkObject());
        AirConsole.instance.Broadcast(data);
    }

}