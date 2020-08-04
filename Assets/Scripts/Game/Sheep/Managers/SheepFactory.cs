using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SheepFactory : Singleton<SheepFactory> {
    public GameObject sheepPrefab;
    public GameObject sheepModel;

    public static SheepUnit CreateSheep(Owner owner) {
        Playfield playfield = ArenaCoordinator.GetPlayfield(owner);
        GameObject newSheepGO = Instantiate(Instance.sheepPrefab);
        newSheepGO.name = "SheepUnit_" + Time.time.GetHashCode();
        float rnd1 = Random.Range(-Mathf.FloorToInt(ConstantsBucket.GridSize / 2), Mathf.CeilToInt(ConstantsBucket.GridSize / 2)) * ConstantsBucket.PlayfieldTileSize;
        float rnd2 = Random.Range(-Mathf.FloorToInt(ConstantsBucket.GridSize / 2), Mathf.CeilToInt(ConstantsBucket.GridSize / 2)) * ConstantsBucket.PlayfieldTileSize;
        newSheepGO.transform.parent = playfield.sheepParent;
        newSheepGO.transform.localPosition = new Vector2(rnd1, rnd2);
        newSheepGO.transform.localScale = new Vector3(1, 1, 1);
        SheepUnit sheep = newSheepGO.GetComponent<SheepUnit>();
        sheep.owner = owner;
        sheep.currentPlayfield = playfield;

        GameObject newSheepModel = Instantiate(Instance.sheepModel);
        Vector3 modelScale = newSheepModel.transform.localScale;
        newSheepModel.transform.parent = newSheepGO.transform;
        newSheepModel.transform.localPosition = Vector3.zero;
        newSheepModel.transform.localScale = modelScale;
        //set color:
        newSheepModel.GetComponentInChildren<SheepModel>().ChangeColor(owner.teamId);
        //set random direction:
        /*         SpineContainerEightDirWalk container = newSheepModel.GetComponent<SpineContainerEightDirWalk>();
                SkeletonMecanim skMecanim = container.GetComponent<SkeletonMecanim>();
                foreach (Spine.Slot slot in skMecanim.skeleton.Slots) {
                    if (slot.Data.Name.Contains("Color")) {
                        Color newColor = owner.GetPlayerProfile().playerColor;
                        newColor.a = slot.GetColor().a;
                        Debug.Log("setting color of component: " + slot.Data.Name + "  to:" + newColor);
                        slot.SetColor(newColor);
                    }
                } */
        return sheep;
    }

    public static void DestroySheep(SheepUnit sheep) {
        if (Instance == null)
            return;
        Destroy(sheep.gameObject);
    }

}