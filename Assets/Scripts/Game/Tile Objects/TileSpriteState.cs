using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TileSpriteState
{
    public FieldTileSpriteType[,] tileState = new FieldTileSpriteType[3,3];
    public FieldTileSpriteType[,] GetState(){
        return tileState;
    }
    public TileSpriteState SetState(FieldTileSpriteType[,] inputTileState){
        for(int i = 0; i < tileState.GetLength(0); i++){
            for(int j = 0; j < tileState.GetLength(1); j++){
                tileState[i,j] = inputTileState[i,j];
            }
        }
        return this;
    }
    public TileSpriteState SetState(Vector2 position, FieldTileSpriteType newState){
        tileState[(int)position.x, (int)position.y] = newState;
        return this;
    }
    public bool IsEqualByState(FieldTileSpriteType[,] inputTileState){
        for(int i = 0; i < tileState.GetLength(0); i++){
            for(int j = 0; j < tileState.GetLength(1); j++){
                if(tileState[i,j] != inputTileState[i,j]){
                    return false;
                }
            }
        }
        return true;
    }
    public bool IsEqualByState(TileSpriteState inputTileSpriteState){
        return IsEqualByState(inputTileSpriteState.tileState);
    }

    public void FillWater(){
        for(int i = 0; i < tileState.GetLength(0); i++){
            for(int j = 0; j < tileState.GetLength(1); j++){
                if(j != 1 || j != 1){
                    if(tileState[i,j] == FieldTileSpriteType.anyOrSelf){
                        tileState[i,j] = FieldTileSpriteType.water;
                    }
                }
            }
        }
    }
}
