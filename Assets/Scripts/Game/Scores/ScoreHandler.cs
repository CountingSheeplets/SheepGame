using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreHandler : MonoBehaviour {
    int eliminiatedPlayerCount = 0;
    public Dictionary<Owner, int> sheepsSmited = new Dictionary<Owner, int>();
    void Start() {
        EventCoordinator.StartListening(EventName.System.Player.Eliminated(), OnPlayerDefeated);
        EventCoordinator.StartListening(EventName.System.Sheep.Launch(), OnSheepLaunch);
        EventCoordinator.StartListening(EventName.System.Sheep.Kill(), OnSheepKilled);
        EventCoordinator.StartListening(EventName.System.King.Hit(), OnKingHit);
        EventCoordinator.StartListening(EventName.System.Environment.EndMatch(), OnMatchEnd);
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnKingSmashed);
    }

    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Player.Eliminated(), OnPlayerDefeated);
        EventCoordinator.StopListening(EventName.System.Sheep.Launch(), OnSheepLaunch);
        EventCoordinator.StopListening(EventName.System.Sheep.Kill(), OnSheepKilled);
        EventCoordinator.StopListening(EventName.System.King.Hit(), OnKingHit);
        EventCoordinator.StopListening(EventName.System.Environment.EndMatch(), OnMatchEnd);
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnKingSmashed);
    }

    void OnPlayerDefeated(GameMessage msg) {
        eliminiatedPlayerCount++;
        msg.targetOwner.GetPlayerProfile().eliminatedPlace = eliminiatedPlayerCount;

        if (msg.owner != null) {
            ScoreCoordinator.IncreaseScoreCounter(msg.owner, ScoreName.Counter.Angry(), 1);
            if (ScoreCoordinator.GetPlayerScore(msg.owner, ScoreName.Achievement.GetThatAction()).counter == 0)
                ScoreCoordinator.IncreaseScoreCounter(msg.owner, ScoreName.Achievement.GetThatAction(), 1);
        }

        ScoreCoordinator.IncreaseScoreCounter(msg.targetOwner, ScoreName.Counter.Merchant(), (int)msg.targetOwner.GetPlayerProfile().GetMoneyEarned());
        if (SheepCoordinator.GetSheeps(msg.targetOwner).Where(x => x.sheepType == SheepType.None).ToList().Count == 0)
            ScoreCoordinator.IncreaseScoreCounter(msg.targetOwner, ScoreName.Achievement.Education(), 1);
    }

    void OnSheepLaunch(GameMessage msg) {
        ScoreCoordinator.IncreaseScoreCounter(msg.sheepUnit.owner, ScoreName.Counter.Shepherd(), 1);
    }
    void OnSheepKilled(GameMessage msg) {
        if (msg.sheepUnit.lastHandler != null)
            ScoreCoordinator.IncreaseScoreCounter(msg.sheepUnit.lastHandler, ScoreName.Counter.Culling(), 1);
    }
    void OnKingHit(GameMessage msg) {
        ScoreCoordinator.IncreaseScoreCounter(msg.owner, ScoreName.Counter.Elvish(), 1);
    }
    void OnMatchEnd(GameMessage msg) {
        if (sheepsSmited.Count > 0) {
            Owner highestSmiter = sheepsSmited.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            ScoreCoordinator.IncreaseScoreCounter(highestSmiter, ScoreName.Achievement.Paladin(), 1);
        }
        ScoreCoordinator.CalculateTier2TechCounts();
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            int total = ScoreCoordinator.GetTotalPlayerScores(owner);
            total = owner.GetPlayerProfile().isAlive ? total * 2 : total;
            owner.GetPlayerProfile().SetCrowns(total);
        }
    }
    void OnKingSmashed(GameMessage msg) {
        if (sheepsSmited.ContainsKey(msg.owner))
            if (msg.sheepUnits.Count < sheepsSmited[msg.owner])return;

        sheepsSmited[msg.owner] = msg.sheepUnits.Count;
    }
}