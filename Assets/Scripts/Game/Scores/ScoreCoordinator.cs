using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreCoordinator : Singleton<ScoreCoordinator> {
    public List<ScoreScriptable> defaultScores = new List<ScoreScriptable>();
    [SerializeField]
    public PlayerScores translatedScores;

    public Dictionary<Owner, PlayerScores> scores = new Dictionary<Owner, PlayerScores>();

    Dictionary<Owner, int> techTier2Counts = new Dictionary<Owner, int>();
    void Awake() {
        Instance.translatedScores = new PlayerScores(Instance.defaultScores);
    }
    public static void AddPlayerScore(Owner owner) {
        Instance.scores.Add(owner, new PlayerScores(Instance.translatedScores));
    }
    public static void RemovePlayerScore(Owner owner) {
        if (Instance.scores.ContainsKey(owner))
            Instance.scores.Remove(owner);
    }

    public static void IncreaseScoreCounter(Owner owner, string scoreName, int amount) {
        Score score = GetPlayerScore(owner, scoreName);
        if (score != null) {
            score.counter += amount;
        }
    }
    public static int GetTotalPlayerScores(Owner owner) {
        PlayerScores scores = GetPlayerScores(owner);
        int total = 0;
        if (scores != null) {
            foreach (Score score in scores.scores) {
                total += score.total;
            }
        }
        return total;
    }

    public static PlayerScores GetPlayerScores(Owner owner) {
        if (!Instance.scores.ContainsKey(owner))
            AddPlayerScore(owner);
        return Instance.scores[owner];
    }
    public static Score GetPlayerScore(Owner owner, string scoreName) {
        return GetPlayerScores(owner).GetScore(scoreName);
    }
    public static Dictionary<Owner, Score> GetScore(string scoreName) {
        Dictionary<Owner, Score> scoresDict = new Dictionary<Owner, Score>();
        foreach (KeyValuePair<Owner, PlayerScores> pair in Instance.scores) {
            scoresDict.Add(pair.Key, pair.Value.GetScore(scoreName));
        }
        return scoresDict;
    }
    public static void SetTechTier2Counts(Owner owner, int techCount) {
        if (owner != null)
            Instance.techTier2Counts[owner] = techCount;
    }
    public static void CalculateTier2TechCounts() {
        if (Instance.techTier2Counts.Count == 0)
            return;
        Owner highestTier2Upgrader = Instance.techTier2Counts.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        IncreaseScoreCounter(highestTier2Upgrader, ScoreName.Achievement.Baaah(), 1);
    }
    public static string GetStringAllPlayerScores() {
        string output = "PlayerScorePrint:";
        foreach (KeyValuePair<Owner, PlayerScores> pair in Instance.scores) {
            output += "\n\nPlayer: " + pair.Key.ToString();
            output += " Scores: " + pair.Value.ToString();
        }
        return output;
    }
}