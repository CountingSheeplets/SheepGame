using UnityEngine;

public class AnalyticsTrackerDefeat : BaseAnalyticsTracker {
    int timeSecStart = 0;
    void Awake() {
        eventName = EventName.System.Player.Eliminated();
        timeSecStart = Mathf.FloorToInt(Time.time);
    }

    public override void OnGameEvent(GameMessage msg) {
        parameters.Add("time_of_defeat", (Mathf.FloorToInt(Time.time) - timeSecStart));
        parameters.Add("gold_total", msg.targetOwner.GetPlayerProfile().GetMoneyEarned());
        parameters.Add("accuracy_loser", PlayerAccuracyTracker.GetAccuracy(msg.targetOwner));
        Dispatch();
    }
}