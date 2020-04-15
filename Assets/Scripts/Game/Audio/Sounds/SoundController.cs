using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : BaseSoundPlayer {
#if UNITY_EDITOR
    [StringInList(typeof(PropertyDrawersHelper), "AllEventNames")]
#endif
    public string eventName;
    void Start() {
        EventCoordinator.StartListening(eventName, OnPlayerEliminated);
    }
    private void OnDestroy() {
        EventCoordinator.StopListening(eventName, OnPlayerEliminated);
    }
    void OnPlayerEliminated(GameMessage msg) {
        base.PlayOnce();
    }
}