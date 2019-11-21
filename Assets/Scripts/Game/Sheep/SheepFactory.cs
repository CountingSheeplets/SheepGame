using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepFactory : Singleton<SheepFactory>
{
    public GameObject sheepPrefab;

    public static SheepUnit CreateSheep(Owner owner){
        Playfield playfield = ArenaCoordinator.GetPlayfield(owner);
        GameObject newSheepGO = Instantiate(Instance.sheepPrefab);
        float rnd1 = Random.Range(-Mathf.FloorToInt(ArenaCoordinator.GridSize/2),Mathf.CeilToInt(ArenaCoordinator.GridSize/2)) * ArenaCoordinator.TileSize;
        float rnd2 = Random.Range(-Mathf.FloorToInt(ArenaCoordinator.GridSize/2),Mathf.CeilToInt(ArenaCoordinator.GridSize/2)) * ArenaCoordinator.TileSize;
        newSheepGO.transform.parent = playfield.sheepParent;
        newSheepGO.transform.localPosition = new Vector2(rnd1,rnd2);
        SheepUnit sheep = newSheepGO.GetComponent<SheepUnit>();
        sheep.owner = owner;
        sheep.currentPlayfield = playfield;

        return sheep;
    }

    public static void DestroySheep(SheepUnit sheep){
        Destroy(sheep.gameObject);
        //instanciate a temp sheep death obj here:
        ///Instanciate()
    }

}
