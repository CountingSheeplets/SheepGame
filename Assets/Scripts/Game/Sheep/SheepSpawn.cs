﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawn : MonoBehaviour
{
    public GameObject sheepPrefab;
    public Transform sheepParent;
    Owner myOwner;
    void Start()
    {
        myOwner = GetComponent<Owner>();
        EventManager.StartListening(EventName.Input.KingAbilities.SpawnSheep(), OnSheepSpawn);
    }

    void OnSheepSpawn(GameMessage msg){
        if(msg.owner.EqualsByValue(myOwner)){
            GameObject newSheepGO = Instantiate(sheepPrefab, sheepParent);
            float rnd1 = Random.Range(-Mathf.FloorToInt(ArenaManager.GridSize/2),Mathf.CeilToInt(ArenaManager.GridSize/2)) * ArenaManager.TileSize;
            float rnd2 = Random.Range(-Mathf.FloorToInt(ArenaManager.GridSize/2),Mathf.CeilToInt(ArenaManager.GridSize/2)) * ArenaManager.TileSize;
            newSheepGO.transform.localPosition = new Vector2(rnd1,rnd2);
            SheepUnit sheep = newSheepGO.GetComponent<SheepUnit>();
            sheep.owner = myOwner;
            EventManager.TriggerEvent(EventName.System.Sheep.Spawned(), GameMessage.Write().WithSheepUnit(sheep));
        }
    }
}
