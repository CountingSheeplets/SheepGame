using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[Serializable]
public class TileSpriteState
{
    public FieldTileSpriteType[,] tileState = new FieldTileSpriteType[2,2];
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
        var equal =
        tileState.Rank == inputTileState.Rank &&
        Enumerable.Range(0,tileState.Rank).All(dimension => tileState.GetLength(dimension) == inputTileState.GetLength(dimension)) &&
        tileState.Cast<FieldTileSpriteType>().SequenceEqual(inputTileState.Cast<FieldTileSpriteType>());
        if(equal)
            return true;
        for(int i = 0; i < tileState.GetLength(0); i++){
            for(int j = 0; j < tileState.GetLength(1); j++){
                if(inputTileState[i,j] == FieldTileSpriteType.anyOrSelf)
                    continue;
                if(tileState[i,j] != inputTileState[i,j]){
                    return false;
                }
            }
        }
        return true;
    }
    public bool IsInListByState(List<FieldTileSpriteType[,]> inputTileStates){
        foreach(FieldTileSpriteType[,] state in inputTileStates){
            if(IsEqualByState(state))
                return true;
        }
        return false;
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
    public void PrintState(){
        for(int i = 0; i < tileState.GetLength(0); i++){
            string output = "";
            for(int j = 0; j < tileState.GetLength(1); j++){
                output += tileState[i,j] + " - ";
            }
            Debug.Log(i+" row TileState: "+output);
        }
    }
}
