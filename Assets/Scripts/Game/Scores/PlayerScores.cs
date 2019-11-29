using System.Collections;
using System.Collections.Generic;

public class PlayerScores
{
    public List<Score> scores = new List<Score>();

    public PlayerScores(List<ScoreScriptable> scriptables){
        foreach(ScoreScriptable scriptable in scriptables){
            scores.Add(new Score(scriptable));
        }
    }

    public Score GetScore(string scoreName){
        foreach(Score score in scores){
            if(score.scoreName== scoreName){
                return score;
            }
        }
        return null;
    }
}
