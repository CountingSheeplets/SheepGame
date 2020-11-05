using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class TipsController : MonoBehaviour {
    TextMeshProUGUI tipText;
    float counter;
    void Start() {
        tipText = GetComponent<TextMeshProUGUI>();
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnGameStart);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnGameStart);
    }
    void OnGameStart(GameMessage msg) {
        gameObject.SetActive(false);
    }

    void Update() {
        counter += Time.deltaTime;
        if (counter > ConstantsBucket.TipLoopTimer) {
            counter = 0;
            tipText.text = "Tip: " + TipsBucket.GetNextTip();
        }
    }
}