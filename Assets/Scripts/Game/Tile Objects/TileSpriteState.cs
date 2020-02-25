using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[Serializable]
public class TileSpriteState
{
    public TileSpriteState(Location2x2 loc){
        tileState[loc.x,loc.y] = FieldTileSpriteType.self;
    }
    public TileSpriteState(int selfX, int selfY){
        tileState[selfX,selfY] = FieldTileSpriteType.self;
    }
    public FieldTileSpriteType[,] tileState = new FieldTileSpriteType[2,2];
    public FieldTileSpriteType[,] GetStateTypeArray(){
        return tileState;
    }
    public TileSpriteState SetState(TileSpriteState inputTileSpriteState){
        tileState = inputTileSpriteState.tileState;
        return this;
    }
    public TileSpriteState SetState(FieldTileSpriteType[,] inputTileState){
        for(int i = 0; i < tileState.GetLength(0); i++){
            for(int j = 0; j < tileState.GetLength(1); j++){
                tileState[i,j] = inputTileState[i,j];
            }
        }
        return this;
    }
    public TileSpriteState SetState(Location2x2 loc2x2, FieldTileSpriteType newState){
        tileState[loc2x2.x, loc2x2.y] = newState;
        return this;
    }
    public bool IsEqualByState(FieldTileSpriteType[,] inputTileState){ //2x2 input
        var equal =
        tileState.Rank == inputTileState.Rank &&
        Enumerable.Range(0,tileState.Rank).All(dimension => tileState.GetLength(dimension) == inputTileState.GetLength(dimension)) &&
        tileState.Cast<FieldTileSpriteType>().SequenceEqual(inputTileState.Cast<FieldTileSpriteType>());
        if(equal){
            //Debug.Log("TileSpriteState returning true by sequence equality...");
            return true;
        }
        for(int i = 0; i < tileState.GetLength(0); i++){
            for(int j = 0; j < tileState.GetLength(1); j++){
                if(inputTileState[i,j] == FieldTileSpriteType.any || tileState[i,j] == FieldTileSpriteType.any)
                    continue;
                if(tileState[i,j] != inputTileState[i,j]){
                    //Debug.Log(tileState[i,j]+":::  tileState[i,j] != inputTileState[i,j]::" + inputTileState[i,j]);
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
                    if(tileState[i,j] == FieldTileSpriteType.any){
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
