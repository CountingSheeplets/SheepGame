using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingController : MonoBehaviour {
    Playfield playfield;
    Quaternion targetRotation;
    Vector2 targetDestination;

    public Transform crosshair;
    public Transform dirIndicator;
    SpriteRenderer indSprite;
    SpriteRenderer crossSprite;
    public Animator animator;

    public bool triggerMove = false;
    bool triggerFade = false;
    float progressMove;
    float progressFade;
    float timeToRotate = 0.2f;
    float timeToFade = 1.2f;
    Quaternion startRotation;
    Vector2 startPos;
    bool tapDown = false;

    Color myColor;
    public Color insideWheelCol = Color.gray;
    void Start() {
        playfield = GetComponentInParent<Playfield>();
        indSprite = dirIndicator.GetComponentInChildren<SpriteRenderer>();
        crossSprite = crosshair.GetComponentInChildren<SpriteRenderer>();
        EventCoordinator.StartListening(EventName.Input.Tap(), OnTap);
        EventCoordinator.StartListening(EventName.Input.Swipe(), OnSwipe);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStart);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.Tap(), OnTap);
        EventCoordinator.StopListening(EventName.Input.Swipe(), OnSwipe);
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStart);
    }
    void OnStart(GameMessage msg) {
        myColor = playfield.owner.GetPlayerProfile().playerColor;
        crossSprite.color = myColor;
        indSprite.color = myColor;

        crossSprite.color = new Color(myColor.r, myColor.g, myColor.b, 0f);
        indSprite.color = new Color(myColor.r, myColor.g, myColor.b, 0f);
    }
    void OnSwipe(GameMessage msg) {
        //Debug.Log("on swipe: " + playfield.owner + " vs " + msg.owner);
        if (playfield.owner.EqualsByValue(msg.owner)) {
            if (!tapDown) {
                indSprite.color = new Color(myColor.r, indSprite.color.g, indSprite.color.b, 1f);
                //crossSprite.color = new Color(myColor.r, myColor.g, myColor.b, 1f);

                startPos = msg.swipe.normalizedVector * msg.swipe.distance * ConstantsBucket.SheepThrowStrength;
                startRotation = Quaternion.Euler(0f, 0f, msg.swipe.angleEuler);

                targetDestination = startPos;
                targetRotation = startRotation;
            } else {
                startPos = transform.localPosition;
                startRotation = dirIndicator.rotation;

                targetDestination = msg.swipe.normalizedVector * msg.swipe.distance * ConstantsBucket.SheepThrowStrength;
                targetRotation = Quaternion.Euler(0f, 0f, msg.swipe.angleEuler); //rot_z - defaultSpriteAngle);
            }
            triggerMove = true;
            triggerFade = false;
            Debug.Log("swipe sets triggerFade = false");
            progressMove = 0;
            bool insideMin = !msg.swipe.isOverWheelMin;
            dirIndicator.gameObject.SetActive(!insideMin);
            if (insideMin) {
                crossSprite.color = new Color(insideWheelCol.r, insideWheelCol.g, insideWheelCol.b, 1f);
            } else {
                crossSprite.color = new Color(myColor.r, myColor.g, myColor.b, 1f);
            }
        }
    }

    void OnTap(GameMessage msg) {
        OnSwipe(msg);
        if (playfield.owner.EqualsByValue(msg.owner)) {
            tapDown = msg.state;
            if (!msg.state) {
                Debug.Log("trigger fade + pop animation, triggerFade = true");
                animator.SetTrigger("pop");
                triggerFade = true;
                progressFade = 0;
            } else {
                animator.SetTrigger("default");
                Debug.Log("OnTap default animation");
            }
        }
    }

    void Update() {
        if (triggerMove) {
            progressMove += Time.deltaTime / timeToRotate;
            if (progressMove <= 1f) {
                dirIndicator.rotation = Quaternion.Lerp(startRotation, targetRotation, progressMove);
                transform.localPosition = Vector2.Lerp(startPos, targetDestination, progressMove);

            } else {
                dirIndicator.rotation = targetRotation;
                transform.localPosition = targetDestination;
                triggerMove = false;
                progressMove = 0;
            }
        }

        if (triggerFade) {
            progressFade += Time.deltaTime / timeToFade;
            if (progressFade <= 1f) {
                indSprite.color = new Color(indSprite.color.r, indSprite.color.g, indSprite.color.b, 1f - progressFade);
                crossSprite.color = new Color(myColor.r, myColor.g, myColor.b, 1f - progressFade);
            } else {
                indSprite.color = new Color(indSprite.color.r, indSprite.color.g, indSprite.color.b, 0);
                crossSprite.color = new Color(myColor.r, myColor.g, myColor.b, 0);
                triggerFade = false;
                progressFade = 0;
            }

        }
    }
}