using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldNumbersController : MonoBehaviour {

    void Start() {
        EventCoordinator.StartListening(EventName.System.Economy.GoldChanged(), OnGoldChanged);
    }

    void OnGoldChanged(GameMessage msg) {
        if (msg.deltaFloat == 0 || msg.transform == null)return;
        PlusGold newGoldObj = GoldNumbersFactory.GetOrCreateGoldNumber(msg.owner, msg.transform, msg.deltaFloat);
    }
}