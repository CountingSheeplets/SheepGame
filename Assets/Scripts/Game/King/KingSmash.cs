using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KingSmash : MonoBehaviour {
    Owner owner;
    public float flyDistance = 2f;
    KingUnit king;
    void Start() {
        if (king == null)king = GetComponent<KingUnit>();
        if (owner == null)owner = king.owner;
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnSmashed);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnSmashed);
    }
    void OnSmashed(GameMessage msg) {
        if (owner.EqualsByValue(msg.owner)) {
            SendSheepFly(msg.sheepUnits);
        }
    }

    void SendSheepFly(List<SheepUnit> sheeps) {

        Debug.Log("sheeps: " + sheeps.Count);
        foreach (SheepUnit sheep in sheeps) {
            SheepFly fly = sheep.GetComponent<SheepFly>();
            Vector2 destination = GetDestinatinoVector(sheep.transform);
            sheep.lastHandler = king.owner;
            fly.StartFlying(SpeedBucket.GetKnockbackSpeed(sheep.sheepType), destination);
        }
    }
    Vector2 GetDestinatinoVector(Transform sheepTr) {
        Vector2 direction = (sheepTr.position - transform.position).normalized;
        //Debug.Log("SMASH: king:"+transform.position+" sheep:"+sheepTr.position+"  dir:"+direction);
        Vector2 destination = direction * flyDistance;
        return destination + (Vector2)sheepTr.position;
    }
}