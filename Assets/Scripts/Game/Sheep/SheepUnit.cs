using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SheepUnit : MonoBehaviour
{
    public Owner owner;
    public bool canBeThrown = true;
    public Playfield currentPlayfield;
    public bool isReadying;
    public bool isSwimming = false;

    void Start(){
        EventManager.StartListening(EventName.System.Sheep.Kill(), OnKill);
    }

    void OnKill(GameMessage msg){
        if(msg.sheepUnit == this){
            Destroy(gameObject);
        }
    }
}
