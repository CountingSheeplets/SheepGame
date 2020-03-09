using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingFactory : Singleton<KingFactory> {
    public GameObject defaultModel;
    public GameObject kingUnitPrefab;

    public static GameObject TryCreateHeroModel(Owner owner) {
        GameObject newHero = CreateHeroModel(owner.teamId);
        //newHero.GetComponentInChildren<KingModel>().ChangeColor(PlayerProfileCoordinator.GetProfile(owner).playerColor);
        newHero.transform.SetParent(owner.gameObject.transform);
        newHero.transform.localPosition = Vector3.zero;
        newHero.GetComponent<Animator>().Play("Idle1", -1, 0);
        return newHero;
    }

    public static GameObject CreateHeroModel(int teamId) {
        GameObject newKingModelGO = Instantiate(Instance.defaultModel);
        newKingModelGO.GetComponent<KingModel>().ChangeColor(teamId);
        return newKingModelGO;
    }

    public static KingUnit CreateKing(Owner owner) {
        GameObject newKingGO = Instantiate(Instance.kingUnitPrefab);
        KingUnit newKing = newKingGO.GetComponent<KingUnit>();
        newKing.owner = owner;
        newKingGO.transform.parent = ArenaCoordinator.GetPlayfield(owner).transform;
        GameObject newKingModelGO = CreateHeroModel(owner.teamId);
        newKingModelGO.transform.SetParent(newKingGO.transform);
        newKingModelGO.transform.localPosition = Vector3.zero;
        return newKing;
    }
}