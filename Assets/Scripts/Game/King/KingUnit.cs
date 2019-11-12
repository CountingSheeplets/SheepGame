using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingUnit : MonoBehaviour
{
    public Owner owner;
    public bool canBeThrown = true;
    private Playfield _myPlayfield;
    public Playfield myPlayfield {
        get{
            if(_myPlayfield == null)
                _myPlayfield = GetComponentInParent<Playfield>();
            return _myPlayfield;
        }
    }
    public bool isUsingAbility = false;
    public bool isRoaming = false;
    public float radius = 0.5f;
    private float health = 100f;
    public float GetHealth {
        get {return health;}
    }

    void Start(){
        EventManager.StartListening(EventName.System.King.Hit(), OnHit);
    }
    void OnDestroy(){
        EventManager.StopListening(EventName.System.King.Hit(), OnHit);
    }

    void OnHit(GameMessage msg){
        DealDamage(10);
        TryDie(msg.owner, owner);
    }

    public void DealDamage(float amount){
        health -= amount;
    }
    void TryDie(Owner killer, Owner eliminated){
        if(health <= 0){
            //show die animation

            //then remove king
            EventManager.TriggerEvent(EventName.System.King.Killed(), GameMessage.Write().WithKingUnit(this).WithOwner(killer).WithTargetOwner(eliminated));
            Destroy(gameObject, 2f); //destroy GO after animations have played out
            Destroy(this); //destroy this instantly, so that wouldnt interfere with other mechanics
        }
    }
}
