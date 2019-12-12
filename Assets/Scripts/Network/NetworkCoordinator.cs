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

    public static void SendUpgradeData(int deviceId, string upgradeName){
        NetworkObject newNetObj = new NetworkObject("upgrade", upgradeName);
        SendObject(deviceId, newNetObj);
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