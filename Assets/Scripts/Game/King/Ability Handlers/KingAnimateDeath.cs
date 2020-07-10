using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAnimateDeath : MonoBehaviour {
    public Owner owner;
    public Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
        if (GetComponentInParent<KingUnit>()) {
            owner = GetComponentInParent<KingUnit>().owner;
            EventCoordinator.StartListening(EventName.System.Player.Eliminated(), OnEliminated);
        }
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Player.Eliminated(), OnEliminated);
    }
    void OnEliminated(GameMessage msg) {
        foreach (AnimatorControllerParameter parameter in anim.parameters) {
            anim.ResetTrigger(parameter.name);
        }
        Debug.Log("king received the owner: " + msg.targetOwner);
        Debug.Log("my owner: " + owner);
        if (owner.EqualsByValue(msg.targetOwner)) {
            anim.SetTrigger("die");
        }
        List<BaseUnitMove> moves = new List<BaseUnitMove>(GetComponentsInParent<BaseUnitMove>());
        foreach (BaseUnitMove move in moves) {
            Destroy(move);
        }

    }
}