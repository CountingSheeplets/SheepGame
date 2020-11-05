using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SheepFactory : Singleton<SheepFactory> {
    public GameObject sheepPrefab;
    public GameObject sheepModel;

    public static SheepUnit CreateSheep(Owner owner, SheepType sheepType) {
        Playfield playfield = ArenaCoordinator.GetPlayfield(owner);
        GameObject newSheepGO = Instantiate(Instance.sheepPrefab);
        newSheepGO.name = "SheepUnit_" + Time.time.GetHashCode();
        float rnd1 = Random.Range(-Mathf.FloorToInt(ConstantsBucket.PlayfieldSize), Mathf.CeilToInt(ConstantsBucket.PlayfieldSize)) / 2f;
        float rnd2 = Random.Range(-Mathf.FloorToInt(ConstantsBucket.PlayfieldSize), Mathf.CeilToInt(ConstantsBucket.PlayfieldSize)) / 2f;
        newSheepGO.transform.parent = playfield.sheepParent;
        newSheepGO.transform.localPosition = new Vector2(rnd1, rnd2);
        newSheepGO.transform.localScale = new Vector3(1, 1, 1);
        SheepUnit sheep = newSheepGO.GetComponent<SheepUnit>();
        sheep.owner = owner;
        sheep.currentPlayfield = playfield;
        sheep.sheepType = sheepType;

        GameObject newSheepModel = CreateSheepModel(owner, newSheepGO.transform);

        newSheepModel.GetComponent<SpineContainerBlendsEight>().SetInitialRandomDirection();
        return sheep;
    }
    public static GameObject CreateSheepModel(Owner owner, Transform parent) {
        GameObject newSheepModel = Instantiate(Instance.sheepModel);
        Vector3 modelScale = newSheepModel.transform.localScale;
        newSheepModel.transform.parent = parent;
        newSheepModel.transform.localPosition = Vector3.zero;
        newSheepModel.transform.localScale = modelScale;
        //set model properties:
        newSheepModel.GetComponentInChildren<SheepModel>().ChangeColor(owner.teamId);
        newSheepModel.GetComponentInChildren<SheepModel>().EnabeUpgradeSlot();
        return newSheepModel;
    }

    public static void DestroySheep(SheepUnit sheep) {
        if (Instance == null)
            return;
        if (sheep != null)
            Destroy(sheep.gameObject);
    }

}