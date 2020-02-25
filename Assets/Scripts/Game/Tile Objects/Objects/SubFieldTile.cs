using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubFieldTile : MonoBehaviour
{
    public Location2x2 myLoc;
    public SpriteRenderer mySprite;
    bool isDirty = true;
    public void SetDirty(){
        isDirty = true;
    }

    TileSpriteState tileState;
    public TileSpriteState GetState(){
        return tileState;
    }
    public TileSpriteState SetState(TileSpriteState inputState){
        tileState.SetState(inputState);
        SetDirty();
        return tileState;
    }
    public TileSpriteState SetState(Location2x2 loc2x2, FieldTileSpriteType newState){
        tileState.SetState(loc2x2, newState);
        SetDirty();
        return tileState;
    }


    void Awake(){
        tileState = new TileSpriteState(myLoc);
    }
    void Update(){
        if(isDirty){
            SpriteSet();
            isDirty = false;
        }
    }

    void SpriteSet(){
        Debug.Log("------------------entering new sprite of SUBtile... : "+gameObject.name);
        Debug.Log("for state: ");
        tileState.PrintState();
        mySprite.sprite = TileSpriteFactory.GetSprite(tileState);
        Debug.Log("sprite was set: "+mySprite.sprite.name);
    }
}