using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEliminatedNetworkHandler : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.System.Player.PostElimination(), OnPostEliminated);
        EventCoordinator.StartListening(EventName.System.Environment.EndMatch(), OnPostWin);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Player.PostElimination(), OnPostEliminated);
        EventCoordinator.StopListening(EventName.System.Environment.EndMatch(), OnPostWin);
    }
    void OnPostEliminated(GameMessage msg) {
        NetworkCoordinator.SendShowView(msg.targetOwner.deviceId, "post");
        NetworkCoordinator.SendKingItems(msg.targetOwner);
    }
    void OnPostWin(GameMessage msg) {
        NetworkCoordinator.SendShowView(msg.owner.deviceId, "post");
        NetworkCoordinator.SendKingItems(msg.owner);
    }
}