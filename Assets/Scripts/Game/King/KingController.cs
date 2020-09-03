using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingController : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
    }

    void OnStartGame(GameMessage msg) {
        List<Owner> owners = OwnersCoordinator.GetOwners();
        foreach (Owner owner in owners) {
            KingUnit king = KingFactory.CreateKing(owner);
            SheepCoordinator.CreateStack(owner);
            if (king != null)
                EventCoordinator.TriggerEvent(EventName.System.King.Spawned(), GameMessage.Write().WithOwner(king.owner).WithGameObject(king.gameObject).WithKingUnit(king));
        }
    }
}