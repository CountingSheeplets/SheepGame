using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrackController : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.System.Player.Eliminated(), OnPlayerElimination);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnPlayerElimination);
        //EventCoordinator.StartListening(EventName.System.SceneLoaded(), OnSceneLoaded);
        SoundTrackCoordinator.PlayMenu();
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Player.Eliminated(), OnPlayerElimination);
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnPlayerElimination);
        //EventCoordinator.StopListening(EventName.System.SceneLoaded(), OnSceneLoaded);
    }
    void OnPlayerElimination(GameMessage msg) {
        SoundTrackCoordinator.PlayNext();
    }
    void OnSceneLoaded(GameMessage msg) {}
}