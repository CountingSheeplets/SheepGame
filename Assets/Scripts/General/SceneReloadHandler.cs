using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReloadHandler : MonoBehaviour {
    void Start() {
        if (GameStateView.HasState(GameState.gameReloaded))
            EventCoordinator.TriggerEvent(EventName.System.SceneLoaded(), GameMessage.Write());
    }
}