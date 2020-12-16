using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
public class SheepFall : BaseUnitMove {
    SheepUnit sheep;
    SortingGroup sGroup;
    void Start() {
        if (sheep == null)sheep = GetComponent<SheepUnit>();
        if (sGroup == null)sGroup = sheep.GetComponentInChildren<SortingGroup>();
        EventCoordinator.StartListening(EventName.System.Sheep.Land(), OnLand);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Land(), OnLand);
    }
    public void StartFalling(float speed, Vector2 _destination) {
        //Debug.Log("StartFalling");
        SetDestination(_destination, false);
        sheep.isSwimming = true;
        //change layer to be behind all
        if (sGroup)
            sGroup.sortingOrder = -10;
        //move the transform to destination
        MoveToDestination(speed, 0.5f, 0.3f);
        //fall animation;
        animator.Die();
    }
    void OnLand(GameMessage msg) {
        //Debug.Log("OnLand:StartFalling:"+gameObject.name);
        if (msg.sheepUnit == sheep) {
            //if (GameStateView.GetGameState() != GameState.arenaAnimating) {
            if (msg.playfield == null) {
                SheepFly fly = GetComponent<SheepFly>();
                Vector2 fallDirection = fly.GetLocalMoveDir() / 10f + (Vector2)transform.position;
                if (fly == null)
                    fallDirection = Vector2.zero;
                this.StartFalling(SpeedBucket.GetFallSpeed(sheep.sheepType), fallDirection);
            }
            //} //else {
            //   EventCoordinator.TriggerEvent(EventName.System.Sheep.Kill(), GameMessage.Write().WithSheepUnit(sheep));
            //}
        }
    }
    public override void PostMoveAction() {
        //Debug.Log("post falling, splat at:" + (Vector2) (transform.position));

        //GetComponent<SheepUnit>().isSwimming = false; //uncomment this, if unit would not die, but do smth else
        EventCoordinator.TriggerEvent(EventName.System.Sheep.Kill(), GameMessage.Write().WithSheepUnit(GetComponent<SheepUnit>()));
    }
}