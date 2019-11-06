using System.Collections;
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
            GameObject newSheep = Instantiate(sheepPrefab, sheepParent);
            float rnd1 = Random.Range(0,ArenaManager.fieldSize.x);
            float rnd2 = Random.Range(0,ArenaManager.fieldSize.y);
            newSheep.transform.position = new Vector2(rnd1,rnd2);
        }
    }
}
