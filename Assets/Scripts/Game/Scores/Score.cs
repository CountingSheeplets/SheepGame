public class Score
{
    public string scoreName;
    public int reward;
    public string description;
    public int counter;
    public int rewardDelta;
    public string wordDelta;
    public ScoreType scoreType;
    public string icon;

    public int total {
        get{return counter / rewardDelta * reward;}
    }

    public Score(ScoreScriptable score){
        scoreName = score.scoreName;
        reward = score.reward;
        description = score.description;
        rewardDelta = score.rewardDelta;
        scoreType = score.scoreType;
        wordDelta = score.wordDelta;
        icon = score.icon.ToString().Split(' ')[0];
    }
}
