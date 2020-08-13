using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class NetworkImportantController : MonoBehaviour {
    public float timeout = 1f;
    float counter;

    void Awake() {
        if (AirConsole.instance != null)
            AirConsole.instance.onMessage += OnConfirmationReceived;
    }

    void OnConfirmationReceived(int from, JToken message) {
        if (message["important"] == null)
            return;
        if (message["token"] == null)
            return;
        if (message["deviceId"] == null)
            return;
        if ((bool) message["important"]) {
            NetworkImportantCoordinator.TryConfirmImportantReceived(message);
        }
    }

    private void Update() {
        if (NetworkImportantCoordinator.IsImportantUnsent()) {
            counter += Time.deltaTime;
            if (counter > timeout) {
                counter = 0;
                NetworkImportantCoordinator.ResendImportantAll(); //for some reason keeps resending, though shouldnt!
            }
        }
    }

    private void OnDestroy() {
        if (AirConsole.instance != null) {
            AirConsole.instance.onMessage -= OnConfirmationReceived;
        }
    }
}