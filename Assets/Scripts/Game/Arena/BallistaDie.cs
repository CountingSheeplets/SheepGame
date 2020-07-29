using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class BallistaDie : MonoBehaviour {
    public Owner owner;
    public bool trigger;
    float alpha;
    Color color;
    Spine.Skeleton skeleton;
    Vector3 startingScale = new Vector3(1f, 1f, 1f);
    Vector3 endingScale = new Vector3(0.3f, 0.3f, 0.3f);
    float counter = 0f;

    void Awake() {
        skeleton = GetComponent<SkeletonMecanim>().skeleton;
        color = skeleton.GetColor();
        alpha = color.a + 0.1f;
    }
    void Start() {
        if (GetComponentInParent<Playfield>()) {
            owner = GetComponentInParent<Playfield>().owner;
            EventCoordinator.StartListening(EventName.System.Player.Eliminated(), OnEliminated);
        }
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Player.Eliminated(), OnEliminated);
    }
    void Update() {
        if (trigger) {
            alpha -= Time.deltaTime / ConstantsBucket.PlayfieldFadeTime;
            counter += Time.deltaTime / ConstantsBucket.PlayfieldFadeTime;
            float eased = Easing.Cubic.InOut(alpha);
            float easedCounter = Easing.Cubic.InOut(counter);
            if (alpha > 0) {
                skeleton.SetColor(new Color(color.r, color.g, color.b, eased));
                transform.localScale = Vector3.Lerp(startingScale, endingScale, easedCounter);
                //Debug.Log(skeleton.GetColor());
            } else trigger = false;
        }
    }
    void OnEliminated(GameMessage msg) {
        if (owner.EqualsByValue(msg.targetOwner)) {
            trigger = true;
        }
    }
}