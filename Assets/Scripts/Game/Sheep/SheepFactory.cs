using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SheepFactory : Singleton<SheepFactory>
{
    public GameObject sheepPrefab;
    public GameObject sheepModel;

    public static SheepUnit CreateSheep(Owner owner){
        Playfield playfield = ArenaCoordinator.GetPlayfield(owner);
        GameObject newSheepGO = Instantiate(Instance.sheepPrefab);
        float rnd1 = Random.Range(-Mathf.FloorToInt(ConstantsBucket.GridSize/2),Mathf.CeilToInt(ConstantsBucket.GridSize/2)) * ConstantsBucket.PlayfieldTileSize;
        float rnd2 = Random.Range(-Mathf.FloorToInt(ConstantsBucket.GridSize/2),Mathf.CeilToInt(ConstantsBucket.GridSize/2)) * ConstantsBucket.PlayfieldTileSize;
        newSheepGO.transform.parent = playfield.sheepParent;
        newSheepGO.transform.localPosition = new Vector2(rnd1,rnd2);
        SheepUnit sheep = newSheepGO.GetComponent<SheepUnit>();
        sheep.owner = owner;
        sheep.currentPlayfield = playfield;

        GameObject newSheepModel = Instantiate(Instance.sheepModel);
        newSheepModel.transform.parent = newSheepGO.transform;
        newSheepModel.transform.localPosition = Vector3.zero;
        //set color:
        SpineContainer container = newSheepModel.GetComponent<SpineContainer>();
        SkeletonMecanim skMecanim = container.GetComponent<SkeletonMecanim>();
        foreach(Spine.Slot slot in skMecanim.skeleton.Slots){
            if(slot.Data.Name.Contains("Color")){
                Color newColor = owner.GetPlayerProfile().playerColor;
                newColor.a = slot.GetColor().a;
                Debug.Log("setting color of component: "+slot.Data.Name+"  to:"+newColor);
                slot.SetColor(newColor);
            }
        }
        return sheep;
    }

    public static void DestroySheep(SheepUnit sheep){
        if(Instance == null)
            return;
        Destroy(sheep.gameObject);
    }

}
