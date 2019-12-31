﻿public class Score
{
    public string scoreName;
    public int reward;
    public string description;
    public int counter;
    public int rewardDelta;
    public ScoreType scoreType;

    public int total {
        get{return counter / rewardDelta * reward;}
    }

    public Score(ScoreScriptable score){
        scoreName = score.scoreName;
        reward = score.reward;
        description = score.description;
        rewardDelta = score.rewardDelta;
        scoreType = score.scoreType;
    }
}
