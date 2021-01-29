using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUIAnimationController : MonoBehaviour {
    public Animator blueBackgAnim;
    public Animator anim;
    public float delay = 1f;
    bool endMatchTrigger = false;
    float counter = 0f;
    void Start() {
        EventCoordinator.StartListening(EventName.UI.ShowScoreScreen(), OnScoresShow);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StartListening(EventName.System.Environment.ScrollScoresOut(), OnSceneClean);
    }
    void OnStartGame(GameMessage msg) {
        blueBackgAnim.SetTrigger("scroll");
        anim.SetTrigger("match");
    }
    void OnScoresShow(GameMessage msg) {
        anim.SetTrigger("show");
    }
    void OnSceneClean(GameMessage msg) {
        anim.SetTrigger("hide");
        endMatchTrigger = true;
    }

    void Update() {
        if (endMatchTrigger) {
            counter += Time.deltaTime;
            if (counter > delay) {
                EventCoordinator.TriggerEvent(EventName.System.Environment.CleanScene(), GameMessage.Write());
                endMatchTrigger = false;
            }
        }
    }
}