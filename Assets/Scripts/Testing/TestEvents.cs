using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System.Linq;
public class TestEvents : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D)){
            Debug.Log("Defeat Random Player...");
            List<Owner> aliveOwners = new List<Owner> (OwnersCoordinator.GetOwners().Where(x => x.GetPlayerProfile().isAlive).ToList());
            Owner killer = OwnersCoordinator.GetRandomOwner();
            aliveOwners.Remove(killer);
            Owner killed = aliveOwners[Random.Range(0, aliveOwners.Count)];
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
        if(Input.GetKeyDown(KeyCode.G)){
            Debug.Log("Fake eat grass...");
            EventCoordinator.TriggerEvent(EventName.System.Economy.EatGrass(), GameMessage.Write().WithFloatMessage(10f));
        }
        if(Input.GetKeyDown(KeyCode.S)){
            NetworkCoordinator.SendShowViewAll("match");
        }
        if(Input.GetKeyDown(KeyCode.S)){
            Debug.Log("Fake spawn sheep...");
            SheepUnit sheep = SheepCoordinator.SpawnSheep(OwnersCoordinator.GetRandomOwner());
            EventCoordinator.TriggerEvent(EventName.System.Sheep.Spawned(), GameMessage.Write().WithSheepUnit(sheep));
        }
        if(Input.GetKeyDown(KeyCode.T)){
            Debug.Log("Fake throw sheep...");
            Swipe newSwipe = new Swipe();
            Debug.Log(newSwipe.ToString());
            EventCoordinator.TriggerEvent(EventName.Input.Swipe(), GameMessage.Write().WithSwipe(newSwipe).WithOwner(OwnersCoordinator.GetRandomOwner()));
        }
        if(Input.GetKeyDown(KeyCode.KeypadMultiply)){
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