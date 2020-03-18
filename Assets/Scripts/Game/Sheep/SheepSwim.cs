using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SheepSwim : BaseUnitMove {
    SheepUnit sheep;
    void Start() {
        if (sheep == null) sheep = GetComponent<SheepUnit>();
        EventCoordinator.StartListening(EventName.System.Sheep.Land(), OnLand);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Land(), OnLand);
    }
    public void StartSwiming(float speed, Vector2 _destination) {
        //Debug.Log("StartSwiming");
        SetDestination(_destination, false);
        sheep.isSwimming = true;
        //swim animation;

        //move the transform to destination
        MoveToDestination(speed, 0.25f);
    }
    void OnLand(GameMessage msg) {
        //Debug.Log("OnLand:StartSwiming:"+gameObject.name);
        if (msg.sheepUnit == sheep) {
            if (GameStateView.GetGameState() != GameState.arenaAnimating) {
                if (msg.playfield == null) {
                    Transform nearestVortex = FindObjectsOfType<Vortex>().Select(x => x.transform).ToList().FindNearest(transform);
                    if (nearestVortex != null)
                        this.StartSwiming(SpeedBucket.GetSwimSpeed(sheep.sheepType), nearestVortex.position);
                    else
                        EventCoordinator.TriggerEvent(EventName.System.Sheep.Kill(), GameMessage.Write().WithSheepUnit(sheep));
                }
            } else {
                EventCoordinator.TriggerEvent(EventName.System.Sheep.Kill(), GameMessage.Write().WithSheepUnit(sheep));
            }
        }
    }
    public override void PostMoveAction() {
        //trigger ready to jump/launch animation
        Debug.Log("post swimming, drowning at:" + (Vector2) (transform.position));

        //GetComponent<SheepUnit>().isSwimming = false; //uncomment this, if unit would not die, but do smth else
        Playfield playfield = GetComponent<SheepUnit>().currentPlayfield;
        EventCoordinator.TriggerEvent(EventName.System.Sheep.Kill(), GameMessage.Write().WithSheepUnit(GetComponent<SheepUnit>()));
    }
}