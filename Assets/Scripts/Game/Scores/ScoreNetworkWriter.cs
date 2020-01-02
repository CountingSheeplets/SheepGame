using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreNetworkWriter : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Environment.EndMatch(), OnWin);
        EventCoordinator.StartListening(EventName.System.Player.Eliminated(), OnLost);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Environment.EndMatch(), OnWin);
        EventCoordinator.StopListening(EventName.System.Player.Eliminated(), OnLost);
    }
    void OnWin(GameMessage msg){
        PlayerScores playerScores = ScoreCoordinator.GetPlayerScores(msg.owner);
        NetworkCoordinator.SendPlayerScores(msg.owner.deviceId, true, playerScores.scores);
    }
    void OnLost(GameMessage msg){
        PlayerScores playerScores = ScoreCoordinator.GetPlayerScores(msg.targetOwner);
        NetworkCoordinator.SendPlayerScores(msg.targetOwner.deviceId, false, playerScores.scores);
    }
}
