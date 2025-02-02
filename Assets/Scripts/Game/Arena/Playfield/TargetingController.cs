using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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
    bool isTouching = false;
    bool isInsideBlockRange = false;
    Color myColor;
    public Color insideWheelCol = Color.gray;
    Color colorToSet;

    float fadeTimeout = 1f;
    bool triggerTimeout = false;

    void Start() {
        playfield = GetComponentInParent<Playfield>();
        indSprite = dirIndicator.GetComponentInChildren<SpriteRenderer>();
        crossSprite = crosshair.GetComponentInChildren<SpriteRenderer>();
        EventCoordinator.StartListening(EventName.Input.Swipe(), OnSwipe);
        EventCoordinator.StartListening(EventName.Input.Tap(), OnTap);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStart);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.Swipe(), OnSwipe);
        EventCoordinator.StopListening(EventName.Input.Tap(), OnTap);
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStart);
    }
    void OnStart(GameMessage msg) {
        myColor = playfield.owner.GetPlayerProfile().playerColor;
        colorToSet = myColor;
        crossSprite.color = myColor;
        indSprite.color = myColor;

        crossSprite.color = new Color(myColor.r, myColor.g, myColor.b, 0f);
        indSprite.color = new Color(myColor.r, myColor.g, myColor.b, 0f);
    }

    void OnTap(GameMessage msg) {
        if (playfield.owner.EqualsByValue(msg.owner)) {
            //if (!isTouching)
            //    InitialMarkerMove(msg.swipe);
            //ChangeMarkerState(msg.state != 0);
            animator.SetTrigger("pop");
            //SetGrayColor();
        }
    }
    void OnSwipe(GameMessage msg) {
        if (playfield.owner.EqualsByValue(msg.owner)) {
            //if (isTouching) {
            MoveMarker(msg.swipe);
            SetGrayColor();
            if(msg.swipe.rawVector.magnitude > 0f) {
                fadeTimeout = 1f;
                FullyShow();
            } else {
                triggerTimeout = true;
            }
            //}
        }
    }
    void InitialMarkerMove(Swipe swipe) {
        startPos = swipe.normalizedVector * swipe.distance * ConstantsBucket.SheepThrowStrength;
        startRotation = Quaternion.Euler(0f, 0f, swipe.angleEuler);
        targetDestination = startPos;
        targetRotation = startRotation;
        triggerMove = true;
        progressMove = 0;

        isInsideBlockRange = !swipe.isOverWheelMin;
        dirIndicator.gameObject.SetActive(!isInsideBlockRange);
    }
    void MoveMarker(Swipe swipe) {
        startPos = transform.localPosition;
        startRotation = dirIndicator.rotation;

        targetDestination = swipe.rawVector * ConstantsBucket.SwipeDistanceMax;
        targetRotation = Quaternion.Euler(0f, 0f, swipe.angleEuler); //rot_z - defaultSpriteAngle);
        triggerMove = true;

        progressMove = 0;
        isInsideBlockRange = !swipe.isOverWheelMin;
        dirIndicator.gameObject.SetActive(!isInsideBlockRange);
    }

    void ChangeMarkerState(bool isDown) {
        isTouching = isDown;
        if (!isDown) {
            animator.SetTrigger("pop");
            StartFading();
        } else {
            animator.SetTrigger("default");
            FullyShow();
        }
    }
    void StartFading() {
        triggerFade = true;
        progressFade = 0;
    }
    void FullyShow() {
        if (triggerFade)
            animator.SetTrigger("default");
        triggerFade = false;
        triggerTimeout = false;
        progressFade = 0;
        UpdateFadeState();
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
        if (!triggerFade && triggerTimeout)
            fadeTimeout -= Time.deltaTime;
        if (fadeTimeout <= 0) {
            fadeTimeout = 1f;
            StartFading();
        }

        if (triggerFade) {
            progressFade += Time.deltaTime / timeToFade;
            UpdateFadeState();
        }
    }
    void UpdateFadeState() {
        if (progressFade <= 1f) {
            indSprite.color = new Color(indSprite.color.r, indSprite.color.g, indSprite.color.b, 1f - progressFade);
            crossSprite.color = new Color(colorToSet.r, colorToSet.g, colorToSet.b, 1f - progressFade);
        } else {
            indSprite.color = new Color(indSprite.color.r, indSprite.color.g, indSprite.color.b, 0);
            crossSprite.color = new Color(colorToSet.r, colorToSet.g, colorToSet.b, 0);
            triggerFade = false;
        }
    }
    void SetGrayColor() {
        if (isInsideBlockRange)
            colorToSet = insideWheelCol;
        else
            colorToSet = myColor;
        crossSprite.color = new Color(colorToSet.r, colorToSet.g, colorToSet.b, 1f - progressFade);
    }
}