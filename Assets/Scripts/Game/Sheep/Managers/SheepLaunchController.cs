using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepLaunchController : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.System.Sheep.Launch(), OnLaunch);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Launch(), OnLaunch);
    }
    void OnLaunch(GameMessage msg) {
        SheepUnit sheep = msg.sheepUnit;
        SheepFly fly = sheep.gameObject.GetComponent<SheepFly>();
        //Debug.Log(msg.swipe);
        Vector2 destination = msg.swipe.vector * msg.swipe.distance * ConstantsBucket.SheepThrowStrength / 10f + (Vector2) fly.transform.position;
        float speed = SpeedBucket.GetFlySpeed(sheep.sheepType);
        //Debug.Log("speed fly:"+speed);
        fly.StartFlying(speed, destination);
        sheep.ResetContainer();
    }
}