using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BaseAnalyticsTracker : MonoBehaviour {
    public Dictionary<string, object> parameters = new Dictionary<string, object>();
    [HideInInspector]
    public string eventName = "";

    void Start() {
        EventCoordinator.StartListening(eventName, OnGameEvent);
    }
    private void OnDestroy() {
        EventCoordinator.StopListening(eventName, OnGameEvent);
    }

    public virtual void OnGameEvent(GameMessage msg) {}

    public bool Dispatch(string evName = "") {
        if (evName != "")
            eventName = evName;
        AnalyticsResult result
            = AnalyticsEvent.Custom(eventName, parameters);
        if (result == AnalyticsResult.Ok) {
            Debug.Log("Analytics dispatch success: " + eventName);
            return true;
        } else {
            return false;
        }
    }
}