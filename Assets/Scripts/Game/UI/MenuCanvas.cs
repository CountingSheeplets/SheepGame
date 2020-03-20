using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour {
    private void Start() {
        EventCoordinator.StartListening(EventName.System.SceneLoaded(), OnSceneLoaded);
    }
    private void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.SceneLoaded(), OnSceneLoaded);
    }
    void OnSceneLoaded(GameMessage msg) {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}