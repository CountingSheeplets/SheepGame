using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : BaseTile
{
    ////public Color activeCol;
    //public Color passiveCol;
    bool isSpriteDirty = false;
    public SpriteRenderer[] mySprites = new SpriteRenderer[4];

    void Update(){
        if(isSpriteDirty){
            SpriteSet();
            isSpriteDirty = false;
        }
    }
    public void SetState(bool state){
        FieldTileSpriteType stateType = FieldTileSpriteType.any;
        if(state){
            stateType = FieldTileSpriteType.grass;
        } else {
            stateType = FieldTileSpriteType.mud;
        }
        if(onListenStateChange != null)
            onListenStateChange(this, stateType);
    }
    public delegate void OnStateChange (FieldTile tile, FieldTileSpriteType state);
    public OnStateChange onListenStateChange;
    public Dictionary<FieldTile, Vector2> neighbours = new Dictionary<FieldTile, Vector2>();
    public List<NeighbourItem> neighbTest = new List<NeighbourItem>();
    TileSpriteState[] tileState = {
        new TileSpriteState(0, 0),
        new TileSpriteState(0, 1),
        new TileSpriteState(1, 0),
        new TileSpriteState(1, 1)
    };

    public void SubscribeToNeighbour(FieldTile tile, Vector2 location){
        if(tile != null){
            if(!neighbours.ContainsKey(tile)){
                neighbours.Add(tile, LocTo3x3State(location));
                tile.onListenStateChange += OnNeighbourStateChanged;
            }
        } else {
            isSpriteDirty = true;
        }
    }

    void OnNeighbourStateChanged(FieldTile tile, FieldTileSpriteType newState){
        bool[,] affected = TileSpriteFactory.GetAffectedSprites(neighbours[tile]);
        int index = 0;
        //instead of all of this, make another small object, SubTileState, 1 for each total of 4
        //this will hold list of these
        //then here just call each 'affected' pointing the state. set dirty there, not here.
        //this object would just fetch info for them, they themselves would set dirty if their state changes
        for(int i = 0; i < 2; i++){
            for(int j = 0; j < 2; j++){
                if(affected[i,j]){
                    TileSpriteState newTileState = new TileSpriteState(i,j);
                    Vector2 loc2x2 = LocTo2x2State(neighbours[tile], i, j);
                    if(!newTileState.IsEqualByState(tileState[index])){
                        Debug.Log("not equal!:");
                        Debug.Log("previous!:");
                        tileState[index].PrintState();
                        Debug.Log("new!:");
                        newTileState.SetState(loc2x2, newState);
                        newTileState.PrintState();
                        Debug.Log("after!:");
                        tileState[index].SetState(loc2x2, newState);
                        tileState[index].PrintState();
                        isSpriteDirty = true;
                    }
                    index++;
                }
            }
        }
    }
    void SpriteSet(){
        Debug.Log("------------------entering new sprite of tile... : "+gameObject.name);
        for(int i = 0; i < 4; i++){
            //Debug.Log("sprite: "+i);
            mySprites[i].sprite = TileSpriteFactory.GetSprite(tileState[i]);
            //tileState[i].PrintState();
        }
        //Debug.Log("----------------------sprite changed! : "+gameObject.name);
    }
    Vector2 LocTo3x3State(Vector2 location){
        return new Vector2(location.x+1, location.y+1);
    }
    Vector2 LocTo2x2State(Vector2 location3x3, int affTileX, int affTileY){
        return new Vector2(location3x3.x - affTileX, location3x3.y - affTileY);
    }
    public void FillWater(){
        ////tileState.FillWater();
    }
    void OnDestroy(){
        foreach(FieldTile tile in neighbours.Keys)
            tile.onListenStateChange -= OnNeighbourStateChanged;
    }
    //public TileSpriteState GetTileSprites()
}

[System.Serializable]
public struct NeighbourItem {
    public FieldTile tileNeighb;
    public Vector2 position;
    public NeighbourItem(FieldTile tile, Vector2 pos){
        tileNeighb = tile;
        position = pos;
    }
}