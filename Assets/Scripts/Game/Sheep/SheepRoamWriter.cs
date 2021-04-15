using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepRoamWriter : MonoBehaviour {

    float counter;
    void Update() {
        if (!GameStateView.HasState(GameState.started))return;
        counter += Time.deltaTime;
        if (counter > ConstantsBucket.RoamInterval) {
            counter = 0;
            EventCoordinator.TriggerEvent(EventName.System.Sheep.Roam(), GameMessage.Write().WithFloatMessage(ConstantsBucket.RoamProbability));
        }
    }
}