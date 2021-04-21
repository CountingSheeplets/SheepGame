using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessiveMatchTracker : Singleton<SuccessiveMatchTracker> {
    string eventName;
    int _nextMatch = 0;
    public static int GetMatchAmount() {
        return Instance._nextMatch;
    }
    void Start() {
        eventName = EventName.System.Environment.EndMatch();
        EventCoordinator.StartListening(eventName, OnEvent);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(eventName, OnEvent);
    }
    void OnEvent(GameMessage msg) {
        Instance._nextMatch++;
    }
}