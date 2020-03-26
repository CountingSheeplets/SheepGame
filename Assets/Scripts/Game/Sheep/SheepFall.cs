using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SheepFall : BaseUnitMove {
    SheepUnit sheep;
    void Start() {
        if (sheep == null)sheep = GetComponent<SheepUnit>();
        EventCoordinator.StartListening(EventName.System.Sheep.Land(), OnLand);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Land(), OnLand);
    }
    public void StartFalling(float speed, Vector2 _destination) {
        //Debug.Log("StartFalling");
        SetDestination(_destination, false);
        sheep.isSwimming = true;
        //fall animation;

        //change layer to be behind all

        //move the transform to destination
        MoveToDestination(speed, 0.25f);
    }
    void OnLand(GameMessage msg) {
        //Debug.Log("OnLand:StartFalling:"+gameObject.name);
        if (msg.sheepUnit == sheep) {
            if (GameStateView.GetGameState() != GameState.arenaAnimating) {
                if (msg.playfield == null) {
                    SheepFly fly = GetComponent<SheepFly>();
                    if (fly != null)
                        this.StartFalling(SpeedBucket.GetFallSpeed(sheep.sheepType), fly.GetLocalMoveDir() + (Vector2)transform.position);
                    else
                        EventCoordinator.TriggerEvent(EventName.System.Sheep.Kill(), GameMessage.Write().WithSheepUnit(sheep));
                }
            } else {
                EventCoordinator.TriggerEvent(EventName.System.Sheep.Kill(), GameMessage.Write().WithSheepUnit(sheep));
            }
        }
    }
    public override void PostMoveAction() {
        Debug.Log("post falling, splat at:" + (Vector2)(transform.position));

        //GetComponent<SheepUnit>().isSwimming = false; //uncomment this, if unit would not die, but do smth else
        EventCoordinator.TriggerEvent(EventName.System.Sheep.Kill(), GameMessage.Write().WithSheepUnit(GetComponent<SheepUnit>()));
    }
}