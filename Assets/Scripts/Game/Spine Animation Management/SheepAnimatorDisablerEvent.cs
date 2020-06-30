using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepAnimatorDisablerEvent : MonoBehaviour {
    Animator animator;
    void Awake() {
        if (animator == null) animator = GetComponent<Animator>();
    }
    public void AlertObservers(string message) {
        if (message.Equals("IdleAnimationEnded")) {
            animator.enabled = false;
        }
    }
}