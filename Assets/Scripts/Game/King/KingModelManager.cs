using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingModelManager : Singleton<KingModelManager>
{
    public GameObject defaultModel;
    //public List<GameObject> heroes = new List<GameObject>();
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
}
