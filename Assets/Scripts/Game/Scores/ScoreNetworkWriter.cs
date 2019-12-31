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
        OnPlayerOut(true, msg);
    }
    void OnLost(GameMessage msg){
        OnPlayerOut(false, msg);
    }
    void OnPlayerOut(bool win, GameMessage msg)
    {
        PlayerScores playerScores = ScoreCoordinator.GetPlayerScores(msg.owner);
        //Debug.Log(msg.owner);
        //Debug.Log(playerScores);
        NetworkCoordinator.SendPlayerScores(msg.owner.deviceId, win, playerScores.scores);
    }
}
