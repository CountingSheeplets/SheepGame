using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.GridLayoutGroup;

public class ControllerInputDemo : MonoBehaviour {

    public Owner playerOwner;
    public SheepType typeA;
    public SheepType typeB;
    Swipe newSwipe;
    //Vector2 stickValue;

    void Start() {
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
        newSwipe = new Swipe();
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnReadyToLaunch);
    }

    void Update() {
    }
    public void SetupOwner(Owner owner) {
        playerOwner = owner;
    }

    public void OnTargetMove(InputValue value) {
        if (!GameStateView.HasState(GameState.started)) return;
        // The given InputValue is only valid for the duration of the callback. Storing the InputValue references somewhere and calling Get<T>() later does not work correctly.
        Vector2 v = value.Get<Vector2>();
        //stickValue = v;
        newSwipe = new Swipe(v);
        //Debug.Log(newSwipe.ToString());
        EventCoordinator.TriggerEvent(EventName.Input.Swipe(), GameMessage.Write().WithSwipe(newSwipe).WithOwner(playerOwner));
    }

    public void OnShoot() {
        if (GameStateView.HasState(GameState.started)) {
            EventCoordinator.TriggerEvent(EventName.Input.Tap(), GameMessage.Write().WithSwipe(newSwipe).WithOwner(playerOwner));
        } else {
            if (playerOwner == null) return;
            if (!TryStart(playerOwner)){
                playerOwner.ready = true;
            }
        }
    }
    public void OnGrassRegrow() {
        if (!GameStateView.HasState(GameState.started)) return;
        EventCoordinator.TriggerEvent(EventName.Input.KingAbilities.BuyGrass(), GameMessage.Write().WithOwner(playerOwner));
    }
    public void OnBuySheep() {
        if (!GameStateView.HasState(GameState.started)) return;
        EventCoordinator.TriggerEvent(EventName.Input.KingAbilities.KingUpgrade(), GameMessage.Write().WithOwner(playerOwner));
    }
    public void OnKingSmash() {
        if (!GameStateView.HasState(GameState.started)) return;
        EventCoordinator.TriggerEvent(EventName.Input.KingAbilities.Smash(), GameMessage.Write().WithOwner(playerOwner));
    }

    public void OnUpgradeA() {
        if (!GameStateView.HasState(GameState.started)) return;
        EventCoordinator.TriggerEvent(EventName.Input.SheepUpgrade(), GameMessage.Write().WithOwner(playerOwner).WithSheepType(typeA));
    }
    public void OnUpgradeB() {
        if (!GameStateView.HasState(GameState.started)) return;
        EventCoordinator.TriggerEvent(EventName.Input.SheepUpgrade(), GameMessage.Write().WithOwner(playerOwner).WithSheepType(typeB));
    }

    bool TryStart(Owner readyingOwner) {
        int count = OwnersCoordinator.GetOwners().Where(x => x.connected).ToList().Count;
        if (count < 2)
            if (GameStateView.HasState(GameState.ended)) {
                return true;
            } else return false;
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            if (owner.EqualsByValue(readyingOwner))
                continue;
            if (owner.ready == false) {
                return false;
            }
        }
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            owner.ready = false;
        }
        //EventCoordinator.TriggerEvent(EventName.Input.PlayersReady(), GameMessage.Write());
        EventCoordinator.TriggerEvent(EventName.Input.StartGame(), GameMessage.Write());
        //NetworkCoordinator.SendShowViewAll("match");
        return true;
    }

    void OnReadyToLaunch(GameMessage msg) {
        UpgradeProperty upgA = UpgradeBucket.GetNextUpgradeA(msg.sheepUnit);
        if (upgA != null)
            typeA = upgA.sheepTypeOutput;
        else typeA = 0;
        UpgradeProperty upgB = UpgradeBucket.GetNextUpgradeB(msg.sheepUnit);
        if (upgB != null)
            typeB = upgB.sheepTypeOutput;
        else typeB = 0;
    }
}
