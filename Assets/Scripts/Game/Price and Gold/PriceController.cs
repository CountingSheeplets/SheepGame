using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceController : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
    }

    void OnStartGame(GameMessage msg) {
        List<Owner> owners = OwnersCoordinator.GetOwners();
        foreach (Owner owner in owners)
            PriceCoordinator.AddPriceAttribute(owner);
    }
}