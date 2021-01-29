using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerViewMenuHandler : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.System.SceneLoaded(), OnSceneReloaded);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.SceneLoaded(), OnSceneReloaded);
    }

    void OnSceneReloaded(GameMessage msg) {
        MenuNetworkHandler.ReloadAllConnected();
        //Debug.Log("OnSceneLoad - Owners:" + OwnersCoordinator.GetOwnersAll().Count);
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            owner.ready = false;
        }
    }
}