using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class TestEvents : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D)){
            Debug.Log("Defeat Random Player...");
            List<Owner> owners = new List<Owner>(OwnersCoordinator.GetOwners());
            Owner killer = owners[Random.Range(0, owners.Count-1)];
            owners.Remove(killer);
            Owner killed = owners[Random.Range(0, owners.Count-1)];
            EventCoordinator.TriggerEvent(EventName.System.Player.Eliminated(), GameMessage.Write().WithOwner(killer).WithTargetOwner(killed));
        }
        if(Input.GetKeyDown(KeyCode.A)){
            Debug.Log("Fake Hit a random King with random sheep...");
            List<KingUnit> kings = new List<KingUnit>( FindObjectsOfType<KingUnit>() );
            List<SheepUnit> sheeps = new List<SheepUnit>( FindObjectsOfType<SheepUnit>() );
            SheepUnit sheep = null;
            List<Owner> owners = OwnersCoordinator.GetOwners();
            if(sheeps.Count>0)
                sheep = sheeps[Random.Range(0, sheeps.Count-1)];
            EventCoordinator.TriggerEvent(EventName.System.King.Hit(), GameMessage.Write().WithKingUnit(kings[Random.Range(0, kings.Count-1)]).WithSheepUnit(sheep).WithOwner(owners[Random.Range(0, owners.Count-1)]));
        }
        if(Input.GetKeyDown(KeyCode.B)){
            Debug.Log("Fake eat grass...");
            EventCoordinator.TriggerEvent(EventName.System.Economy.EatGrass(), GameMessage.Write().WithFloatMessage(10f));
        }
        if(Input.GetKeyDown(KeyCode.S)){
            NetworkCoordinator.SendShowViewAll("match");
        }

        if(Input.GetKeyDown(KeyCode.KeypadPlus)){
            foreach(Owner owner in OwnersCoordinator.GetOwners()){
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Achievement.Baaah(), 1);
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Achievement.Education(), 1);
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Achievement.GetThatAction(), 1);
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Achievement.Paladin(), 1);

                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Counter.Angry(), 1);
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Counter.Culling(), 150);
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Counter.Elvish(), 50);
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Counter.Merchant(), 500);
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Counter.Shepherd(), 300);
            }
        }
    }
}