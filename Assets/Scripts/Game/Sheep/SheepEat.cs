using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepEat : MonoBehaviour {
    SheepUnit sheep;

    void Start() {
        sheep = GetComponent<SheepUnit>();
        EventCoordinator.StartListening(EventName.System.Economy.EatGrass(), OnEat);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Economy.EatGrass(), OnEat);
    }
    void OnEat(GameMessage msg) {
        if (sheep.currentPlayfield == null)
            return;
        Owner playfieldOwner = sheep.currentPlayfield.owner;
        if (playfieldOwner.EqualsByValue(sheep.owner) || !playfieldOwner.GetPlayerProfile().isAlive)
            return;
        float eatAmount = DamageBucket.GetEatDamage(sheep.sheepType);
        float totalGrass = PlayerProfileCoordinator.ModifyPlayerGrass(sheep.currentPlayfield.owner, -eatAmount);
    }

}