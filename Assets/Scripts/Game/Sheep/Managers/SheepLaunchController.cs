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
        //Vector2 destination = msg.swipe.normalizedVector * msg.swipe.distance * ConstantsBucket.SheepThrowStrength + (Vector2)fly.transform.position;
        Vector2 destination = msg.coordinates;// + (Vector2)fly.transform.position;
        float speed = SpeedBucket.GetFlySpeed(sheep.sheepType);
        fly.StartFlying(speed, destination);
        sheep.ResetContainer();
    }
}