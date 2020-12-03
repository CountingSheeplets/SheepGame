using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfieldOutlineColorHandler : MonoBehaviour {
    bool trigger;
    float timeToFade = 4f;
    float visibility = 0f;
    float startingVisibility = 0.2f;
    SpriteRenderer rend;
    Owner owner;
    Color playerColor = Color.black;
    void Start() {
        EventCoordinator.StartListening(EventName.System.Environment.ArenaAnimating(), OnArenaAnimate);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnGameStart);
        rend = GetComponent<SpriteRenderer>();
        owner = GetComponentInParent<Owner>();
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Environment.ArenaAnimating(), OnArenaAnimate);
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnGameStart);
    }
    void OnGameStart(GameMessage msg) {
        if (playerColor.r == 0)playerColor = owner.GetPlayerProfile().playerColor;
        rend.material.SetColor("_PlayerColor", playerColor);
        OnArenaAnimate(msg);
    }
    void OnArenaAnimate(GameMessage msg) {
        trigger = true;
        visibility = 1f;
        rend.material.SetFloat("_OutlineEdgeThickness", startingVisibility);
    }
    void Update() {
        if (trigger) {
            visibility -= Time.deltaTime / timeToFade;
            float nonLinear = 0f;
            if (visibility > 0.5f)
                nonLinear = Easing.Sinusoidal.InOut(2 - visibility * 2f) * startingVisibility;
            else
                nonLinear = Easing.Quadratic.In(visibility * 2f) * startingVisibility;
            if (visibility >= 0)
                rend.material.SetFloat("_OutlineEdgeThickness", nonLinear);
            else {
                rend.material.SetFloat("_OutlineEdgeThickness", -1f);
                trigger = false;
            }
        }
    }

}