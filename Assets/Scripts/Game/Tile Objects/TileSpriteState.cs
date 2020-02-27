using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[Serializable]
public class TileSpriteState
{
    public TileSpriteState(TileSpriteState inputTileSpriteState){
        SetState(inputTileSpriteState);
    }
    public TileSpriteState(Location2x2 loc){
        tileState[loc.y,loc.x] = FieldTileSpriteType.self;
    }
    public TileSpriteState(int selfX, int selfY){
        tileState[selfY,selfX] = FieldTileSpriteType.self;
    }
    public FieldTileSpriteType[,] tileState = new FieldTileSpriteType[2,2];
    public FieldTileSpriteType[,] GetStateTypeArray(){
        return tileState;
    }
    public FieldTileSpriteType[,] GetState(){
        return tileState;
    }
    public TileSpriteState SetState(TileSpriteState inputTileSpriteState){
        SetState(inputTileSpriteState.tileState);
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
    public TileSpriteState SetSelfToState(FieldTileSpriteType newState){
        for(int i = 0; i < tileState.GetLength(0); i++){
            for(int j = 0; j < tileState.GetLength(1); j++){
                if(tileState[i,j] == FieldTileSpriteType.self)
                    tileState[i,j] = newState;
                if(tileState[i,j] == FieldTileSpriteType.water)
                    tileState[i,j] = FieldTileSpriteType.mud;
            }
        }
        return this;
    }
    public TileSpriteState SetSubState(Location2x2 loc2x2, FieldTileSpriteType newState){
        if(tileState[loc2x2.y, loc2x2.x] != FieldTileSpriteType.self)
            tileState[loc2x2.y, loc2x2.x] = newState;
        return this;
    }
    public FieldTileSpriteType GetSubStateType(Location2x2 loc2x2){
        return tileState[loc2x2.y, loc2x2.x];
    }
    public bool EqualsByState(FieldTileSpriteType[,] inputTileState){
        if(EqualsBySequence(inputTileState)){
            return true;
        }
        for(int i = 0; i < tileState.GetLength(0); i++){
            for(int j = 0; j < tileState.GetLength(1); j++){
                if(tileState[i,j] != inputTileState[i,j]){
                    return false;
                }
            }
        }
        return true;
    }
    bool EqualsBySequence(FieldTileSpriteType[,] inputTileState){
        return tileState.Rank == inputTileState.Rank &&
        Enumerable.Range(0,tileState.Rank).All(dimension => tileState.GetLength(dimension) == inputTileState.GetLength(dimension)) &&
        tileState.Cast<FieldTileSpriteType>().SequenceEqual(inputTileState.Cast<FieldTileSpriteType>());
        
    }
    public bool IsMatching(FieldTileSpriteType[,] inputTileState){ //2x2 input
        if(EqualsBySequence(inputTileState)){
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
            if(EqualsByState(state))
                return true;
        }
        return false;
    }
    public bool IsInListByMatching(List<FieldTileSpriteType[,]> inputTileStates){
        foreach(FieldTileSpriteType[,] state in inputTileStates){
            if(IsMatching(state))
                return true;
        }
        return false;
    }
    public bool IsMatching(TileSpriteState inputTileSpriteState){
        return IsMatching(inputTileSpriteState.GetState());
    }
    public bool EqualsByState(TileSpriteState inputTileSpriteState){
        return EqualsByState(inputTileSpriteState.tileState);
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
        Debug.Log(this);
    }
    public override string ToString(){
        string allOutput = "\n";
        for(int i = 0; i < tileState.GetLength(0); i++){
            string output = "";
            for(int j = 0; j < tileState.GetLength(1); j++){
                output += tileState[i,j] + " - ";
            }
            allOutput += i+" row TileState: "+output+ " \n";
        }
        return allOutput;
    }
}
