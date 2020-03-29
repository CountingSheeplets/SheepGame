using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class GridLayoutGroupHandler : MonoBehaviour {
    GridLayoutGroup gridLayoutGroup;
    void Start() {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), Regroup);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerLeft(), Regroup);
    }

    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), Regroup);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerLeft(), Regroup);
    }

    void Regroup(GameMessage msg) {
        int count = OwnersCoordinator.GetOwners().Where(x => x.connected).ToList().Count;
        if (count > 4) {
            gridLayoutGroup.constraintCount = 2;
        } else {
            gridLayoutGroup.constraintCount = 1;
        }
    }
}