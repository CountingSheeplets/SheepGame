using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class KingAttackController : MonoBehaviour {
    List<SheepUnit> sheepInField = new List<SheepUnit>();
    public Playfield playfield;
    SheepUnit nextTarget = null;
    KingCharge kingCharge;
    float kingAttackDistance = 0.5f;
    AlertObserversGeneral observersGeneral;
    void Start() {
        kingCharge = GetComponent<KingCharge>();
        observersGeneral = GetComponentInChildren<AlertObserversGeneral>();
        observersGeneral.animEndedCallback += OnAttackAnimationEnded;
        EventCoordinator.StartListening(EventName.System.Sheep.Land(), OnLand);
        //EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Land(), OnLand);
        //EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnStartGame(GameMessage msg) {
        //king here does not get component.need to fix it
    }
    void OnLand(GameMessage msg) {
        if (playfield == null) playfield = GetComponentInParent<Playfield>();
        if (msg.playfield != null) {
            //Debug.Log(msg.playfield + " landed in my field: " + playfield);
            if (msg.playfield == playfield) {
                Debug.Log("landed in my field: " + gameObject.name);
                if (!msg.sheepUnit.owner.EqualsByValue(playfield.owner)) {
                    Debug.Log("I am not owner: " + gameObject.name);
                    if (!sheepInField.Contains(msg.sheepUnit)) {
                        sheepInField.Add(msg.sheepUnit);
                    }
                }
                ChargeNextTarget();
            } else {
                if (sheepInField.Contains(msg.sheepUnit)) {
                    sheepInField.Remove(msg.sheepUnit);
                }
            }
        }
    }

    //animation is played inside KingCharge on Post function. This function is attached to event on anim
    public void OnAttackAnimationEnded(string message) {
        if (nextTarget != null)
            LaunchSheep(nextTarget);
        ChargeNextTarget();
    }

    void LaunchSheep(SheepUnit sheep) {
        SheepFly fly = sheep.gameObject.GetComponent<SheepFly>();
        //Debug.Log(msg.swipe);
        Vector2 direction = (sheep.transform.localPosition - transform.localPosition).normalized;
        float sheepEffect = KnockDistanceBucket.GetKnockStrength(sheep.sheepType);
        float kingSizeEffect = Mathf.Sqrt(GoldRewardCoordinator.GetComboLevel(playfield.owner) + 1);
        Vector2 destination = sheepEffect * kingSizeEffect * direction * ConstantsBucket.SheepKickStrength / 1f + (Vector2) fly.transform.position;
        float speed = SpeedBucket.GetFlySpeed(sheep.sheepType);
        Debug.Log("speed kicked:" + speed);
        fly.sheep.lastHandler = playfield.owner;
        fly.StartFlying(speed, destination);
        sheep.ResetContainer();
    }
    void ChargeNextTarget() {
        if (sheepInField.Count == 0) {
            Debug.Log("nothing to charge at...");
            return;
        } else {
            if (sheepInField.Count == 1)
                nextTarget = sheepInField[0];
            else
                nextTarget = sheepInField.OrderBy(x => (x.transform.localPosition - transform.localPosition)).FirstOrDefault();
        }
        Debug.Log("charge at sheep: " + nextTarget + " sheep playfield: " + nextTarget.currentPlayfield);
        if (nextTarget != null) {
            sheepInField.Remove(nextTarget);
            Vector3 stopOffset = (nextTarget.transform.localPosition - transform.localPosition).normalized * kingAttackDistance;
            Debug.Log("stopOffset" + stopOffset);
            Debug.Log("StartCharging" + (nextTarget.transform.position - stopOffset));
            kingCharge.StartCharging(nextTarget.transform.position - stopOffset);

        } else Debug.Log("nothing to charge at...");
    }
}