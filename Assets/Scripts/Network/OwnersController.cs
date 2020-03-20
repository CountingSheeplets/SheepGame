using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class OwnersController : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StartListening(EventName.System.Environment.CleanScene(), OnCleanScene);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StopListening(EventName.System.Environment.CleanScene(), OnCleanScene);
    }
    void OnStartGame(GameMessage msg) {
        foreach (Owner owner in OwnersCoordinator.GetOwners())
            owner.gameObject.SetActive(false);
    }
    void OnCleanScene(GameMessage msg) {
        foreach (Owner owner in OwnersCoordinator.GetOwners())
            owner.gameObject.SetActive(true);
    }
}