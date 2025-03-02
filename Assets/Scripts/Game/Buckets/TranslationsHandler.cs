using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationsHandler : Singleton<TranslationsHandler> {
    bool translationsReady = false;
    public string ready = "ready";
    public static string GetReadyTranslation() {
        if (Instance)
            if (Instance.ready != null)
                return Instance.ready.ToLower();
        return "ready";
    }
    string achievements = "achievements";
    public static string GetAchievementsTranslation() {
        if (Instance)
            if (Instance.achievements != null)
                return Instance.achievements.ToLower();
        return "achievements";
    }
    string disconnected = "disconnected";
    public static string GetDisconnectedTranslation() {
        if (Instance)
            if (Instance.disconnected != null)
                return Instance.disconnected.ToLower();
        return "disconnected";
    }
    string info_tile_more = "more players - more fun! push A on your controller to join!";
    public static string GetInfoTileMoreTr() {
        if (Instance)
            if (Instance.info_tile_more != null)
                return Instance.info_tile_more;
        return "more players - more fun! push A on your controller to join!";
    }
    string info_tile_need = "need at least 2 players to be able to play. best - 5 players";
    public static string GetInfoTileNeedTr() {
        if (Instance)
            if (Instance.info_tile_need != null)
                return Instance.info_tile_need;
        return "need at least 2 players to be able to play. best - 5 players";
    }
    void Start() {
        //AirConsole.instance.onReady += OnReady;
        EventCoordinator.StartListening(EventName.System.SceneLoaded(), OnReady);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), OnJoined);
    }
    void OnDestroy() {
        //if (AirConsole.instance != null) {
        //    AirConsole.instance.onReady -= OnReady;
        //}
        EventCoordinator.StopListening(EventName.System.SceneLoaded(), OnReady);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), OnJoined);
    }
    void OnReady(GameMessage msg) {
        //OnReady();
        //NetworkCoordinator.SendTranslationsReady();
    }
    void OnJoined(GameMessage msg) {
        //if (translationsReady)
         //   NetworkCoordinator.SendTranslationsReady(msg.owner.deviceId);
    }
    /*void OnReady(string code = "") {
        translationsReady = true;
        string lang = AirConsole.instance.GetLanguage();
        //single word translations, which are somewhere in UI
        Instance.ready = AirConsole.instance.GetTranslation("ready");
        Instance.achievements = AirConsole.instance.GetTranslation("achievements");
        Instance.disconnected = AirConsole.instance.GetTranslation("disconnected");
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
        NetworkCoordinator.SendTranslationsReady();
    }*/
}