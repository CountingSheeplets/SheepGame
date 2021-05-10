using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerScores {
    [SerializeField]
    public List<Score> scores = new List<Score>();
    public PlayerScores(PlayerScores scr) {
        foreach (Score score in scr.scores) {
            scores.Add(new Score(score));
        }
    }
    public PlayerScores(List<ScoreScriptable> scriptables) {
        foreach (ScoreScriptable scriptable in scriptables) {
            scores.Add(new Score(scriptable));
        }
    }

    public Score GetScore(string scoreName) {
        //if (scores.Count == 0)
        //    Debug.Log("check defaultScores if scriptables are in the list in ScoreCoordinator !");
        foreach (Score score in scores) {
            if (score.scoreName == scoreName) {
                return score;
            }
        }
        return null;
    }

    public int GetScoreSum(ScoreType scoreType) {
        int scoreSum = 0;
        foreach (Score score in scores) {
            if (score.scoreType == scoreType) {
                scoreSum += score.total;
            }
        }
        return scoreSum;
    }
    public override string ToString() {
        string output = "Scores:";
        foreach (Score score in scores) {
            output += "\n|||" + score.ToString();
        }
        return output;
    }
}