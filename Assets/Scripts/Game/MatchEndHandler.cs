using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchEndHandler : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.System.Environment.ArenaDestroyed(), OnArenaDestroyed);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Environment.ArenaDestroyed(), OnArenaDestroyed);
    }
    void OnArenaDestroyed(GameMessage msg) {
        ////Debug.Log("OnPostEliminated: state: " + GameState.ended);
        if (GameStateView.HasState(GameState.ended))
            return;
        foreach (Owner owner in msg.targetOwners)
            owner.GetPlayerProfile().isAlive = false;
        List<Owner> owners = PlayerProfileCoordinator.GetAliveOwners();
        //Debug.Log("alive owners: " + owners.Count);
        if (owners.Count == 1)
            EventCoordinator.TriggerEvent(EventName.System.Environment.EndMatch(), GameMessage.Write().WithOwner(owners[0]));
        if (owners.Count == 0)
            EventCoordinator.TriggerEvent(EventName.System.Environment.EndMatch(), GameMessage.Write().WithOwner(msg.owner));
    }
}