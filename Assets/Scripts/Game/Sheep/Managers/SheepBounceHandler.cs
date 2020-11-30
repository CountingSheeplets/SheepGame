using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBounceHandler : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.System.Sheep.KingMissed(), OnMiss);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.KingMissed(), OnMiss);
    }
    void OnMiss(GameMessage msg) {
        if (msg.playfield == null)return;
        KingUnit king = KingCoordinator.GetKing(msg.playfield);
        if (msg.playfield != msg.sheepUnit.owner.GetPlayfield())
            if (msg.sheepUnit.sheepType == SheepType.Bouncy) {
                SheepFly fly = msg.sheepUnit.GetComponent<SheepFly>();
                fly.StartFlying(SpeedBucket.GetFlySpeed(msg.sheepUnit.sheepType), msg.kingUnit.transform.position);
                Debug.Log("bounce bouncy sheep");
                //play bounce sound fx, bounce animation etc.:

                //play
            }
    }
}