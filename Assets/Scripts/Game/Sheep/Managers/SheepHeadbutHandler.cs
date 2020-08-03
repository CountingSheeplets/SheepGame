using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHeadbutHandler : MonoBehaviour {

    void Start() {
        EventCoordinator.StartListening(EventName.System.Sheep.Land(), OnLand);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Land(), OnLand);
    }

    void OnLand(GameMessage msg) {
        KingUnit king = KingCoordinator.GetKing(msg.playfield);
        if (king == null) {
            //Debug.Log("king == null");
            return;
        }
        if (msg.sheepUnit == null) {
            Debug.Log("msg.sheepUnit == null");
            return;
        }
        if (king == msg.sheepUnit.lastHandler.GetKing())
            return;
        float distance = (king.transform.position - msg.sheepUnit.transform.position).magnitude;
        //Debug.Log("distance: " + distance + "  King Radius: " + king.GetRadius() + "  sheep radius: " + ConstantsBucket.HitRange);
        if (distance < king.GetRadius() + ConstantsBucket.HitRange) {
            msg.sheepUnit.lastHandler.GetKing().SuccesfullHit();
            king.ResetSuccesfullHits();
            EventCoordinator.TriggerEvent(EventName.System.King.Hit(), GameMessage.Write().WithSheepUnit(msg.sheepUnit).WithKingUnit(king).WithOwner(msg.sheepUnit.lastHandler));
        } else {
            EventCoordinator.TriggerEvent(EventName.System.Sheep.KingMissed(), GameMessage.Write().WithSheepUnit(msg.sheepUnit).WithKingUnit(king).WithOwner(msg.sheepUnit.lastHandler));
        }
        if (msg.playfield != msg.sheepUnit.owner.GetPlayfield())
            if (msg.sheepUnit.sheepType == SheepType.Bouncy) {
                SheepFly fly = msg.sheepUnit.GetComponent<SheepFly>();
                fly.StartFlying(SpeedBucket.GetFlySpeed(msg.sheepUnit.sheepType), king.transform.position);
                Debug.Log("bounce bouncey sheep");
                //play bounce sound fx, bounce animation etc.:

                //play
            }
    }
}