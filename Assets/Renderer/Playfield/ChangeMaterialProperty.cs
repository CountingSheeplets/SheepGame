using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialProperty : MonoBehaviour {
    bool trigger;
    float timeToDestroy = 0.5f;
    float visibility = 1f;
    SpriteRenderer rend;
    public Owner owner;
    void Start() {
        EventCoordinator.StartListening(EventName.System.Environment.DestroyArena(), OnDestroyArena);
        rend = GetComponent<SpriteRenderer>();
        owner = GetComponentInParent<Owner>();
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Environment.DestroyArena(), OnDestroyArena);
    }
    void OnDestroyArena(GameMessage msg) {
        if (!owner.EqualsByValue(msg.targetOwner))
            return;
        timeToDestroy = msg.floatMessage;
        trigger = true;
        visibility = 1f;
        rend.material.SetFloat("_CurrentLevel", 1);
    }
    void Update() {
        if (trigger) {
            visibility -= Time.deltaTime / timeToDestroy;
            if (visibility >= 0)
                rend.material.SetFloat("_CurrentLevel", visibility);
            else {
                rend.material.SetFloat("_CurrentLevel", 0);
                trigger = false;
            }
        }
    }
}