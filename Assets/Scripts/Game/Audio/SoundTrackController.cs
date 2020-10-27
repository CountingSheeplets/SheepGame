using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrackController : MonoBehaviour {
    void Awake() {
        EventCoordinator.StartListening(EventName.System.Player.Eliminated(), OnPlayerElimination);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnPlayerElimination);
        EventCoordinator.StartListening(EventName.UI.ShowScoreScreen(), OnShowScores);
        //EventCoordinator.StartListening(EventName.System.SceneLoaded(), OnSceneLoaded);
        SoundTrackCoordinator.PlayMenu();
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Player.Eliminated(), OnPlayerElimination);
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnPlayerElimination);
        EventCoordinator.StopListening(EventName.UI.ShowScoreScreen(), OnShowScores);
        //EventCoordinator.StopListening(EventName.System.SceneLoaded(), OnSceneLoaded);
    }
    void OnPlayerElimination(GameMessage msg) {
        SoundTrackCoordinator.PlayNext();
    }
    void OnShowScores(GameMessage msg) {
        SoundTrackCoordinator.PlayScores();
    }
    void OnSceneLoaded(GameMessage msg) {}
}