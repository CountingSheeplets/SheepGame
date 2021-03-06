﻿using System.Collections;
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
    bool _isVulnerable = false;
    public delegate void OnStateChange(bool state);
    public OnStateChange onVulnerabilityChange;
    public bool isVulnerable {
        get { return _isVulnerable; }
        set {
            _isVulnerable = value;
            if (onVulnerabilityChange != null)
                onVulnerabilityChange(_isVulnerable);
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
    KingModel _model;
    public KingModel GetModel() {
        if (_model != null)
            return _model;
        else
            return _model = GetComponentInChildren<KingModel>();
    }
    void Start() {
        EventCoordinator.StartListening(EventName.System.King.Hit(), OnHit);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.Hit(), OnHit);
    }

    void OnHit(GameMessage msg) {
        if (msg.kingUnit == this) {
            TryDie(msg.owner, owner);
        }
    }

    void TryDie(Owner killer, Owner eliminated) {
        if (isVulnerable) {
            isVulnerable = false;
            //then remove king
            EventCoordinator.TriggerEvent(EventName.System.King.Killed(), GameMessage.Write().WithKingUnit(this).WithOwner(killer).WithTargetOwner(eliminated));
            Destroy(gameObject, 2f); //destroy this instantly, so that wouldnt interfere with other mechanics
            //show die animation
            Animator anim = GetComponentInChildren<Animator>();
            foreach (AnimatorControllerParameter parameter in anim.parameters) {
                if (parameter.type == AnimatorControllerParameterType.Trigger)
                    anim.ResetTrigger(parameter.name);
            }
            anim.SetTrigger("die");
        }
    }
}