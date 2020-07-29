using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfieldKillUnitsOnDestroy : MonoBehaviour {
    List<SheepUnit> sheepUnits;
    public Owner myOwner;
    bool trigger;
    float killInterval = 1f;
    float progress = 0f;
    float destrucionTime;
    int counter = 0;
    int amount = 0;
    void Start() {
        EventCoordinator.StartListening(EventName.System.Environment.DestroyArena(), OnDestroyPlayfield);
        myOwner = GetComponent<Playfield>().owner;
    }
    void OnDestroyPlayfield(GameMessage msg) {
        if (!myOwner.EqualsByValue(msg.targetOwner))
            return;
        sheepUnits = new List<SheepUnit>(GetComponentsInChildren<SheepUnit>());
        amount = sheepUnits.Count;
        destrucionTime = msg.floatMessage;
        killInterval = destrucionTime / amount;
        trigger = true;
        progress = 0f;
        counter = 0;
        foreach (SheepUnit unit in sheepUnits) {
            SkeletonRendererController.MakeSheepActive(unit);
            unit.GetComponent<SheepFall>().StartFalling(SpeedBucket.GetFallSpeed(unit.sheepType), Vector2.zero);
        }
    }

    void Update() {
        if (trigger) {
            if (progress > killInterval) {
                counter++;
                if (counter >= amount)
                    trigger = false;
                else {
                    if (sheepUnits.Count > 0)
                        EventCoordinator.TriggerEvent(EventName.System.Sheep.Kill(), GameMessage.Write().WithSheepUnit(sheepUnits[0]));
                    progress = 0f;
                    sheepUnits.RemoveAt(0);
                }
            }
            progress += Time.deltaTime;
        }
    }
}