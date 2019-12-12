using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreNetworkWriter : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Player.Victorious(), OnWin);
        EventCoordinator.StartListening(EventName.System.Player.Defeated(), OnLost);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Player.Victorious(), OnWin);
        EventCoordinator.StopListening(EventName.System.Player.Defeated(), OnLost);
    }
    void OnWin(GameMessage msg){
        OnPlayerOut(true, msg);
    }
    void OnLost(GameMessage msg){
        OnPlayerOut(false, msg);
    }
    void OnPlayerOut(bool win, GameMessage msg)
    {
        PlayerScores playerScores = ScoreCoordinator.GetPlayerScore(msg.owner);
        NetworkCoordinator.SendPlayerScores(msg.owner.deviceId, win, playerScores.scores);
    }
}
