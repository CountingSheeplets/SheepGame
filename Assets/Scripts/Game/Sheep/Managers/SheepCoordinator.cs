using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SheepCoordinator : Singleton<SheepCoordinator> {
    public List<SheepUnit> sheeps = new List<SheepUnit>();

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
    public static SheepUnit SpawnSheep(Owner owner) {
        SheepUnit sheep = SheepFactory.CreateSheep(owner);
        Instance.sheeps.Add(sheep);
        //SkeletonRendererController.MakeSheepActive(sheep);
        return sheep;
    }
    public static void DestroySheep(SheepUnit sheep) {
        if (Instance == null)
            return;
        Instance.sheeps.Remove(sheep);
        SheepFactory.DestroySheep(sheep);
    }
    public static List<SheepUnit> GetSheepInField(Playfield playfield) {
        List<SheepUnit> sheeps = new List<SheepUnit>();
        foreach (SheepUnit sheep in Instance.sheeps) {
            if (sheep.currentPlayfield == playfield)
                sheeps.Add(sheep);
        }
        return sheeps;
    }
    public static int GetSheepInFieldByType(Playfield playfield, SheepType typeMask) {
        int count = 0;
        foreach (SheepUnit sheep in Instance.sheeps) {
            if (sheep.currentPlayfield == playfield)
                if (typeMask != 0) {
                    if ((typeMask & sheep.sheepType) != 0) {
                        //here do stuff if at least some input types are matching with sheep

                    }
                } else {
                    count++;
                }
        }
        return count;
    }
    public static List<SheepUnit> GetSheeps(Owner owner) {
        return Instance.sheeps.Where(x => x.owner == owner).ToList();
    }
    public static List<SheepUnit> GetSheeps() {
        return Instance.sheeps;
    }
}