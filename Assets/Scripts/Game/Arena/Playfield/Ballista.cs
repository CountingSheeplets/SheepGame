using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour {
    float defaultSpriteAngle = 90f;
    SheepThrow sheepThrow;
    Playfield playfield;
    SpriteRenderer rend;
    public Animator anim;
    bool trigger = false;
    Quaternion targetRotation;
    Quaternion startRotation;
    float progress;
    public float timeToRotate = 0.5f;
    void Start() {
        rend = GetComponentInChildren<SpriteRenderer>();
        playfield = GetComponentInParent<Playfield>();
        sheepThrow = GetComponentInParent<SheepThrow>();
        EventCoordinator.StartListening(EventName.System.Sheep.Launch(), OnSheepLaunch);
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnSheepReady);
        EventCoordinator.StartListening(EventName.Input.Swipe(), OnSwipe);
        EventCoordinator.StartListening(EventName.Input.Tap(), OnTap);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Launch(), OnSheepLaunch);
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnSheepReady);
        EventCoordinator.StopListening(EventName.Input.Swipe(), OnSwipe);
        EventCoordinator.StopListening(EventName.Input.Tap(), OnTap);
    }

    void OnSheepLaunch(GameMessage msg) {
        if (playfield == msg.playfield) {
            anim.Play("Shoot");
            //float rot_z = Mathf.Atan2(msg.swipe.vector.y, msg.swipe.vector.x) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0f, 0f, msg.swipe.angleEuler); //rot_z - defaultSpriteAngle);
            startRotation = transform.rotation;
            progress = 0;
            trigger = true;
        }
    }
    void OnTap(GameMessage msg) {
        OnSheepLaunch(msg);
    }
    void OnSwipe(GameMessage msg) {
        //Debug.Log("on swipe: " + playfield.owner + " vs " + msg.owner);
        if (playfield.owner.EqualsByValue(msg.owner)) {
            //transform.rotation = Quaternion.Euler(0f, 0f, msg.swipe.angleEuler);
            targetRotation = Quaternion.Euler(0f, 0f, msg.swipe.angleEuler); //rot_z - defaultSpriteAngle);
            startRotation = transform.rotation;
            progress = 0;
            trigger = true;
        }
    }

    void OnSheepReady(GameMessage msg) {
        if (playfield == msg.playfield) {
            anim.Play("Pull");
        }
    }
    void Update() {
        if (trigger) {
            progress += Time.deltaTime / timeToRotate;
            if (progress <= 1f)
                transform.rotation = Quaternion.Lerp(startRotation, targetRotation, progress);
            else {
                transform.rotation = targetRotation;
                trigger = false;
                progress = 0;
            }
        }
    }
}