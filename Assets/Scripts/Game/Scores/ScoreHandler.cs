using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreHandler : MonoBehaviour
{
    public Dictionary<Owner, int> sheepsSmited = new Dictionary<Owner, int>();
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Player.Eliminated(), OnPlayerDefeated);
        EventCoordinator.StartListening(EventName.System.Sheep.Spawned(), OnSheepSpawn);
        EventCoordinator.StartListening(EventName.System.Sheep.Kill(), OnSheepKilled);
        EventCoordinator.StartListening(EventName.System.King.Hit(), OnKingHit);
        EventCoordinator.StartListening(EventName.System.Environment.EndMatch(), OnMatchEnd);
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnKingSmashed);
    }

    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.System.Player.Eliminated(), OnPlayerDefeated);
        EventCoordinator.StopListening(EventName.System.Sheep.Spawned(), OnSheepSpawn);
        EventCoordinator.StopListening(EventName.System.Sheep.Kill(), OnSheepKilled);
        EventCoordinator.StopListening(EventName.System.King.Hit(), OnKingHit);
        EventCoordinator.StopListening(EventName.System.Environment.EndMatch(), OnMatchEnd);
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnKingSmashed);
    }

    void OnPlayerDefeated(GameMessage msg){
        ScoreCoordinator.IncreaseScoreCounter(msg.owner, ScoreName.Counter.Angry(), 1);
        if(ScoreCoordinator.GetPlayerScore(msg.owner, ScoreName.Achievement.GetThatAction()).counter == 0)
            ScoreCoordinator.IncreaseScoreCounter(msg.owner, ScoreName.Achievement.GetThatAction(), 1);
        ScoreCoordinator.IncreaseScoreCounter(msg.owner, ScoreName.Counter.Merchant(), (int)msg.owner.GetPlayerProfile().GetMoneyEarned());
        if(SheepCoordinator.GetSheeps(msg.owner).Where(x=>x.sheepType == SheepType.None).ToList().Count == 0)
            ScoreCoordinator.IncreaseScoreCounter(msg.owner, ScoreName.Achievement.Education(), 1);

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
/*         Owner owner = null;
        int sheepKilled = 0;
        foreach(KeyValuePair<Owner, Score> pair in ScoreCoordinator.GetScore(ScoreName.Counter.Culling())){
            if(pair.Value.counter > sheepKilled){
                sheepKilled = pair.Value.counter;
                owner = pair.Key;
            }
        } */
        if(sheepsSmited.Count > 0){
            Owner highestSmiter = sheepsSmited.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            ScoreCoordinator.IncreaseScoreCounter(highestSmiter, ScoreName.Achievement.Paladin(), 1);
        }
        ScoreCoordinator.CalculateTier2TechCounts();
    }
    void OnKingSmashed(GameMessage msg){
        //make a dictionary <Owner, int> and track the count of list of sheep which where hit by smash
        if(sheepsSmited.ContainsKey(msg.owner))
            if(msg.sheepUnits.Count < sheepsSmited[msg.owner]) return;

        sheepsSmited[msg.owner] = msg.sheepUnits.Count;
        //OnMatchEnd apply score
    }
}
