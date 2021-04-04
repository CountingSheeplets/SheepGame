using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Rendering;
public class SheepFactory : PersistentFactory {
    public GameObject sheepModel;

    private static SheepFactory _instance;
    public static SheepFactory Instance {
        get {
            if (_instance != null)
                return _instance;
            _instance = FindObjectOfType(typeof(SheepFactory))as SheepFactory;
            return _instance;
        }
    }

    public static SheepUnit CreateSheep(Owner owner, SheepType sheepType) {
        Playfield playfield = owner.GetPlayfield();
        GameObject newSheepGO = Instance.GetOrCreateItem();
        if (!newSheepGO.name.Contains("SheepUnit"))
            newSheepGO.name = "SheepUnit_" + newSheepGO.name;
        SortingGroup sGroup = newSheepGO.GetComponentInChildren<SortingGroup>();
        if (sGroup)
            sGroup.sortingOrder = 100;
        float rnd1 = Random.Range(-Mathf.FloorToInt(ConstantsBucket.PlayfieldSize), Mathf.CeilToInt(ConstantsBucket.PlayfieldSize)) / 2f;
        float rnd2 = Random.Range(-Mathf.FloorToInt(ConstantsBucket.PlayfieldSize), Mathf.CeilToInt(ConstantsBucket.PlayfieldSize)) / 2f;
        newSheepGO.transform.parent = playfield.sheepParent;
        newSheepGO.transform.localPosition = new Vector2(rnd1, rnd2);
        newSheepGO.transform.localScale = new Vector3(1, 1, 1);
        SheepUnit sheep = newSheepGO.GetComponent<SheepUnit>();
        sheep.owner = owner;
        sheep.currentPlayfield = playfield;
        sheep.sheepType = sheepType;
        SheepModel model = newSheepGO.GetComponentInChildren<SheepModel>();
        if (model == null)
            model = CreateSheepModel(owner, newSheepGO.transform);
        else
            SetupSheepModel(model, owner, newSheepGO.transform);
        model.GetComponent<SpineContainerBlendsEight>().SetInitialRandomDirection();
        return sheep;
    }
    public static SheepModel CreateSheepModel(Owner owner, Transform parent) {
        GameObject newSheepModel = Instantiate(((SheepFactory)Instance).sheepModel);
        SheepModel model = newSheepModel.GetComponent<SheepModel>();
        SetupSheepModel(model, owner, parent);
        return model;
    }

    static void SetupSheepModel(SheepModel model, Owner owner, Transform parent) {
        GameObject newSheepModel = model.gameObject;
        Vector3 modelScale = newSheepModel.transform.localScale;
        newSheepModel.transform.parent = parent;
        newSheepModel.transform.localPosition = Vector3.zero;
        newSheepModel.transform.localScale = modelScale;
        //set model properties:
        model.ChangeColor(owner.teamId);
        model.DisableAllAttachments();
        model.EnabeUpgradeSlot();
    }

    public static void DestroySheep(SheepUnit sheep) {
        if (Instance == null)
            return;
        Instance.HideObject(sheep.gameObject);
        sheep.TrigSheepDestroy();
    }

}