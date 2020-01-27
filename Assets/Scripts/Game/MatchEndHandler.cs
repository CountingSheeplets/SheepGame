using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchEndHandler : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Player.PostElimination(), OnPostEliminated);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Player.PostElimination(), OnPostEliminated);
    }
    void OnPostEliminated(GameMessage msg)
    {
        if(GameStateView.HasState(GameState.ended))
            return;
        msg.targetOwner.GetPlayerProfile().isAlive = false;
        List<Owner> owners = PlayerProfileCoordinator.GetAliveOwners();
        //Debug.Log("alive owners: "+owners.Count);
        if(owners.Count == 1)
            EventCoordinator.TriggerEvent(EventName.System.Environment.EndMatch(), GameMessage.Write().WithOwner(owners[0]));
/*         if(owners.Count == 0){
            Owner lastElimedOwner = OwnersCoordinator.GetOwners().OrderByDescending(owner => owner.GetPlayerProfile().eliminatedPlace).FirstOrDefault();
            EventCoordinator.TriggerEvent(EventName.System.Environment.EndMatch(), GameMessage.Write().WithOwner(lastElimedOwner));
        } */
    }
}
