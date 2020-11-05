using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreNetworkWriter : MonoBehaviour {
    List<Owner> eliminatedOwners = new List<Owner>();
    void Start() {
        EventCoordinator.StartListening(EventName.UI.ShowScoreScreen(), OnScoresShow);
        EventCoordinator.StartListening(EventName.System.Player.Eliminated(), OnEliminated);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.UI.ShowScoreScreen(), OnScoresShow);
        EventCoordinator.StopListening(EventName.System.Player.Eliminated(), OnEliminated);
    }
    void OnScoresShow(GameMessage msg) {
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            PlayerScores playerScores = ScoreCoordinator.GetPlayerScores(owner);
            int total = ScoreCoordinator.GetTotalPlayerScores(owner);
            Debug.Log("Sending winner Scores:::" + playerScores.ToString());
            NetworkCoordinator.SendPlayerScores(owner.deviceId, owner.GetPlayerProfile().isAlive, playerScores.scores, total);
            Debug.Log("total score:" + total);
        }
    }
    void OnEliminated(GameMessage msg) {
        if (!eliminatedOwners.Contains(msg.targetOwner))
            eliminatedOwners.Add(msg.targetOwner);
        foreach (Owner owner in eliminatedOwners) {
            PlayerScores playerScores = ScoreCoordinator.GetPlayerScores(owner);
            int total = ScoreCoordinator.GetTotalPlayerScores(owner);
            owner.GetPlayerProfile().SetCrowns(total);
            Debug.Log("Sending Scores:::" + playerScores.ToString());
            NetworkCoordinator.SendPlayerScores(owner.deviceId, false, playerScores.scores, total);
            Debug.Log("total score:" + total);
        }
    }
}