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
        if (msg.playfield == null) {
            if (!msg.sheepUnit.lastHandler.EqualsByValue(msg.sheepUnit.owner))
                return;
            EventCoordinator.TriggerEvent(EventName.System.Sheep.KingMissed(), GameMessage.Write().WithSheepUnit(msg.sheepUnit).WithOwner(msg.sheepUnit.lastHandler).WithPlayfield(msg.playfield));
            return;
        }
        KingUnit king = KingCoordinator.GetKing(msg.playfield);
        if (king == null) {
            return;
        }
        if (msg.sheepUnit == null) {
            Debug.Log("msg.sheepUnit == null");
            return;
        }
        if (!msg.sheepUnit.lastHandler.GetPlayerProfile().isAlive)
            return;
        if (king == msg.sheepUnit.lastHandler.GetKing())
            return;
        float distance = (king.transform.position - msg.sheepUnit.transform.position).magnitude;
        //Debug.Log("distance: " + distance + "  King Radius: " + king.GetRadius() + "  sheep radius: " + ConstantsBucket.HitRange);
        if (distance < king.GetRadius() + ConstantsBucket.HitRange) {
            msg.sheepUnit.lastHandler.GetKing().SuccesfullHit();
            king.ResetSuccesfullHits();
            EventCoordinator.TriggerEvent(EventName.System.King.Hit(), GameMessage.Write().WithSheepUnit(msg.sheepUnit).WithKingUnit(king).WithOwner(msg.sheepUnit.lastHandler).WithPlayfield(msg.playfield));
        } else {
            EventCoordinator.TriggerEvent(EventName.System.Sheep.KingMissed(), GameMessage.Write().WithSheepUnit(msg.sheepUnit).WithKingUnit(king).WithOwner(msg.sheepUnit.lastHandler).WithPlayfield(msg.playfield));
        }
    }
}