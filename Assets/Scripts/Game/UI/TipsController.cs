using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using TMPro;
using UnityEngine;
public class TipsController : MonoBehaviour {
    TextMeshProUGUI tipText;
    float counter;
    bool ready = false;
    void Awake() {
        tipText = GetComponent<TextMeshProUGUI>();
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnGameStart);
        EventCoordinator.StartListening(EventName.System.SceneLoaded(), OnReady);
        AirConsole.instance.onReady += OnReady;
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnGameStart);
        EventCoordinator.StopListening(EventName.System.SceneLoaded(), OnReady);
        if (AirConsole.instance != null)
            AirConsole.instance.onReady -= OnReady;
    }
    void OnGameStart(GameMessage msg) {
        gameObject.SetActive(false);
    }
    void OnReady(GameMessage msg) {
        OnReady();
    }
    void OnReady(string input = "") {
        ready = true;
    }
    void Update() {
        if (ready)
            counter += Time.deltaTime;
        if (counter > ConstantsBucket.TipLoopTimer) {
            counter = 0;
            tipText.text = "Tip: " + TipsBucket.GetNextTip();
        }
    }
}