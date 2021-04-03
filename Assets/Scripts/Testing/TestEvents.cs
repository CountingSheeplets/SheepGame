using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;
public class TestEvents : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            List<Owner> aliveOwners = new List<Owner>(OwnersCoordinator.GetOwners().Where(x => x.GetPlayerProfile().isAlive).ToList());
            if (aliveOwners.Count > 1) {

                Owner killer = OwnersCoordinator.GetRandomOwner();
                aliveOwners.Remove(killer);
                Owner killed = aliveOwners[Random.Range(0, aliveOwners.Count)];
                Debug.Log("Defeating Random Player..." + killed + "  by  " + killer);
                EventCoordinator.TriggerEvent(EventName.System.Player.Eliminated(), GameMessage.Write().WithOwner(killer).WithTargetOwner(killed));
            } else {
                Owner killer = OwnersCoordinator.GetRandomOwner();
                EventCoordinator.TriggerEvent(EventName.System.Player.Eliminated(), GameMessage.Write().WithOwner(killer).WithTargetOwner(killer));
            }
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            Debug.Log("Fake Hit a random King with random sheep...");
            List<KingUnit> kings = new List<KingUnit>(FindObjectsOfType<KingUnit>());
            List<SheepUnit> sheeps = new List<SheepUnit>(FindObjectsOfType<SheepUnit>());
            SheepUnit sheep = null;
            List<Owner> owners = OwnersCoordinator.GetOwners();
            if (sheeps.Count > 0)
                sheep = sheeps[Random.Range(0, sheeps.Count - 1)];
            EventCoordinator.TriggerEvent(EventName.System.King.Hit(), GameMessage.Write().WithKingUnit(kings[Random.Range(0, kings.Count - 1)]).WithSheepUnit(sheep).WithOwner(owners[Random.Range(0, owners.Count - 1)]));
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            Debug.Log("Fake land sheep on a random king...");
            List<SheepUnit> sheeps = new List<SheepUnit>(FindObjectsOfType<SheepUnit>());
            SheepUnit sheep = null;
            KingUnit king = GetRandomKing();
            if (sheeps.Count > 0)
                sheep = sheeps[Random.Range(0, sheeps.Count)];
            sheep.lastHandler = sheep.currentPlayfield.owner;
            sheep.transform.position = king.transform.position;
            Playfield newPlayfield = ArenaCoordinator.GetPlayfield(sheep.transform.position);
            sheep.currentPlayfield = newPlayfield;
            sheep.ResetContainer();
            EventCoordinator.TriggerEvent(EventName.System.Sheep.Land(), GameMessage.Write().WithSheepUnit(sheep).WithPlayfield(king.myPlayfield));
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            Debug.Log("Fake eat grass...");
            EventCoordinator.TriggerEvent(EventName.System.Economy.EatGrass(), GameMessage.Write().WithFloatMessage(1f));
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            Debug.Log("Fake king upgrade...");
            KingUnit randomKing = GetRandomKing();
            EventCoordinator.TriggerEvent(EventName.Input.KingAbilities.KingUpgrade(), GameMessage.Write().WithOwner(randomKing.owner));
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            //NetworkCoordinator.SendShowViewAll("match");
            KingUnit randomKing = GetRandomKing();
            EventCoordinator.TriggerEvent(EventName.Input.KingAbilities.Smash(), GameMessage.Write().WithOwner(randomKing.owner));
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            Debug.Log("Fake spawn sheep...: " + SheepCoordinator.GetSheepsAll().Count);
            SheepUnit sheep = SheepCoordinator.SpawnSheep(OwnersCoordinator.GetRandomOwner());
            EventCoordinator.TriggerEvent(EventName.System.Sheep.Spawned(), GameMessage.Write().WithSheepUnit(sheep));
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            Debug.Log("Fake increase sheep spawn rate...");
            EventCoordinator.TriggerEvent(EventName.Input.KingAbilities.SpawnSheep(), GameMessage.Write().WithOwner(OwnersCoordinator.GetRandomOwner()));
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            Debug.Log("Fake throw sheep...random");
            Swipe newSwipe = new Swipe();
            //Debug.Log(newSwipe.ToString());
            EventCoordinator.TriggerEvent(EventName.Input.Tap(), GameMessage.Write().WithSwipe(newSwipe).WithOwner(OwnersCoordinator.GetRandomOwner()).WithState(false));
        }
        if (Input.GetKeyDown(KeyCode.Y)) {
            Debug.Log("Fake throw sheep...to a 0th Owner from random player");
            Swipe newSwipe = new Swipe();
            Owner randomOwner = OwnersCoordinator.GetRandomOwner();
            newSwipe.ToZeroPlayfield(randomOwner);
            //Debug.Log(newSwipe.ToString());
            EventCoordinator.TriggerEvent(EventName.Input.Tap(), GameMessage.Write().WithSwipe(newSwipe).WithOwner(randomOwner).WithState(false));
        }
        if (Input.GetKeyDown(KeyCode.KeypadMultiply)) {
            foreach (Owner owner in OwnersCoordinator.GetOwners()) {
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
        if (Input.GetKeyDown(KeyCode.F10)) {
            EventCoordinator.TriggerEvent(EventName.System.Environment.ScrollScoresOut(), GameMessage.Write());
            Debug.Log("Scene Cleaning done.");
        }

        if (Input.GetKeyDown(KeyCode.F1)) {
            Debug.Log("Random king smites....");
            Owner smasher = OwnersCoordinator.GetRandomOwner();
            EventCoordinator.TriggerEvent(EventName.Input.KingAbilities.Smash(), GameMessage.Write().WithOwner(smasher));
        }
        if (Input.GetKeyDown(KeyCode.F4)) {
            Debug.Log("clear all current owner data....");
            PersistentDataCoordinator.DeleteAllStoredData();
        }

        ///upgrades:
        if (Input.GetKeyDown(KeyCode.U)) {
            Debug.Log("Random owner clicks upgrade....");
            Owner upgrader = OwnersCoordinator.GetRandomOwner();
            EventCoordinator.TriggerEvent(EventName.Input.SheepUpgrade(), GameMessage.Write().WithOwner(upgrader).WithUpgradeType((UpgradeType)Random.Range(0, 2)));
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Debug.Log("second owner gets first upgrade....");
            Owner upgrader = OwnersCoordinator.Instance.owners[1];
            SheepType sType = SheepType.Small;
            EventCoordinator.TriggerEvent(EventName.Input.SheepUpgrade(), GameMessage.Write().WithOwner(upgrader).WithSheepType(sType));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("second owner gets second upgrade....");
            Owner upgrader = OwnersCoordinator.Instance.owners[1];
            SheepType sType = SheepType.Bouncy;
            EventCoordinator.TriggerEvent(EventName.Input.SheepUpgrade(), GameMessage.Write().WithOwner(upgrader).WithSheepType(sType));
        }
    }

    KingUnit GetRandomKing() {
        List<KingUnit> kings = new List<KingUnit>(FindObjectsOfType<KingUnit>());
        KingUnit king = null;
        if (kings.Count > 0)
            king = kings[Random.Range(0, kings.Count)];
        return king;
    }
}