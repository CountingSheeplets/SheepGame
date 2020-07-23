using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour {
    float defaultSpriteAngle = 90f;
    SheepThrow sheepThrow;
    Playfield playfield;
    SpriteRenderer rend;
    //public Sprite loaded;
    //public Sprite empty;
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
        EventCoordinator.StartListening(EventName.System.Player.Eliminated(), OnEliminated);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Launch(), OnSheepLaunch);
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnSheepReady);
        EventCoordinator.StopListening(EventName.System.Player.Eliminated(), OnEliminated);
    }

    void OnSheepLaunch(GameMessage msg) {
        if (playfield == msg.playfield) {
            //anim.SetTrigger("shoot");
            anim.Play("Shoot");
            float rot_z = Mathf.Atan2(msg.swipe.vector.y, msg.swipe.vector.x) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0f, 0f, rot_z - defaultSpriteAngle);
            startRotation = transform.rotation;
            progress = 0;
            trigger = true;
        }
    }
    void OnEliminated(GameMessage msg) {
        if (playfield.owner == msg.targetOwner) {
            anim.SetTrigger("die");
        }
    }
    void OnSheepReady(GameMessage msg) {
        if (playfield == msg.playfield) {
            //anim.SetTrigger("load");
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