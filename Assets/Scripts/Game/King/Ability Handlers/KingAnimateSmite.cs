using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAnimateSmite : MonoBehaviour {
    KingUnit king;
    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
        king = GetComponentInParent<KingUnit>();
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnSmited);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnSmited);
    }
    void OnSmited(GameMessage msg) {
        foreach (AnimatorControllerParameter parameter in anim.parameters) {
            anim.ResetTrigger(parameter.name);
        }
        if (msg.kingUnit == king) {
            anim.SetTrigger("smite");
        }
    }
}