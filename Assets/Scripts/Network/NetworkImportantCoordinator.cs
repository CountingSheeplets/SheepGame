﻿using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class NetworkImportantCoordinator : Singleton<NetworkImportantCoordinator> {
    Dictionary<string, JObject> importantUnsent = new Dictionary<string, JObject>();
    public List<string> hashes;
    public static void SendImportant(int deviceId, JObject json) {
        string hash = GetToken(json);
        json["important"] = true;
        json["token"] = hash;
        json["deviceId"] = deviceId;
        Instance.importantUnsent.Add(hash, json);
    }
    public static void ResendImportantAll() {
        Debug.Log("ResendImportantAll");
        Instance.hashes = new List<string>(Instance.importantUnsent.Keys);
        foreach (KeyValuePair<string, JObject> pair in Instance.importantUnsent) {
            NetworkCoordinator.TrySend((int)pair.Value["deviceId"], pair.Value);
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
        string token = "sheep" + json + "_!";
        //Debug.Log("token:" + token);
        return CryptoHelper.md5(token);
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