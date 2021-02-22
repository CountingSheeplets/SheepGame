using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaOffsetAnimator : MonoBehaviour {
    public Transform arenaTr;
    public bool animateIn;
    public bool animateOut;
    public float progress = 0;
    public float animSpeed = 1f;
    Vector3 startPos;
    Vector3 matchPosition = new Vector3(0.8f, 0f, 0f);
    Vector3 scorePosition = new Vector3(0.8f, 10f, 0f);
    void Start() {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StartListening(EventName.System.Environment.EndMatch(), OnEndMatch);
    }
    void OnStartGame(GameMessage msg) {
        startPos = arenaTr.position;
        animateIn = true;
        progress = 0;
    }
    void OnEndMatch(GameMessage msg) {
        startPos = matchPosition;
        animateOut = true;
        progress = 0;
    }

    void FixedUpdate() {
        if (animateIn) {
            progress += Time.deltaTime * animSpeed;
            if (progress < 1f)
                arenaTr.position = Vector3.Lerp(startPos, matchPosition, progress);
            else {
                arenaTr.position = Vector3.Lerp(startPos, matchPosition, 1f);
                animateIn = false;
            }
        }

        if (animateOut) {
            progress += Time.deltaTime * animSpeed;
            if (progress < 1f)
                arenaTr.position = Vector3.Lerp(startPos, scorePosition, progress);
            else {
                arenaTr.position = Vector3.Lerp(startPos, scorePosition, 1f);
                animateOut = false;
                EventCoordinator.TriggerEvent(EventName.UI.ScoreScreenShown(), GameMessage.Write());
            }
        }
    }
}