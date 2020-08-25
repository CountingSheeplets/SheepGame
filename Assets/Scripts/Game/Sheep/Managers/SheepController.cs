﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.System.Environment.EndMatch(), OnEndMatch);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Environment.EndMatch(), OnEndMatch);
    }
    void OnEndMatch(GameMessage msg) {
        List<SheepUnit> sheeps = new List<SheepUnit>(SheepCoordinator.GetSheeps());
        for (int i = 0; i < sheeps.Count; i++) {
            if (sheeps[i] != null)
                if (sheeps[i].isSwimming || sheeps[i].isFlying)
                    EventCoordinator.TriggerEvent(EventName.System.Sheep.Kill(), GameMessage.Write().WithSheepUnit(sheeps[i]));
        }
    }
}