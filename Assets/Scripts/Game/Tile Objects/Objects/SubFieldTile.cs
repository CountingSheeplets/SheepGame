using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubFieldTile : MonoBehaviour
{
    public Location2x2 myLoc;
    public SpriteRenderer mySprite;
    bool isDirty = true;
    FieldTileSpriteType myParentStateType;
    public FieldTileSpriteType GetParentType(){
        return myParentStateType;
    }
    public void SetDirty(){
        isDirty = true;
    }

    TileSpriteState tileState;
    public TileSpriteState ExportState(){
        TileSpriteState exportState = new TileSpriteState(tileState);
        exportState.SetSelfToState(myParentStateType);
        return exportState;
    }
    public TileSpriteState GetState(){
        return tileState;
    }
    public FieldTileSpriteType GetSubStateType(Location2x2 loc2x2){
        return tileState.GetSubStateType(loc2x2);
    }
    public TileSpriteState SetState(TileSpriteState inputState){
        tileState.SetState(inputState);
        SetDirty();
        return tileState;
    }
    public TileSpriteState SetSubState(Location2x2 loc2x2, FieldTileSpriteType newState){
        if (GetSubStateType(loc2x2) != newState){
            tileState.SetSubState(loc2x2, newState);
            SetDirty();
        }
        return tileState;
    }
    void OnParentStateChanged(FieldTile tile, FieldTileSpriteType newState){
        if(myParentStateType != newState)
            SetDirty();
        myParentStateType = newState;
        
    }
    void Awake(){
        tileState = new TileSpriteState(myLoc.Inverted());
        GetComponentInParent<FieldTile>().onListenStateChange += OnParentStateChanged;
    }
    void OnDestroy(){
        if(GetComponentInParent<FieldTile>())
            GetComponentInParent<FieldTile>().onListenStateChange -= OnParentStateChanged;
    }
    void Update(){
        if(isDirty){
            SpriteSet();
            isDirty = false;
        }
    }

    void SpriteSet(){
        //Debug.Log("------------------entering new sprite of SUBtile... : "+gameObject.name);
        //Debug.Log("for state: ");
        ///tileState.PrintState();
        mySprite.sprite = TileSpriteFactory.GetSprite(this);
        //Debug.Log("sprite was set: "+mySprite.sprite.name);
    }

    public bool IsAffected(Location3x3 loc3x3){
        if(loc3x3.IsCenterTile()){
            Debug.Log("its me! - no fieldtiles should be affected");
            return false;
        }
        Location2x2 affLoc = loc3x3.To2x2(myLoc);
/*         Debug.Log("loc3x3: "+loc3x3+" and get: "+loc3x3.Get());
        Debug.Log("myLoc: "+myLoc+" and get: "+myLoc.Get());
        Debug.Log("affloc post trasform: "+affLoc);
        Debug.Log("will affect: "+((affLoc != null).ToString())+"    "+gameObject.name); */
        return affLoc != null;
    }
    public override string ToString(){
        return gameObject.name+" "+myLoc;
    }
}