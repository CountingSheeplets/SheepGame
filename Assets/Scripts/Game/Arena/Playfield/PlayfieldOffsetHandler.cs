using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfieldOffsetHandler : MonoBehaviour {
    public float offsetDown = 0.5f;
    public float targetScale = 0.9f;
    public float effectDuration = 0.5f;
    float progress = 0f;
    bool trigger;
    Playfield playfield;
    Vector3 startingPosition;
    void Start() {
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnSmashed);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnSmashed);
    }
    void OnSmashed(GameMessage msg) {
        if (playfield == null) playfield = GetComponentInParent<Playfield>();
        if (playfield == msg.kingUnit.myPlayfield) {
            trigger = true;
            progress = 0f;
            if (startingPosition == null)
                startingPosition = transform.position;
        }
    }
    void Update() {
        if (trigger) {
            progress += Time.deltaTime / effectDuration;
            if (progress <= 0.5f) {
                float curved = Easing.Quadratic.InOut(progress * 2f);
                transform.localPosition = Vector2.Lerp(startingPosition, startingPosition + Vector3.down * offsetDown, curved);
                transform.localScale = Vector2.Lerp(Vector3.one, Vector3.one * targetScale, curved);
            } else {
                float curved = Easing.Quadratic.InOut(progress * 2f - 1f);
                transform.localPosition = Vector2.Lerp(startingPosition + Vector3.down * offsetDown, startingPosition, curved);
                transform.localScale = Vector2.Lerp(Vector3.one * targetScale, Vector3.one, curved);
            }
            if (progress > 1f) {
                trigger = false;
                transform.localPosition = startingPosition;
            }
        }
    }
}