using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour {
    public Transform ownerPanelContainer;

    private void Start() {
        //EventCoordinator.StartListening(EventName.System.SceneLoaded(), OnSceneLoaded);
        //OnSceneLoaded(GameMessage.Write());
    }
    private void OnDestroy() {
        //EventCoordinator.StopListening(EventName.System.SceneLoaded(), OnSceneLoaded);
    }
    void OnSceneLoaded(GameMessage msg) {
        //OwnersCoordinator.SetOwnersPanelContainer(ownerPanelContainer);
    }
}