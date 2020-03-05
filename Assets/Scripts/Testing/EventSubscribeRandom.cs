using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscribeRandom : MonoBehaviour {
    string randomEvent = "";
    void Start() {
        randomEvent = EventName.Get() [Random.Range(0, EventName.Get().Count)];
        gameObject.name = randomEvent;
        EventCoordinator.StartListening(randomEvent, OnEvent);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(randomEvent, OnEvent);
    }

    void OnEvent(GameMessage msg) {
        Debug.Log("event occured");
    }
}