using System.Collections;
using System.Collections.Generic;
//using NDream.AirConsole;
//using Newtonsoft.Json.Linq;
using UnityEngine;

/*public class NetworkImportantCoordinator : Singleton<NetworkImportantCoordinator> {
    public int defaultAttemptCount = 5;
    Dictionary<string, JObject> importantUnsent = new Dictionary<string, JObject>();
    public HashSet<string> hashes = new HashSet<string>();
    public static void SendImportant(int deviceId, JObject json) {
        SendImportant(deviceId, json, Instance.defaultAttemptCount);
    }
    public static void SendImportant(int deviceId, JObject json, int attempts) {
        if (AirConsole.instance == null)
            return;
        if (!OwnersCoordinator.GetOwner(deviceId).connected)
            return;
        string hash = GetToken(json);
        if (Instance.hashes.Contains(hash))
            return;
        json["token"] = hash;
        json["important"] = true;
        json["deviceId"] = deviceId;
        json["attempts"] = attempts;

        Instance.importantUnsent.Add(hash, json);
        Instance.hashes.Add(hash);
    }
    public static void ResendImportantAll() {
        if (AirConsole.instance == null)
            return;
        //Debug.Log("ResendImportantAll");
        List<string> toRemove = new List<string>();
        foreach (KeyValuePair<string, JObject> pair in Instance.importantUnsent) {
            pair.Value["attempts"] = (int)pair.Value["attempts"] - 1;
            //Debug.Log("sending important to: " + (int)pair.Value["deviceId"] + "  msg:: " + pair.Value + " atempts left: " + (int)pair.Value["attempts"]);
            if ((int)pair.Value["attempts"] >= 0) {
                NetworkCoordinator.TrySend((int)pair.Value["deviceId"], pair.Value);
            } else {
                toRemove.Add(pair.Key);
            }
        }
        foreach (string key in toRemove) {
            //Debug.Log("important out of attempts: " + (int)Instance.importantUnsent[key]["deviceId"] + "  msg:: " + Instance.importantUnsent[key] + " atempts left: " + (int)Instance.importantUnsent[key]["attempts"]);
            Instance.importantUnsent.Remove(key);
            Instance.hashes.Remove(key);
        }
    }
    public static void TryConfirmImportantReceived(JToken json) {
        string hash = json["token"].ToString();
        if (Instance.hashes.Contains(hash)) {
            //Debug.Log("important message confirmation received: " + hash);
            Instance.importantUnsent.Remove(hash);
            Instance.hashes.Remove(hash);
        } else {
            //Debug.Log("no such hash: " + hash);
        }
    }
    static string GetToken(JObject json) {
        string token = "sheep" + json.ToString() + "_!-" + Time.time;
        string hash = CryptoHelper.md5(token);
        //Debug.Log("token:" + token + " hash: " + hash);
        return hash;
    }
    public static bool IsImportantUnsent() {
        if (Instance.importantUnsent.Keys.Count > 0) {
            return true;
        }
        return false;
    }
}*/