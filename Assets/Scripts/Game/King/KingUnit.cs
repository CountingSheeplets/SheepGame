using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingUnit : MonoBehaviour {
    public SheepType sheepType;
    public Owner owner;
    public bool canBeThrown = true;
    private Playfield _myPlayfield;
    public Playfield myPlayfield {
        get {
            if (_myPlayfield == null)
                _myPlayfield = GetComponentInParent<Playfield>();
            return _myPlayfield;
        }
    }
    bool isUsingAbility = false;
    public void SetUsingAbility() {
        isUsingAbility = true;
    }
    public bool GetIsUsingAbility() {
        return isUsingAbility;
    }
    public void StopUsingAbility() {
        isUsingAbility = false;
    }
    public bool isRoaming = false;
    int succesfullHits = 0;
    public int GetSuccesfullHits() {
        return succesfullHits;
    }
    public void SuccesfullHit() {
        succesfullHits++;
        GetComponent<KingChangeSize>().StartIncreasingSize(succesfullHits);
    }
    public void ResetSuccesfullHits() {
        succesfullHits = 0;
        GetComponent<KingChangeSize>().StartResetingSize();
    }
    public float GetRadius() {
        return ConstantsBucket.KingHitRadius * Mathf.Pow(1 + ConstantsBucket.KingHitRadiusIncrement, succesfullHits);
    }

    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float currentDamage = 0f;
    public float GetHealth {
        get { return maxHealth - currentDamage; }
    }
    public float GetMaxHealth {
        get { return maxHealth; }
    }
    public float GetDamage {
        get { return currentDamage; }
    }
    public delegate void OnReceivedDamage(float dmg);
    public OnReceivedDamage onReceivedDamage;

    void Start() {
        //Debug.Log("pre sclae:"+transform.localScale);
        EventCoordinator.StartListening(EventName.System.King.Hit(), OnHit);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.Hit(), OnHit);
    }

    void OnHit(GameMessage msg) {
        if (msg.kingUnit == this) {
            //if target owner == damaged owner. throw msg "king is starving!"
            DealDamage(ConstantsBucket.HitDamage);
            //onReceivedDamage(10); move this to messaging system
            TryDie(msg.owner, owner);
        }
    }
    public void OnStarve() {
        DealDamage(ConstantsBucket.StarveDamage);
        TryDie(null, owner);
    }
    public void DealDamage(float amount) {
        currentDamage += amount;
        EventCoordinator.TriggerEvent(EventName.System.King.ReceivedDamage(), GameMessage.Write().WithKingUnit(this).WithFloatMessage(amount));
    }
    void TryDie(Owner killer, Owner eliminated) {
        if (GetHealth <= 0) {
            //show die animation

            //then remove king
            EventCoordinator.TriggerEvent(EventName.System.King.Killed(), GameMessage.Write().WithKingUnit(this).WithOwner(killer).WithTargetOwner(eliminated));
            //Destroy(gameObject, 2f); //destroy GO after animations have played out
            Destroy(gameObject, 2f); //destroy this instantly, so that wouldnt interfere with other mechanics
            //then with OnDestroy have death animations created via KingFactory
            Animator anim = GetComponentInChildren<Animator>();
            foreach (AnimatorControllerParameter parameter in anim.parameters) {
                anim.ResetTrigger(parameter.name);
            }
            anim.SetTrigger("die");
        }
    }
}