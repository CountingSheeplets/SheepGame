using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : BaseTile
{
    ////public Color activeCol;
    //public Color passiveCol;
    public SpriteRenderer mySprite;
    public void SetState(bool state){
        FieldTileSpriteType stateType = FieldTileSpriteType.anyOrSelf;
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

    public TileSpriteState[] tileState = new TileSpriteState[4];

    public void SubscribeToNeighbour(FieldTile tile, Vector2 location){
        if(tile != null){
            if(!neighbours.ContainsKey(tile)){
                neighbours.Add(tile, LocToState(location));
                tile.onListenStateChange += OnNeighbourStateChanged;
            }
        } else {
            //dont set sprite, only state. instead set sprite if it's dirty on update.
            //tileState[TileSpriteFactory.GetAffectedSprites(LocToState(location))].SetState(LocToState(location), FieldTileSpriteType.water);
        }
    }

    void OnNeighbourStateChanged(FieldTile tile, FieldTileSpriteType newState){
        //tileState.SetState(new Vector2((int)neighbours[tile].x, (int)neighbours[tile].y), newState);
            //dont set sprite, only state. instead set sprite if it's dirty on update.
        //mySprite.sprite = TileSpriteFactory.GetSprite(tileState);
        Debug.Log("entering new state of tile: "+gameObject.name);
        //tileState.PrintState();
    }

    Vector2 LocToState(Vector2 location){
        return new Vector2(location.x+1, location.y+1);
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

