using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using UnityEngine;
public class KingAnimateDeath : MonoBehaviour {
    public Owner owner;
    public Animator anim;
    bool trigger;
    Spine.Skeleton skeleton;
    Color color;
    float alpha;
    void Awake() {
        skeleton = GetComponent<SkeletonMecanim>().skeleton;
        color = skeleton.GetColor();
        alpha = color.a + 0.1f;
        Debug.Log(skeleton.GetColor());
    }
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
            trigger = true;
        }
        List<BaseUnitMove> moves = new List<BaseUnitMove>(GetComponentsInParent<BaseUnitMove>().Where(x => !(x is FieldFloat)));
        foreach (BaseUnitMove move in moves) {
            Destroy(move);
        }
    }
    void Update() {
        if (trigger) {
            alpha -= Time.deltaTime / ConstantsBucket.PlayfieldFadeTime;
            float eased = Easing.Cubic.InOut(alpha);
            if (alpha > 0) {
                skeleton.SetColor(new Color(color.r, color.g, color.b, eased));
                Debug.Log(skeleton.GetColor());
            } else trigger = false;
        }
    }
}