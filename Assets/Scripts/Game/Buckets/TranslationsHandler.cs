using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using UnityEngine;

public class TranslationsHandler : Singleton<TranslationsHandler> {
    public string ready = "ready";
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
    string info_tile_more = "more people - more fun! up to 8 players can join";
    public static string GetInfoTileMoreTr() {
        if (Instance.info_tile_more != null)
            return Instance.info_tile_more;
        else return "more people - more fun! up to 8 players can join";
    }
    string info_tile_need = "need at least 2 players to be able to play. best - 5 players";
    public static string GetInfoTileNeedTr() {
        if (Instance.info_tile_need != null)
            return Instance.info_tile_need;
        else return "need at least 2 players to be able to play. best - 5 players";
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
        Debug.Log("OnReady:" + AirConsole.instance._translations);
        //single word translations, which are somewhere in UI
        Instance.ready = AirConsole.instance.GetTranslation("ready");
        Instance.achievements = AirConsole.instance.GetTranslation("achievements");
        Instance.info_tile_more = AirConsole.instance.GetTranslation("info_tile_more");
        Instance.info_tile_need = AirConsole.instance.GetTranslation("info_tile_need");
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
            string idScore = scoreScriptable.scoreName.ToLower().Replace(" ", "_").Replace(".", "").Replace("-", "_") + "_";
            string trTitle = AirConsole.instance.GetTranslation(idScore + "title");
            string trDesc = AirConsole.instance.GetTranslation(idScore + "desc");
            string trDelta = AirConsole.instance.GetTranslation(idScore + "delta");
            if (trTitle != null)ScoreCoordinator.Instance.translatedScores.scores[i].displayName = trTitle;
            if (trDesc != null)ScoreCoordinator.Instance.translatedScores.scores[i].description = trDesc;
            if (trDelta != null)ScoreCoordinator.Instance.translatedScores.scores[i].wordDelta = trDelta;
            if (trDelta != null)ScoreCoordinator.Instance.translatedScores.scores[i].Y = trDelta;
        }
    }
}