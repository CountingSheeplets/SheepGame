using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDestructionHandler : MonoBehaviour {
    float timeToDestroy = 2f;
    float progress = 1f;
    bool trigger = false;
    Owner ownerBeingDestroyed;
    void Start() {
        timeToDestroy = ConstantsBucket.PlayfieldFadeTime;
        EventCoordinator.StartListening(EventName.System.Player.Eliminated(), OnPlayerEliminated);
    }
    void OnDesstroy() {
        EventCoordinator.StopListening(EventName.System.Player.Eliminated(), OnPlayerEliminated);
    }
    void OnPlayerEliminated(GameMessage msg) {
        ownerBeingDestroyed = msg.targetOwner;
        EventCoordinator.TriggerEvent(EventName.System.Environment.DestroyArena(), GameMessage.Write().WithFloatMessage(timeToDestroy).WithTargetOwner(ownerBeingDestroyed));
        progress = 1f;
        trigger = true;
    }

    void Update() {
        if (trigger) {
            progress -= Time.deltaTime / timeToDestroy;
            if (progress <= 0) {
                progress = 1f;
                EventCoordinator.TriggerEvent(EventName.System.Environment.ArenaDestroyed(), GameMessage.Write().WithTargetOwner(ownerBeingDestroyed));
                trigger = false;
            }
        }
    }
}