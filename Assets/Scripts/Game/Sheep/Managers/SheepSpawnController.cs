using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawnController : MonoBehaviour {
    float scatterRandomBase = 0.2f;
    Dictionary<Owner, float> nextSpawn = new Dictionary<Owner, float>();
    void Start() {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnStartGame(GameMessage msg) {
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            nextSpawn.Add(owner, GetSpawnRate(owner));
        }
    }
    void Update() {
        if (GameStateView.HasState(GameState.started)) {
            foreach (Owner owner in OwnersCoordinator.GetOwnersAlive()) {
                if (Time.time > nextSpawn[owner]) {
                    nextSpawn[owner] = GetSpawnRate(owner);
                    if (!SheepSpawnCapCoordinator.IsCapped(owner))
                        SpawnNewSheep(owner);
                    //Debug.Log("Time: " + Time.time + "  spawn:  " + nextSpawn[owner] + " d: " + (nextSpawn[owner] - Time.time));
                }
            }
        }
    }

    void SpawnNewSheep(Owner owner) {
        SheepUnit sheep = SheepCoordinator.SpawnSheep(owner);
        EventCoordinator.TriggerEvent(EventName.System.Sheep.Spawned(), GameMessage.Write().WithSheepUnit(sheep));
    }

    float GetSpawnRate(Owner owner) {
        int level = SheepCoordinator.GetSpawnRateLevel(owner);
        float newNextSpawn = Random.Range(-scatterRandomBase, scatterRandomBase);
        return Time.time + (newNextSpawn + ConstantsBucket.SheepSpawnPeriod) * (Mathf.Pow(1 - ConstantsBucket.SheepSpawnUpgradeDecrement, level));
    }
}