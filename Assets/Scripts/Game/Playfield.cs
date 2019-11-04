using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public Playfield Init(){
        GameObject newPlayfieldGO = Instantiate(gameObject);
        Playfield playfield = newPlayfieldGO.GetComponent<Playfield>();
        return playfield;
    }

    void Awake(){
        EventManager.StartListening(EventName.Input.Swipe(), OnSwipe);
    }

    void OnSwipe(GameMessage msg){
        Debug.Log("swiping:"+msg.swipe.vector);
    }
}
