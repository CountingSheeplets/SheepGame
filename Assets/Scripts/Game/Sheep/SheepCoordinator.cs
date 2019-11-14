using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepCoordinator : Singleton<SheepCoordinator>
{
    public List<SheepUnit> sheeps = new List<SheepUnit>();

    public static SheepUnit SpawnSheep(Owner owner){
        SheepUnit sheep = SheepFactory.CreateSheep(owner);
        Instance.sheeps.Add(sheep);
        return sheep;
    }
}