using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PlayfieldKillUnitsOnDestroy : MonoBehaviour {
    public Owner myOwner;
    bool trigger;
    float progress = 0f;
    float destrucionTime;
    int counter = 0;
    int amount = 0;

    List<OrderObject> orderedSheep = new List<OrderObject>();

    void Start() {
        EventCoordinator.StartListening(EventName.System.Environment.DestroyArena(), OnDestroyPlayfield);
        myOwner = GetComponent<Playfield>().owner;
    }
    void OnDestroyPlayfield(GameMessage msg) {
        if (!myOwner.EqualsByValue(msg.targetOwner))
            return;
        foreach (SheepUnit unit in GetComponentsInChildren<SheepUnit>())orderedSheep.Add(new OrderObject(unit));
        orderedSheep = OrderByDistance(orderedSheep, ConstantsBucket.PlayfieldSize);
        amount = orderedSheep.Count;
        destrucionTime = msg.floatMessage;
        trigger = true;
        progress = 0f;
        counter = 0;
    }

    void Update() {
        if (trigger) {
            if (counter >= amount) {
                trigger = false;
                return;
            }
            if (progress > orderedSheep[counter].timeWeight * destrucionTime) {
                SheepUnit sheep = orderedSheep[counter].sheep;
                if (sheep != null) {
                    SheepFall fall = sheep.GetComponent<SheepFall>();
                    if (fall != null) {
                        fall.transform.parent = SheepCoordinator.Instance.transform;
                        fall.StartFalling(SpeedBucket.GetFallSpeed(sheep.sheepType), (Vector2)sheep.transform.position + FunctionsHelper.RandomVector2() * 1.5f);
                    } else
                        EventCoordinator.TriggerEvent(EventName.System.Sheep.Kill(), GameMessage.Write().WithSheepUnit(sheep));
                }
                counter++;
            }
            progress += Time.deltaTime;
        }
    }

    List<OrderObject> OrderByDistance(List<OrderObject> inputList, float normalizeDistance) {
        foreach (OrderObject obj in inputList) {
            obj.timeWeight = obj.sheep.transform.localPosition.magnitude / normalizeDistance;
        }
        inputList = inputList.OrderBy(x => x.timeWeight).ToList();
        return inputList;
    }

    class OrderObject {
        public SheepUnit sheep;
        public float timeWeight;
        public OrderObject(SheepUnit _sheep) {
            sheep = _sheep;
        }
    }
}