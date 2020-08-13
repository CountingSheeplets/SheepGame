using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class SheepThrow : MonoBehaviour {
    public List<SheepUnit> throwableSheep = new List<SheepUnit>();
    public SheepUnit sheepReadyToBeThrown;
    Playfield playfield;
    void Start() {
        playfield = GetComponent<Playfield>();
        EventCoordinator.StartListening(EventName.Input.Swipe(), OnSwipe);
        EventCoordinator.StartListening(EventName.System.Sheep.Spawned(), OnSpawn);
        EventCoordinator.StartListening(EventName.System.Sheep.Land(), OnLand);
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnReady);
        EventCoordinator.StartListening(EventName.System.Sheep.Kill(), OnKill);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.Swipe(), OnSwipe);
        EventCoordinator.StopListening(EventName.System.Sheep.Spawned(), OnSpawn);
        EventCoordinator.StopListening(EventName.System.Sheep.Land(), OnLand);
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnReady);
        EventCoordinator.StopListening(EventName.System.Sheep.Kill(), OnKill);
    }

    void OnSwipe(GameMessage msg) {
        if (msg.owner.EqualsByValue(GetComponent<Owner>())) { //because this is input message
            if (sheepReadyToBeThrown) {
                throwableSheep.Remove(sheepReadyToBeThrown);
                sheepReadyToBeThrown.lastHandler = msg.owner;
                EventCoordinator.TriggerEvent(EventName.System.Sheep.Launch(), msg.WithSheepUnit(sheepReadyToBeThrown).WithPlayfield(sheepReadyToBeThrown.currentPlayfield).WithOwner(msg.owner));
                sheepReadyToBeThrown.currentPlayfield = null;
                sheepReadyToBeThrown = null;
            } else {
                //show animation/sign that no sheep ready to be thrown
                Debug.Log("no sheep could be found that is ready to be thrown!");
            }
            TryReadyNewSheep();
        }
    }
    void OnSpawn(GameMessage msg) {
        //Debug.Log("sheepSpawned:" + msg.sheepUnit);
        if (msg.sheepUnit) {
            //Debug.Log("msg.sheepUnit.owner:"+msg.sheepUnit.owner+" vs "+GetComponent<Owner>());
            if (msg.sheepUnit.owner.EqualsByValue(GetComponent<Owner>())) {
                throwableSheep.Add(msg.sheepUnit);
                TryReadyNewSheep();
            }
        }
    }
    void OnKill(GameMessage msg) {
        if (msg.sheepUnit && throwableSheep.Contains(msg.sheepUnit)) {
            throwableSheep.Remove(msg.sheepUnit);
        }
    }
    void OnLand(GameMessage msg) {
        if (msg.playfield == playfield &&
            msg.sheepUnit.owner == playfield.owner &&
            playfield.owner.GetPlayerProfile().isAlive) {
            //Debug.Log(msg.playfield+"-adding new sheep on Land:"+msg.sheepUnit.gameObject.name);
            throwableSheep.Add(msg.sheepUnit);
            TryReadyNewSheep();
        }
    }
    void OnReady(GameMessage msg) {
        if (msg.sheepUnit != null) {
            if (msg.sheepUnit.currentPlayfield == GetComponent<Playfield>()) {
                sheepReadyToBeThrown = msg.sheepUnit;
                sheepReadyToBeThrown.isReadyToFly = true;
            }
        }
    }
    void TryReadyNewSheep() {
        if (!SheepIsReadying() && sheepReadyToBeThrown == null) {
            SheepUnit availableSheep = GetNextSheep();
            //Debug.Log("GetNextSheep:"+availableSheep);
            if (availableSheep != null) {
                //Debug.Log("availableSheep.currentPlayfield:"+availableSheep.currentPlayfield);
                if (availableSheep.currentPlayfield != null) {
                    //Debug.Log("sheeprun:"+availableSheep.GetComponent<SheepRun>());
                    //Debug.Log(" at speed:"+SpeedBucket.GetRunSpeed(availableSheep.sheepType));
                    availableSheep.GetComponent<SheepRun>().StartRunning(SpeedBucket.GetRunSpeed(availableSheep.sheepType), (Vector2) availableSheep.transform.parent.position);
                }
            }
        }
    }
    SheepUnit GetNextSheep() {
        //ReorderTrenchSheep(); dont need this anymore, because enemy sheap dont shoot anymore
        foreach (SheepUnit sheep in throwableSheep) {
            if (sheep.canBeThrown) {
                if (sheep.sheepType == SheepType.Trench) {
                    if (!sheep.isTrenching) {
                        sheep.isTrenching = true;
                        Debug.Log("skipped cause trenching");
                        continue;
                    }
                    if (sheep.isTrenching) {
                        Debug.Log("trenching sheep not skipepd anymore");
                        sheep.isTrenching = false;
                        return sheep;
                    }
                }
                return sheep;
            }
        }
        return null;
    }
    /*     void ReorderTrenchSheep() {
            //put trench sheep to last
            List<SheepUnit> tempList = throwableSheep.OrderBy(sheep => sheep.sheepType == SheepType.Trench).ToList();
            throwableSheep = new List<SheepUnit>(tempList);
        } */
    bool SheepIsReadying() {
        foreach (SheepUnit sheep in throwableSheep) {
            if (sheep.isReadying)
                return true;
        }
        return false;
    }
}