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
            if(!neighbours.ContainsKey(tile)){
                neighbours.Add(tile, loc3x3);
                tile.onListenStateChange += OnNeighbourStateChanged;
                //tile.listenerNames.Add(gameObject.name);
            }
        } else {
            //this loc3x3 is water, mark it as water!
            bool[,] affected = loc3x3.GetAffectedSubTiles();
            //mark state
        }
    }

    void OnNeighbourStateChanged(FieldTile tile, FieldTileSpriteType newState){
        Debug.Log("calling OnNeightbourh:"+gameObject.name+" by: "+tile.gameObject.name);
        bool[,] affected = neighbours[tile].GetAffectedSubTiles();
        for(int i = 0; i < 2; i++){
            for(int j = 0; j < 2; j++){
                if(affected[i,j]){
                    TileSpriteState newTileState = new TileSpriteState(i,j);
                    Location2x2 loc2x2 = new Location2x2(neighbours[tile], i, j);
                    if(!newTileState.IsEqualByState(subTiles[i,j].GetState())){
                        Debug.Log("not equal!:");
                        Debug.Log("previous!:");
                        subTiles[i,j].GetState().PrintState();
                        Debug.Log("new!:");
                        newTileState.SetState(loc2x2, newState);
                        newTileState.PrintState();
                        Debug.Log("after!:");
                        subTiles[i,j].SetState(loc2x2, newState);
                        subTiles[i,j].GetState().PrintState();
                    }
                }
            }
        }
    }


    public void FillWater(){
        ////tileState.FillWater();
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