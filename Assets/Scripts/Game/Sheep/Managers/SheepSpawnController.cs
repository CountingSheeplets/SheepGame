using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawnController : MonoBehaviour {
    float scatterRandomBase = 0.2f;
    Dictionary<Owner, float> nextSpawn = new Dictionary<Owner, float>();
    void Start() {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StartListening(EventName.System.Player.PostElimination(), OnEliminated);
        EventCoordinator.StartListening(EventName.System.Sheep.Kill(), OnKill);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StopListening(EventName.System.Player.PostElimination(), OnEliminated);
        EventCoordinator.StopListening(EventName.System.Sheep.Kill(), OnKill);
    }
    void OnStartGame(GameMessage msg) {
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            nextSpawn.Add(owner, GetSpawnRate(owner));
        }
    }
    void Update() {
        if (GameStateView.HasState(GameState.started) && !GameStateView.HasState(GameState.ended)) {
            foreach (Owner owner in OwnersCoordinator.GetOwnersAlive()) {
                if (Time.time > nextSpawn[owner]) {
                    nextSpawn[owner] = GetSpawnRate(owner);
                    if (!SheepSpawnCapCoordinator.IsCapped(owner)) {
                        SpawnNewSheep(owner);
                    }
                    //Debug.Log("Time: " + Time.time + "  spawn:  " + nextSpawn[owner] + " d: " + (nextSpawn[owner] - Time.time));
                }
            }
        }
    }
    void OnKill(GameMessage msg) {
        SheepCoordinator.DestroySheep(msg.sheepUnit);
    }
    void SpawnNewSheep(Owner owner) {
        SheepUnit sheep = SheepCoordinator.SpawnSheep(owner);
        EventCoordinator.TriggerEvent(EventName.System.Sheep.Spawned(), GameMessage.Write().WithSheepUnit(sheep));
    }
    void OnEliminated(GameMessage msg) {
        SheepSpawnCapCoordinator.RemoveOwner(msg.targetOwner);
        SheepSpawnCapCoordinator.IncreaseCaps(ConstantsBucket.SheepSpawnCapIncrement);
        SheepCoordinator.IncreaseStacksSize(ConstantsBucket.SheepSpawnCapIncrement);
    }
    float GetSpawnRate(Owner owner) {
        int level = SpawnRateCoordinator.GetSpawnRateLevel(owner);
        float newNextSpawn = Random.Range(-scatterRandomBase, scatterRandomBase);
        return Time.time + (newNextSpawn + ConstantsBucket.SheepSpawnPeriod) * (Mathf.Pow(1 - ConstantsBucket.SheepSpawnUpgradeDecrement, level));
    }
}