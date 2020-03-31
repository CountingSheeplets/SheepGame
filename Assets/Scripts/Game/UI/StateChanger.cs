using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChanger : MonoBehaviour {
    public SetActive startState;
    public SetActive toState;
#if UNITY_EDITOR
    [StringInList(typeof(PropertyDrawersHelper), "AllEventNames")]
#endif
    public string changeStateEventName;
    //public Component changeTarget;

    void Start() { //EventName.Input.StartGame()
        EventCoordinator.StartListening(changeStateEventName, OnStart);
        switch (startState) {
            case SetActive.none:
                break;
            case SetActive.enable:
                gameObject.SetActive(true);
                break;
            case SetActive.disable:
                gameObject.SetActive(false);
                break;
        }
    }
    void OnDestroy() {
        EventCoordinator.StopListening(changeStateEventName, OnStart);
    }
    void OnStart(GameMessage msg) {
        //activate visuals here
        switch (toState) {
            case SetActive.enable:
                gameObject.SetActive(true);
                break;
            case SetActive.disable:
                gameObject.SetActive(false);
                break;
        }
    }

}
public enum SetActive {
    none,
    enable,
    disable
}