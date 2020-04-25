using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreloadSheep : MonoBehaviour {
    public GameObject sheepModel;
    void Start() {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnGameStart);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnGameStart);
    }
    void OnGameStart(GameMessage msg) {
        GameObject newModel = Instantiate(sheepModel, Vector2.up * 1000f, Quaternion.identity);
        Destroy(newModel);
    }
}