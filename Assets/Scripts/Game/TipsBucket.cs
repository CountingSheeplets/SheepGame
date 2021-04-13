using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using UnityEngine;

public class TipsBucket : Singleton<TipsBucket> {
    public List<string> tips = new List<string>();
    public List<string> translations = new List<string>();
    public List<string> shuffledTips = new List<string>();
    int currentTip = 0;
    List<int> order = new List<int>();
    string baseStr = "tip_";

    void Start() {
        AirConsole.instance.onReady += OnReady;
    }
    public void OnReady(string code) {
        string lang = AirConsole.instance.GetLanguage();
        Debug.Log("Language: " + lang);
        Debug.Log("tip1:" + AirConsole.instance.GetTranslation("tip_1"));
        for (int i = 0; i < tips.Count; i++) {
            string inp = baseStr + (i + 1).ToString();
            string welcomeScreenTranslated = AirConsole.instance.GetTranslation(inp);
            if (welcomeScreenTranslated == "")
                welcomeScreenTranslated = tips[i];
            translations.Add(welcomeScreenTranslated);
        }
        shuffledTips = translations.Shuffle<string>();
    }
    public static string GetNextTip() {
        Instance.currentTip++;
        if (Instance.currentTip >= Instance.shuffledTips.Count)
            Instance.currentTip = 0;
        return Instance.shuffledTips[Instance.currentTip];
    }
    void OnDestroy() {
        if (AirConsole.instance != null) {
            AirConsole.instance.onReady -= OnReady;
        }
    }
}