using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreNetworkWriter : MonoBehaviour
{
    List<Owner> eliminatedOwners = new List<Owner>();
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Environment.EndMatch(), OnWin);
        EventCoordinator.StartListening(EventName.System.Player.Eliminated(), OnEliminated);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Environment.EndMatch(), OnWin);
        EventCoordinator.StopListening(EventName.System.Player.Eliminated(), OnEliminated);
    }
    void OnWin(GameMessage msg){
        PlayerScores playerScores = ScoreCoordinator.GetPlayerScores(msg.owner);
        int total = ScoreCoordinator.GetTotalPlayerScores(msg.owner);
        msg.owner.GetPlayerProfile().SetCrowns(total);
        NetworkCoordinator.SendPlayerScores(msg.owner.deviceId, true, playerScores.scores, total);
    }
    void OnEliminated(GameMessage msg){
        if(!eliminatedOwners.Contains(msg.targetOwner))
            eliminatedOwners.Add(msg.targetOwner);
        foreach(Owner owner in eliminatedOwners){
            PlayerScores playerScores = ScoreCoordinator.GetPlayerScores(owner);
            int total = ScoreCoordinator.GetTotalPlayerScores(msg.owner);
            msg.owner.GetPlayerProfile().SetCrowns(total);
            NetworkCoordinator.SendPlayerScores(owner.deviceId, false, playerScores.scores, total);
        }
    }
}