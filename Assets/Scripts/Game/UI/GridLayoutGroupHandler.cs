using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class GridLayoutGroupHandler : MonoBehaviour {
    GridLayoutGroup gridLayoutGroup;
    void Awake() {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        EventCoordinator.StartListening(EventName.Input.Network.PlayerRecalculate(), Regroup);
    }

    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.Network.PlayerRecalculate(), Regroup);
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