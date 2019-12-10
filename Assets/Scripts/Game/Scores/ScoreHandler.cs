using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Player.Defeated(), OnPlayerDefeated);
        EventCoordinator.StartListening(EventName.System.Sheep.Spawned(), OnSheepSpawn);
        EventCoordinator.StartListening(EventName.System.Sheep.Kill(), OnSheepKilled);
        EventCoordinator.StartListening(EventName.System.King.Hit(), OnKingHit);
        EventCoordinator.StartListening(EventName.System.Player.Victorious(), OnMatchEnd);
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnKingSmashed);
    }

    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.System.Player.Defeated(), OnPlayerDefeated);
        EventCoordinator.StopListening(EventName.System.Sheep.Spawned(), OnSheepSpawn);
        EventCoordinator.StopListening(EventName.System.Sheep.Kill(), OnSheepKilled);
        EventCoordinator.StopListening(EventName.System.King.Hit(), OnKingHit);
        EventCoordinator.StopListening(EventName.System.Player.Victorious(), OnMatchEnd);
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnKingSmashed);
    }

    void OnPlayerDefeated(GameMessage msg){
        ScoreCoordinator.IncreaseScoreCounter(msg.owner, ScoreName.Counter.Angry(), 1);
        if(ScoreCoordinator.GetPlayerScore(msg.owner, ScoreName.Achievement.GetthatAction()).counter == 0)
            ScoreCoordinator.IncreaseScoreCounter(msg.owner, ScoreName.Achievement.GetthatAction(), 1);
    }

    void OnSheepSpawn(GameMessage msg){
        ScoreCoordinator.IncreaseScoreCounter(msg.sheepUnit.owner, ScoreName.Counter.Shepherd(), 1);
    }
    void OnSheepKilled(GameMessage msg){
        if(msg.sheepUnit.lastHandler != null)
            ScoreCoordinator.IncreaseScoreCounter(msg.sheepUnit.lastHandler, ScoreName.Counter.Culling(), 1);
    }
    void OnKingHit(GameMessage msg){
        ScoreCoordinator.IncreaseScoreCounter(msg.owner, ScoreName.Counter.Elvish(), 1);
    }
    void OnMatchEnd(GameMessage msg){
        Owner owner = null;
        int sheepKilled = 0;
        foreach(KeyValuePair<Owner, Score> pair in ScoreCoordinator.GetScore(ScoreName.Counter.Culling())){
            if(pair.Value.counter > sheepKilled){
                sheepKilled = pair.Value.counter;
                owner = pair.Key;
            }
        }
        if(owner != null){
            ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Achievement.Paladin(), 1);
        }
    }
    void OnKingSmashed(GameMessage msg){
        //make a dictionary <Owner, int> and track the count of list of sheep which where hit by smash

        //OnMatchEnd apply score
    }
}
