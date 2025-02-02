using System.Collections;
using System.Collections.Generic;
//using NDream.AirConsole;
using UnityEngine;

public class TipsBucket : Singleton<TipsBucket> {
    public List<string> tips = new List<string>();
    public List<string> translations = new List<string>();
    public List<string> shuffledTips = new List<string>();
    int currentTip = 0;
    List<int> order = new List<int>();
    string baseStr = "tip_";

    void Start() {
        //EventCoordinator.StartListening(EventName.System.SceneLoaded(), OnReady);
        //AirConsole.instance.onReady += OnReady;
    }
    /*void OnReady(GameMessage msg) {
        OnReady();
    }
    public void OnReady(string code = "") {
        for (int i = 0; i < tips.Count; i++) {
            string inp = baseStr + (i + 1).ToString();
            //string welcomeScreenTranslated = AirConsole.instance.GetTranslation(inp);
            //if (welcomeScreenTranslated == "")
            //    welcomeScreenTranslated = tips[i];
            //translations.Add(welcomeScreenTranslated);
        }
        //shuffledTips = translations.Shuffle<string>();
        shuffledTips = translations.Shuffle<string>();
    }*/
    public static string GetNextTip() {
        Instance.currentTip++;
        if (Instance.currentTip >= Instance.shuffledTips.Count)
            Instance.currentTip = 0;
        return Instance.shuffledTips[Instance.currentTip];
    }
    void OnDestroy() {
        //EventCoordinator.StopListening(EventName.System.SceneLoaded(), OnReady);
        //if (AirConsole.instance != null) {
        //    AirConsole.instance.onReady -= OnReady;
        //}
    }
}