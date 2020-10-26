using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIAnimationController : MonoBehaviour {
    public Animator anim;
    void Start() {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StartListening(EventName.System.Environment.CleanScene(), OnCleanScene);
    }
    void OnStartGame(GameMessage msg) {
        anim.SetTrigger("loseFocus");
    }
    void OnCleanScene(GameMessage msg) {
        anim.SetTrigger("toFocus");
    }
}