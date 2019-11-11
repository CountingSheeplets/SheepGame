using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SheepUnit : MonoBehaviour
{
    public Owner owner;
    public bool canBeThrown = true;
    public Playfield currentPlayfield;
    public bool isReadying;
    public bool isReadyToFly = false;
    public bool isFlying = false;
    public bool isSwimming = false;
    public bool isRoaming = false;
    public float radius = 0.5f;

    void Start(){
        EventManager.StartListening(EventName.System.Sheep.Kill(), OnKill);
    }
    void OnDestroy(){
        EventManager.StopListening(EventName.System.Sheep.Kill(), OnKill);
    }
    void OnKill(GameMessage msg){
        if(msg.sheepUnit == this){
            //show drown animation

            //then remove sheep
            Destroy(gameObject, 1f);
        }
    }

}
