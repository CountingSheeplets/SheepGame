using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class EventSubscribeAll : MonoBehaviour {
    public List<string> events = new List<string>();
    void Start() {
        events.AddRange(EventName.Get());
        foreach (string ev in events) {
            EventCoordinator.StartListening(ev, OnEvent);
        }
    }
    void OnDestroy() {
        foreach (string ev in events) {
            EventCoordinator.StopListening(ev, OnEvent);
        }
    }

    void OnEvent(GameMessage msg) {
        Debug.Log("event occured");
    }
}