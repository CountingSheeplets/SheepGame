using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventChain : MonoBehaviour {
    void Start () {
        EventCoordinator.Attach(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.Attach(EventName.System.Environment.EndMatch(), OnEndMatch);
        EventCoordinator.Attach(EventName.System.Player.Eliminated(), OnPlayerEliminated);
    }
    void OnDestroy() {
        EventCoordinator.Detach(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.Detach(EventName.System.Environment.EndMatch(), OnEndMatch);
        EventCoordinator.Detach(EventName.System.Player.Eliminated(), OnPlayerEliminated);
    }
    void OnStartGame(GameMessage msg)
    {
        EventCoordinator.TriggerEvent(EventName.System.Environment.Initialized(), msg);
    }
    void OnEndMatch(GameMessage msg)
    {
        EventCoordinator.TriggerEvent(EventName.UI.ShowScoreScreen(), msg);
    }
    void OnPlayerEliminated(GameMessage msg){
        EventCoordinator.TriggerEvent(EventName.System.Player.PostElimination(), msg);
    }
}