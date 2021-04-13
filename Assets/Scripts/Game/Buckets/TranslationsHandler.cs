using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using UnityEngine;

public class TranslationsHandler : Singleton<TranslationsHandler> {
    string ready = "ready";
    public static string GetReadyTranslation() {
        if (Instance.ready != null)
            return Instance.ready.ToLower();
        else return "ready";
    }
    string achievements = "achievements";
    public static string GetAchievementsTranslation() {
        if (Instance.achievements != null)
            return Instance.achievements.ToLower();
        else return "achievements";
    }
    void Start() {
        AirConsole.instance.onReady += OnReady;
    }
    void OnDestroy() {
        if (AirConsole.instance != null) {
            AirConsole.instance.onReady -= OnReady;
        }
    }
    void OnReady(string code) {
        //single word translations, which are somewhere in UI
        Instance.ready = AirConsole.instance.GetTranslation("ready");
        Instance.achievements = AirConsole.instance.GetTranslation("achievements");
        //upgrades
        for (int i = 0; i < UpgradeBucket.GetUpgrades().Count; i++) {
            string idTitle = UpgradeBucket.Instance.upgrades[i].upgradeCodeName + "_Title";
            string idDesc = UpgradeBucket.Instance.upgrades[i].upgradeCodeName + "_Desc";
            string upgTrTitle = AirConsole.instance.GetTranslation(idTitle);
            string upgTrDesc = AirConsole.instance.GetTranslation(idDesc);
            if (upgTrTitle != null)
                UpgradeBucket.Instance.upgrades[i].upgradeDisplayName = upgTrTitle;
            if (upgTrDesc != null)
                UpgradeBucket.Instance.upgrades[i].upgradeDescription = upgTrDesc;
        }
        //king hats
        for (int i = 0; i < KingItemBucket.Instance.kingHats.Count; i++) {
            string idItem = KingItemBucket.Instance.kingHats[i].spriteName;
            string trItem = AirConsole.instance.GetTranslation(idItem);
            if (trItem != null) {
                Debug.Log(idItem + "-id, tr item:" + trItem + ":");
                KingItemBucket.Instance.kingHats[i].itemName = trItem;
            }
        }
        //king scepters
        for (int i = 0; i < KingItemBucket.Instance.kingScepters.Count; i++) {
            string idItem = KingItemBucket.Instance.kingScepters[i].spriteName;
            string trItem = AirConsole.instance.GetTranslation(idItem);
            if (trItem != null)
                KingItemBucket.Instance.kingScepters[i].itemName = trItem;
        }
        //scores
        ScoreCoordinator.Instance.translatedScores = new PlayerScores(ScoreCoordinator.Instance.defaultScores);
        for (int i = 0; i < ScoreCoordinator.Instance.translatedScores.scores.Count; i++) {
            Score scoreScriptable = ScoreCoordinator.Instance.translatedScores.scores[i];
            string idScore = scoreScriptable.scoreName.ToLower().Replace(" ", "_") + "_";
            string trTitle = AirConsole.instance.GetTranslation(idScore + "title");
            string trDesc = AirConsole.instance.GetTranslation(idScore + "desc");
            string trDelta = AirConsole.instance.GetTranslation(idScore + "delta");
            if (trTitle != null)ScoreCoordinator.Instance.translatedScores.scores[i].displayName = trTitle;
            if (trDesc != null)ScoreCoordinator.Instance.translatedScores.scores[i].description = trDesc;
            if (trDelta != null)ScoreCoordinator.Instance.translatedScores.scores[i].wordDelta = trDelta;
        }
    }
}