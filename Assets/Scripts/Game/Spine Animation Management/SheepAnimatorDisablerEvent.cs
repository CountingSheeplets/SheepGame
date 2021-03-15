// using System.Collections;
// using System.Collections.Generic;
// using Spine.Unity;
// using UnityEngine;
// public class SheepAnimatorDisablerEvent : MonoBehaviour {
//     Animator animator;
//     SkeletonMecanim mecanim;
//     void Awake() {
//         if (animator == null) animator = GetComponent<Animator>();
//         if (mecanim == null) mecanim = GetComponent<SkeletonMecanim>();
//     }
//     public void AlertObservers(string message) {
//         if (message.Equals("IdleAnimationEnded")) {
//             SkeletonRendererController.MakeSheepIdle(mecanim);
//             //mecanim.enabled = false;
//         }
//     }
// }