using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour {
    public Transform ownerPanelContainer;
    public GameObject tutorialPanelMenu;
    public GameObject tutorialPanelMatch;

    private void Start() {
        //EventCoordinator.StartListening(EventName.System.SceneLoaded(), OnSceneLoaded);
        //OnSceneLoaded(GameMessage.Write());
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
    }
    private void OnDestroy() {
        //EventCoordinator.StopListening(EventName.System.SceneLoaded(), OnSceneLoaded);
    }
    void OnSceneLoaded(GameMessage msg) {
        //OwnersCoordinator.SetOwnersPanelContainer(ownerPanelContainer);
    }

    void OnStartGame(GameMessage msg) {
        tutorialPanelMenu.SetActive(false);
        tutorialPanelMatch.SetActive(true);
    }
}