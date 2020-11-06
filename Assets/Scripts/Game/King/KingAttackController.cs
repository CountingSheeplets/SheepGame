using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class KingAttackController : MonoBehaviour {
    public List<SheepUnit> sheepInField = new List<SheepUnit>();
    public Playfield playfield;
    public SheepUnit nextTarget = null;
    KingCharge kingCharge;
    float kingAttackDistance = 0.5f;
    void Start() {
        kingCharge = GetComponent<KingCharge>();
        kingCharge.animEndedCallback += OnAttackAnimationEnded;
        EventCoordinator.StartListening(EventName.System.Sheep.Land(), OnLand);
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnSmashed);
    }
    void OnDestroy() {
        kingCharge.animEndedCallback -= OnAttackAnimationEnded;
        EventCoordinator.StopListening(EventName.System.Sheep.Land(), OnLand);
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnSmashed);
    }
    void OnLand(GameMessage msg) {
        if (playfield == null)playfield = GetComponentInParent<Playfield>();
        if (msg.playfield != null) {
            //Debug.Log(msg.playfield + " landed in my field: " + playfield);
            if (msg.playfield == playfield && playfield.owner.GetPlayerProfile().isAlive) {
                //Debug.Log("landed in my field: " + gameObject.name);
                if (!msg.sheepUnit.owner.EqualsByValue(playfield.owner)) {
                    //Debug.Log("I am not owner: " + gameObject.name);
                    if (!sheepInField.Contains(msg.sheepUnit) && msg.sheepUnit.sheepType != SheepType.Trench) {
                        sheepInField.Add(msg.sheepUnit);
                    }
                }
                if (!kingCharge.isCharging)
                    ChargeNextTarget();
                else {
                    Debug.Log("OnLand wont charge, because already charging");
                }
            } else {
                if (sheepInField.Contains(msg.sheepUnit)) {
                    sheepInField.Remove(msg.sheepUnit);
                }
            }
        }
    }

    //animation is played inside KingCharge on Post function. This function is attached to event on anim
    public void OnAttackAnimationEnded(string message) {
        //Debug.Log("OnAttackAnimationEnded");
        kingCharge.isCharging = false;
        if (nextTarget != null)
            LaunchSheep(nextTarget);
        ChargeNextTarget();
    }

    void OnSmashed(GameMessage msg) {
        if (playfield == null)playfield = GetComponentInParent<Playfield>();
        if (msg.owner.EqualsByValue(playfield.owner)) {
            foreach (SheepUnit sheep in msg.sheepUnits)
                sheepInField.Remove(sheep);
            //sheepInField = sheepInField.Where(item => item != null).Where(sheep => sheep.currentPlayfield == playfield).ToList();
            if (!kingCharge.isCharging)
                ChargeNextTarget();
        }
    }
    void LaunchSheep(SheepUnit sheep) {
        SheepFly fly = sheep.gameObject.GetComponent<SheepFly>();
        Vector2 direction = (sheep.transform.localPosition - transform.localPosition).normalized;
        float sheepEffect = KnockDistanceBucket.GetKnockStrength(sheep.sheepType);
        float kingSizeEffect = Mathf.Sqrt(GoldRewardCoordinator.GetComboLevel(playfield.owner) + 1);
        Vector2 destination = sheepEffect * kingSizeEffect * direction * ConstantsBucket.SheepKickStrength / 1f + (Vector2)fly.transform.position;
        float speed = SpeedBucket.GetFlySpeed(sheep.sheepType) / 3f;
        fly.sheep.lastHandler = playfield.owner;
        fly.StartFlying(speed, destination);
        sheep.ResetContainer();
    }
    void ChargeNextTarget() {
        if (sheepInField.Count == 0)
            //Debug.Log("nothing to charge at... Count = 0");
            return;
        if (sheepInField.Count == 1)
            nextTarget = sheepInField[0];
        else
            nextTarget = sheepInField.OrderBy(x => (x.transform.localPosition - transform.localPosition).magnitude).FirstOrDefault();
        //Debug.Log("charge at sheep: " + nextTarget + " sheep playfield: " + nextTarget.currentPlayfield);
        if (nextTarget != null) {
            sheepInField.Remove(nextTarget);
            Vector3 stopOffset = (nextTarget.transform.localPosition - transform.localPosition).normalized * kingAttackDistance;
            kingCharge.StartCharging(nextTarget.transform.position - stopOffset);
        } // else Debug.Log("nothing to charge at... nextTarget = null");
    }
}