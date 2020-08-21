using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepEatWriter : MonoBehaviour {
    float counter;

    void Update() {
        counter += Time.deltaTime;
        if (counter > ConstantsBucket.EatInterval) {
            counter = 0;
            EventCoordinator.TriggerEvent(EventName.System.Economy.EatGrass(), GameMessage.Write());
        }
    }
}