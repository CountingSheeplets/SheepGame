using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubFieldTile : MonoBehaviour {
    public Location2x2 myLoc;
    //SpriteRenderer mySprite;
    MasterTile masterTile;
    //public SpriteMask myMask;
    bool isDirty = true;
    FieldTileSpriteType myParentStateType;
    public FieldTileSpriteType GetParentType() {
        return myParentStateType;
    }
    public void SetDirty() {
        //Debug.Log("settung dirty");
        isDirty = true;
    }

    TileSpriteState tileState;
    public TileSpriteState ExportState() {
        TileSpriteState exportState = new TileSpriteState(tileState);
        exportState.SetSelfToState(myParentStateType);
        return exportState;
    }
    public TileSpriteState GetState() {
        return tileState;
    }
    public FieldTileSpriteType GetSubStateType(Location2x2 loc2x2) {
        return tileState.GetSubStateType(loc2x2);
    }
    public TileSpriteState SetState(TileSpriteState inputState) {
        tileState.SetState(inputState);
        SetDirty();
        return tileState;
    }
    public TileSpriteState SetSubState(Location2x2 loc2x2, FieldTileSpriteType newState) {
        if (GetSubStateType(loc2x2) != newState) {
            tileState.SetSubState(loc2x2, newState);
            SetDirty();
        }
        return tileState;
    }
    void OnParentStateChanged(FieldTile tile, FieldTileSpriteType newState) {
        if (myParentStateType != newState)
            SetDirty();
        myParentStateType = newState;

    }
    void Awake() {
        tileState = new TileSpriteState(myLoc.Inverted());
        masterTile = GetComponentInParent<MasterTile>();
        //Debug.Log("mySprite:" + mySprite.gameObject.name);
        GetComponentInParent<FieldTile>().onListenStateChange += OnParentStateChanged;
    }
    void OnDestroy() {
        if (GetComponentInParent<FieldTile>())
            GetComponentInParent<FieldTile>().onListenStateChange -= OnParentStateChanged;
    }
    void Update() {
        if (isDirty) {
            SpriteSet();
            isDirty = false;
        }
    }

    void SpriteSet() {
        //mySprite.sprite = TileSpriteFactory.GetSprite(this);
        //setPixels of the sprite received to where they must go here:
        Color[] pixels = TileSpriteFactory.GetSprite(this).texture.GetPixels();
        int x = myLoc.x + Mathf.FloorToInt(transform.parent.position.x);
        int y = myLoc.y + Mathf.FloorToInt(transform.parent.position.y);
        masterTile.UpdateTexture(x, y, pixels);
    }

    public bool IsAffected(Location3x3 loc3x3) {
        if (loc3x3.IsCenterTile()) {
            return false;
        }
        Location2x2 affLoc = loc3x3.To2x2(myLoc);
        return affLoc != null;
    }
    public override string ToString() {
        return gameObject.name + " " + myLoc;
    }
}