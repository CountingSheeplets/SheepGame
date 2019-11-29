using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCoordinator : Singleton<ScoreCoordinator>
{
    public List<ScoreScriptable> defaultScores = new List<ScoreScriptable>();

    public Dictionary<Owner, PlayerScores> scores = new Dictionary<Owner, PlayerScores>();
    public static void AddPlayerScore(Owner owner){
        if(!Instance.scores.ContainsKey(owner))
            Instance.scores.Add(owner, new PlayerScores(Instance.defaultScores));
        else Debug.Log("already contains owner for PlayerScores!");
    }
    public static void RemovePlayerScore(Owner owner){
        if(Instance.scores.ContainsKey(owner))
            Instance.scores.Remove(owner);
        else Debug.Log("no Such PlayerScores!");
    }

    public static void IncreaseScoreCounter(Owner owner, string scoreName, int amount){
        Score score = Instance.GetPlayerScore(owner, scoreName);
        if(score != null)
        {
            score.counter += amount;
        }
    }
    public static void GetTotalPlayerScores(Owner owner){
        PlayerScores scores = Instance.GetPlayerScore(owner);
        int total = 0;
        if(scores != null){
            foreach(Score score in scores.scores){
                total += score.total;
            }
        }
    }

    public PlayerScores GetPlayerScore(Owner owner){
        if(scores.ContainsKey(owner))
            return scores[owner];
        else
            return null;
    }
    public Score GetPlayerScore(Owner owner, string scoreName){
        if(scores.ContainsKey(owner))
            return scores[owner].GetScore(scoreName);
        else
            return null;
    }
}