using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class OwnersController : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.System.Environment.CleanScene(), OnCleanScene);
        EventCoordinator.StartListening(EventName.System.Environment.CleanScene(), OnSceneClean);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Environment.CleanScene(), OnCleanScene);
        EventCoordinator.StopListening(EventName.System.Environment.CleanScene(), OnSceneClean);
    }
    void OnStartGame(GameMessage msg) {
        foreach (Owner owner in OwnersCoordinator.GetOwners())
            owner.gameObject.SetActive(false);
    }
    void OnCleanScene(GameMessage msg) {
        foreach (Owner owner in OwnersCoordinator.GetOwners())
            owner.gameObject.SetActive(true);
    }
    void OnSceneClean(GameMessage message) {
        foreach (Owner owner in OwnersCoordinator.GetOwners().Where(x => !x.connected).ToList()) {
            //MenuNetworkHandler.Instance.OnDisconnect(owner.deviceId);
        }
    }
}