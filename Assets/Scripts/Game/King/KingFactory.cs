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
        newHero.GetComponent<Animator>().SetFloat("dirX_blend", 0);
        newHero.GetComponent<Animator>().SetFloat("dirY_blend", -1);
        return newHero;
    }

    public static GameObject CreateHeroModel(int teamId) {
        GameObject newKingModelGO = Instantiate(Instance.defaultModel);
        newKingModelGO.GetComponent<KingModel>().ChangeColor(teamId);
        newKingModelGO.GetComponent<SpineContainerBlendsFour>().SetInitialRandomDirection();
        return newKingModelGO;
    }

    public static KingUnit CreateKing(Owner owner) {
        GameObject newKingGO = Instantiate(Instance.kingUnitPrefab);
        KingUnit newKing = newKingGO.GetComponent<KingUnit>();
        newKing.owner = owner;
        newKingGO.transform.parent = ArenaCoordinator.GetPlayfield(owner).GetComponentInChildren<PlayfieldOffsetHandler>().transform;
        newKingGO.transform.localPosition = Vector3.zero;
        GameObject newKingModelGO = CreateHeroModel(owner.teamId);
        int hat = KingCoordinator.GetSourceKingModel(owner).HatIndex;
        int scept = KingCoordinator.GetSourceKingModel(owner).ScepterIndex;
        KingModel model = newKingModelGO.GetComponent<KingModel>();
        model.SetHat(hat);
        model.SetScepter(scept);
        newKingModelGO.transform.SetParent(newKingGO.transform);
        newKingModelGO.transform.localPosition = Vector3.zero;
        return newKing;
    }
}