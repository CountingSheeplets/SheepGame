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
    public static void DestroySheep(SheepUnit sheep){
        Instance.sheeps.Remove(sheep);
        SheepFactory.DestroySheep(sheep);
    }
    public static List<SheepUnit>  GetSheepInField(Playfield playfield){
        List<SheepUnit> sheeps = new List<SheepUnit>();
        foreach(SheepUnit sheep in Instance.sheeps){
            if(sheep.currentPlayfield == playfield)
                sheeps.Add(sheep);
        }
        return sheeps;
    }
    public static int GetSheepInFieldByType(Playfield playfield, SheepType typeMask){
        int count = 0;
        foreach(SheepUnit sheep in Instance.sheeps){
            if(sheep.currentPlayfield == playfield)
                if(typeMask != 0){
                    if((typeMask & sheep.sheepType) != 0){
                        //here do stuff if at least some input types are matching with sheep
                        
                    }
                } else {
                    count++;
                }
        }
        return count;
    }
    public static List<SheepUnit> GetSheeps(){
        return Instance.sheeps;
    }
}