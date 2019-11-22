﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingFactory : Singleton<KingFactory>
{
    //public List<KingUnit> kings = new List<KingUnit>(); // belongs to coordinator type script
    public GameObject defaultModel;
    public GameObject kingUnitPrefab;

    public static GameObject TryCreateHeroModel(Owner owner){
        GameObject newHero = Instantiate(GetHeroModel(owner.ownerId));
        newHero.transform.parent = owner.gameObject.transform;
        newHero.transform.localPosition = Vector3.zero;
        return newHero;
    }

    public static GameObject GetHeroModel(string ownerID){
        //if player has saved a hat selection, load that hat

        //else just load default model
        return Instance.defaultModel;
    }

    public static KingUnit CreateKing(Owner owner){
        GameObject newKingGO = Instantiate(Instance.kingUnitPrefab);
        KingUnit newKing = newKingGO.GetComponent<KingUnit>();
        newKing.owner = owner;
        newKingGO.transform.parent = ArenaCoordinator.GetPlayfield(owner).transform;
        newKingGO.transform.localPosition = Vector3.zero;
        GameObject newKingModelGO = Instantiate(GetHeroModel(owner.ownerId));
        newKingModelGO.transform.parent = newKingGO.transform;
        newKingModelGO.transform.localPosition = Vector3.zero;
        //Instance.kings.Add(newKing);
        return newKing;
    } 
/*     public static void DestroyKing(KingUnit king){
        Instance.kings.Remove(king);
        Destroy(king);
        Create a die king sprite animation with a small lifetime
    } */
}