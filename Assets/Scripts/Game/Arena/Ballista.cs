using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour
{
    float defaultSpriteAngle = 90f;
    SheepThrow sheepThrow;
    Playfield playfield;
    SpriteRenderer rend;
    //public Sprite loaded;
    //public Sprite empty;
    public Animator anim;
    void Start()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
        playfield = GetComponentInParent<Playfield>();
        sheepThrow = GetComponentInParent<SheepThrow>();
        EventCoordinator.StartListening(EventName.System.Sheep.Launch(), OnSheepLaunch);
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnSheepReady);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Sheep.Launch(), OnSheepLaunch);
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnSheepReady);
    }

    void OnSheepLaunch(GameMessage msg)
    {
        if(playfield == msg.playfield){
            //rend.sprite = empty;
            //anim.SetTrigger("shoot");
            anim.Play("Shoot");
            Debug.Log("shoot");
            float rot_z = Mathf.Atan2(msg.swipe.vector.y, msg.swipe.vector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - defaultSpriteAngle);
        }
    }
    void OnSheepReady(GameMessage msg){
        if(playfield == msg.playfield){
            //rend.sprite = loaded;
            //anim.SetTrigger("load");
            anim.Play("Pull");
            Debug.Log("load");
        }
        //transform.rotation.SetLookRotation(Vector2.up);
    }
}
