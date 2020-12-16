using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfieldTerrainDestructionHandler : MonoBehaviour {
    bool trigger;
    float timeToDestroy = 0.5f;
    float visibility = 0f;
    SpriteRenderer rend;
    Owner owner;
    void Start() {
        EventCoordinator.StartListening(EventName.System.Environment.DestroyArena(), OnDestroyArena);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
        rend = GetComponent<SpriteRenderer>();
        owner = GetComponentInParent<Owner>();
        rend.material.SetFloat("_RandomSeed", Random.Range(0, 10));
        rend.material.SetFloat("_CrackFill", 1);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Environment.DestroyArena(), OnDestroyArena);
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnStartGame(GameMessage msg) {
        rend.material.SetFloat("_CrackFill", 0);
    }
    void OnDestroyArena(GameMessage msg) {
        if (!owner.EqualsByValue(msg.targetOwner))
            return;
        timeToDestroy = ConstantsBucket.PlayfieldFadeTime;
        trigger = true;
        visibility = 0f;
        rend.material.SetFloat("_CrackFill", 0);
    }
    void Update() {
        if (trigger) {
            visibility += Time.deltaTime / timeToDestroy;
            if (visibility <= 1)
                rend.material.SetFloat("_CrackFill", visibility);
            else {
                rend.material.SetFloat("_CrackFill", 1);
                trigger = false;
            }
        }
    }
}