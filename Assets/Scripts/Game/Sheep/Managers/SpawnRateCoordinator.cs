using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRateCoordinator : Singleton<SpawnRateCoordinator> {
    public Dictionary<Owner, int> spawnRateLevel = new Dictionary<Owner, int>();
    public static int GetSpawnRateLevel(Owner owner) {
        if (Instance.spawnRateLevel.ContainsKey(owner)) {
            return Instance.spawnRateLevel[owner];
        }
        return 0;
    }
    public static int IncreaseSpawnRateLevel(Owner owner) {
        if (Instance.spawnRateLevel.ContainsKey(owner)) {
            Instance.spawnRateLevel[owner]++;
            return Instance.spawnRateLevel[owner];
        } else {
            Instance.spawnRateLevel.Add(owner, 1);
            return 1;
        }
    }
}