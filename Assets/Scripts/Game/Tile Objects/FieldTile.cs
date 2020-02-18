using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : BaseTile
{
    public Color activeCol;
    public Color passiveCol;
    public SpriteRenderer mySprite;
    public void SetState(bool state){
        mySprite.enabled = state;
        FieldTileSpriteType typeState = FieldTileSpriteType.anyOrSelf;
        if(state){
            typeState = FieldTileSpriteType.grass;
        } else {
            typeState = FieldTileSpriteType.mud;
        }
        onListenStateChange(this, typeState);
    }
    public delegate void OnStateChange (FieldTile tile, FieldTileSpriteType state);
    public OnStateChange onListenStateChange;
    public Dictionary<FieldTile, Vector2> neighbours = new Dictionary<FieldTile, Vector2>();

    public TileSpriteState tileState = new TileSpriteState();

    public void SubscribeToNeighbour(FieldTile tile, Vector2 location){
        if(!neighbours.ContainsKey(tile)){
            neighbours.Add(tile, LocToState(location));
            tile.onListenStateChange += OnNeighbourStateChanged;
        }
    }

    void OnNeighbourStateChanged(FieldTile tile, FieldTileSpriteType newState){
        tile.tileState.SetState(new Vector2((int)neighbours[tile].x, (int)neighbours[tile].y), newState);
        //update sprite here, by new state
        //sprite = FieldTileSpriteBucket.GetSprite(TileSpriteState)
    }

    Vector2 LocToState(Vector2 location){
        return new Vector2(location.x+1, location.y+1);
    }
    public void FillWater(){
        tileState.FillWater();
    }
}

