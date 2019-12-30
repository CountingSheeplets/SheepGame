using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour
{
    float defaultSpriteAngle = 90f;
    SheepThrow sheepThrow;
    Playfield playfield;
    SpriteRenderer rend;
    public Sprite loaded;
    public Sprite empty;
    void Start()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
        playfield = GetComponentInParent<Playfield>();
        sheepThrow = GetComponentInParent<SheepThrow>();
        EventCoordinator.StartListening(EventName.System.Sheep.Launch(), OnSheepLaunch);
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnSheepReady);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnGameStart);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Sheep.Launch(), OnSheepLaunch);
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnSheepReady);
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnGameStart);
    }
    void OnGameStart(GameMessage msg){
        rend.enabled = true;
    }
    void OnSheepLaunch(GameMessage msg)
    {
        if(playfield == msg.playfield){
            rend.sprite = empty;
            float rot_z = Mathf.Atan2(msg.swipe.vector.y, msg.swipe.vector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - defaultSpriteAngle);
        }
    }
    void OnSheepReady(GameMessage msg){
        if(playfield == msg.playfield){
            rend.sprite = loaded;
        }
        //transform.rotation.SetLookRotation(Vector2.up);
    }
}
