﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenHandler : MonoBehaviour {
    bool trigger;
    void Awake() {
        gameObject.SetActive(false);
        EventCoordinator.StartListening(EventName.Input.PlayersReady(), OnStartGame);
    }

    void OnStartGame(GameMessage msg) {
        gameObject.SetActive(true);
        StartCoroutine(StartGameNextFrame());
    }
    IEnumerator StartGameNextFrame() {
        yield return new WaitForFixedUpdate();
        EventCoordinator.TriggerEvent(EventName.Input.StartGame(), GameMessage.Write());
        yield return null;
        gameObject.SetActive(false);
        yield return null;
    }
}