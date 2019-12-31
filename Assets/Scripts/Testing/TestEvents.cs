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
            List<Owner> owners = OwnersCoordinator.GetOwners();
            EventCoordinator.TriggerEvent(EventName.System.Player.Eliminated(), GameMessage.Write().WithOwner(owners[Random.Range(0, owners.Count-1)]));
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
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Achievement.Baaah(), 5);
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Achievement.Education(), 5);
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Achievement.GetThatAction(), 5);
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Achievement.Paladin(), 5);

                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Counter.Angry(), 500);
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Counter.Culling(), 500);
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Counter.Elvish(), 500);
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Counter.Merchant(), 3000);
                ScoreCoordinator.IncreaseScoreCounter(owner, ScoreName.Counter.Shepherd(), 3000);
            }
        }
    }
}