[System.Serializable]
public class Score {
    public string scoreName;
    public string displayName;
    public int reward;
    public string description;
    public int counter;
    public int rewardDelta;
    public string wordDelta;
    public ScoreType scoreType;
    public string icon;
    public string X;
    public string Y;
    public int total {
        get { return (int)((float)counter / (float)rewardDelta * (float)reward); }
    }
    public Score(Score score) {
        scoreName = score.scoreName;
        displayName = score.displayName;
        reward = score.reward;
        description = score.description;
        rewardDelta = score.rewardDelta;
        scoreType = score.scoreType;
        wordDelta = score.wordDelta;
        X = score.X;
        Y = score.Y;
        icon = score.icon.ToString().Split(' ')[0];
    }
    public Score(ScoreScriptable score) {
        scoreName = score.scoreName;
        displayName = score.scoreName;
        reward = score.reward;
        description = score.description;
        rewardDelta = score.rewardDelta;
        scoreType = score.scoreType;
        wordDelta = score.wordDelta;
        X = score.X;
        Y = score.Y;
        icon = score.icon.ToString().Split(' ')[0];
    }
    public override string ToString() {
        return scoreName +
            " counter:" + counter +
            " total:" + total +
            " reward:" + reward +
            " wordDelta:" + wordDelta +
            " displayName:" + displayName +
            " description:" + description;
        //" wordDelta:" + wordDelta +
        //" icon:" + icon;
    }
}