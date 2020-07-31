using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertObserversGeneral : MonoBehaviour {

    public delegate void OnAnimEnded(string parameterTheFunctionExpects);
    public event OnAnimEnded animEndedCallback;

    public void OnAttackAnimationEnded(string message) {
        if (message.Equals("AttackAnimationEnded")) {
            animEndedCallback("AttackAnimationEnded");
        }
    }
}