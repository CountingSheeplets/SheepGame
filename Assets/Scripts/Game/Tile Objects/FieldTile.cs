using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : BaseTile
{
    public SubFieldTile[,] subTiles = new SubFieldTile[2,2];
    public bool isGrass = false;
    //public List<string> listenerNames = new List<string>();
    public void SetState(bool state){
        isGrass = state;
        FieldTileSpriteType stateType = FieldTileSpriteType.any;
        if(state){
            stateType = FieldTileSpriteType.grass;
        } else {
            stateType = FieldTileSpriteType.mud;
        }
        if(onListenStateChange != null)
            onListenStateChange(this, stateType);
    }
    void Awake(){
        List<SubFieldTile> subs = new List<SubFieldTile>(GetComponentsInChildren<SubFieldTile>());
        int index = 0;
        for(int i = 0; i < 2; i++){
            for(int j = 0; j < 2; j++){
                subTiles[j,i] = subs[index];//order is important
                index ++;
            }
        }
    }
    public delegate void OnStateChange (FieldTile tile, FieldTileSpriteType state);
    public OnStateChange onListenStateChange;
    public Dictionary<FieldTile, Location3x3> neighbours = new Dictionary<FieldTile, Location3x3>();
    public List<NeighbourItem> neighbTest = new List<NeighbourItem>();

    public void SubscribeToNeighbour(FieldTile tile, Location3x3 loc3x3){
        if(tile != null){
            if(!neighbours.ContainsKey(tile) && !loc3x3.IsCenterTile()){
                neighbTest.Add(new NeighbourItem(tile, new Vector2(loc3x3.y, loc3x3.x)));
                neighbours.Add(tile, loc3x3.ToLocal());
                tile.onListenStateChange += OnNeighbourStateChanged;
                //tile.listenerNames.Add(gameObject.name);
            }
        } else {
            FillWater(loc3x3.ToLocal());
        }
    }

    void OnNeighbourStateChanged(FieldTile tile, FieldTileSpriteType newState){
        Debug.Log("calling OnNeightbourh:"+gameObject.name+" by: "+tile.gameObject.name);
        for(int i = 0; i < 2; i++){
            for(int j = 0; j < 2; j++){
                //Debug.Log("affec check: "+subTiles[i,j]+" loc3: "+neighbours[tile]+" "+subTiles[i,j].IsAffected(neighbours[tile]));
                if(subTiles[i,j].IsAffected(neighbours[tile])){
                    Location2x2 loc2x2 = new Location2x2(neighbours[tile], i, j);
                    Debug.Log(subTiles[i,j]+" no exp: "+subTiles[i,j].GetState());
                    Debug.Log(subTiles[i,j]+" before: "+subTiles[i,j].ExportState());
                    subTiles[i,j].SetSubState(loc2x2, newState);
                    Debug.Log(subTiles[i,j]+" after:"+subTiles[i,j].ExportState());
                }
            }
        }
    }

    public void FillWater(Location3x3 loc3x3){
        for(int i = 0; i < 2; i++){
            for(int j = 0; j < 2; j++){
                if(subTiles[i,j].IsAffected(loc3x3)){
                    TileSpriteState newTileState = new TileSpriteState(i,j);
                    Location2x2 loc2x2 = new Location2x2(loc3x3, i, j);
                    subTiles[i,j].SetSubState(loc2x2, FieldTileSpriteType.water);
                }
            }
        }
    }
    void OnDestroy(){
        foreach(FieldTile tile in neighbours.Keys)
            tile.onListenStateChange -= OnNeighbourStateChanged;
    }
}
//testing stuff:
[System.Serializable]
public struct NeighbourItem {
    public FieldTile tileNeighb;
    public Vector2 position;
    public NeighbourItem(FieldTile tile, Vector2 pos){
        tileNeighb = tile;
        position = pos;
    }
}