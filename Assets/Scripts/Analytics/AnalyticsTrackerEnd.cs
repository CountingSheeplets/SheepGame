using System.Linq;
using UnityEngine;

public class AnalyticsTrackerEnd : BaseAnalyticsTracker {
    int timeSecStart = 0;
    void Awake() {
        eventName = EventName.UI.ShowScoreScreen();
        timeSecStart = Mathf.FloorToInt(Time.time);
    }

    public override void OnGameEvent(GameMessage msg) {
        parameters.Add("players", OwnersCoordinator.GetOwners().Count);
        parameters.Add("heroes", OwnersCoordinator.GetOwners().Where(x => x.GetPlayerProfile().isPremium).ToList().Count);

        parameters.Add("next_match", SuccessiveMatchTracker.GetMatchAmount());
        parameters.Add("time_of_win", (Mathf.FloorToInt(Time.time) - timeSecStart));
        parameters.Add("accuracy_winner", PlayerAccuracyTracker.GetAccuracy(msg.owner));
        /*         List<int> crowns = new List<int>();
                List<int> earned = new List<int>();
                Dictionary<string, int> achCrowns = new Dictionary<string, int>();
                foreach (Owner owner in OwnersCoordinator.GetOwners()) {
                    crowns.Add(owner.GetPlayerProfile().permanentCrownCount);
                    earned.Add(owner.GetPlayerProfile().GetCrowns());
                }
                foreach (Owner owner in OwnersCoordinator.GetOwners()) {
                    PlayerScores pScores = ScoreCoordinator.GetPlayerScores(owner);
                    foreach (Score score in pScores.scores) {
                        if (!achCrowns.ContainsKey(score.scoreName))
                            achCrowns.Add(score.scoreName, score.total);
                        else
                            achCrowns[score.scoreName] += score.total;
                    }
                } */
        //parameters.Add("crowns_total", crowns);
        //parameters.Add("crowns_earned", earned);
        /*         foreach (KeyValuePair<string, int> pair in achCrowns) {
                    parameters.Add(pair.Key, pair.Value);
                } */
        Dispatch();
    }

}