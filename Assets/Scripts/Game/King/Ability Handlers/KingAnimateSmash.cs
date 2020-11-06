using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAnimateSmash : MonoBehaviour {
    KingUnit king;
    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
        king = GetComponentInParent<KingUnit>();
        EventCoordinator.StartListening(EventName.System.King.StartSmash(), OnStartSmash);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.StartSmash(), OnStartSmash);
    }
    void OnStartSmash(GameMessage msg) {
        foreach (AnimatorControllerParameter parameter in anim.parameters) {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
                anim.ResetTrigger(parameter.name);
        }
        if (msg.kingUnit == king) {
            anim.SetTrigger("smash");
        }
    }
}