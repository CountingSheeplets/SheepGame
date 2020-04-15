using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class NetworkImportantCoordinator : Singleton<NetworkImportantCoordinator> {
    public int defaultAttemptCount = 5;
    Dictionary<string, JObject> importantUnsent = new Dictionary<string, JObject>();
    public List<string> hashes;
    public static void SendImportant(int deviceId, JObject json) {
        SendImportant(deviceId, json, Instance.defaultAttemptCount);
    }
    public static void SendImportant(int deviceId, JObject json, int attempts) {
        string hash = GetToken(json);
        json["important"] = true;
        json["token"] = hash;
        json["deviceId"] = deviceId;
        json["attempts"] = attempts;
        Instance.importantUnsent.Add(hash, json);
    }
    public static void ResendImportantAll() {
        Debug.Log("ResendImportantAll");
        Instance.hashes = new List<string>(Instance.importantUnsent.Keys);
        List<string> toRemove = new List<string>();
        foreach (KeyValuePair<string, JObject> pair in Instance.importantUnsent) {
            pair.Value["attempts"] = (int)pair.Value["attempts"] - 1;
            if ((int)pair.Value["attempts"] >= 0)
                NetworkCoordinator.TrySend((int)pair.Value["deviceId"], pair.Value);
            else {
                toRemove.Add(pair.Key);
            }
        }
        foreach (string key in toRemove) {
            Instance.importantUnsent.Remove(key);
        }
    }
    public static void TryConfirmImportantReceived(JToken json) {
        string hash = json["token"].ToString();
        if (Instance.importantUnsent.ContainsKey(hash)) {
            Debug.Log("in dict, will try to remove: " + hash);
            Instance.importantUnsent.Remove(hash);
        } else
            Debug.Log("no such hash: " + hash);
    }
    static string GetToken(JObject json) {
        string token = "sheep" + json.ToString() + "_!";
        string hash = CryptoHelper.md5(token);
        Debug.Log("token:" + token + " hash: " + hash);
        return hash;
    }
    public static bool IsImportantUnsent() {
        if (Instance.importantUnsent.Keys.Count > 0) {
            return true;
        } else {
            Instance.hashes = new List<string>(Instance.importantUnsent.Keys);
            return false;
        }
    }
}