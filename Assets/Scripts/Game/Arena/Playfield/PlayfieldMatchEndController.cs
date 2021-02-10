using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfieldMatchEndController : MonoBehaviour {
    void Start() {
        EventCoordinator.StartListening(EventName.UI.ScoreScreenShown(), OnScoreScreenShown);
    }

    void OnScoreScreenShown(GameMessage msg) {
        Owner owner = GetComponent<Playfield>().owner;
        ArenaCoordinator.RemoveField(owner);
        owner.GetPlayerProfile().isAlive = false;
    }
}