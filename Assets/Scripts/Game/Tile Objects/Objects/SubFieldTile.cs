using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubFieldTile : MonoBehaviour {
    public Location2x2 myLoc;
    public SpriteRenderer mySprite;
    //public SpriteMask myMask;
    bool isDirty = true;
    FieldTileSpriteType myParentStateType;
    Material myMaterial;
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
        Sprite spr = TileSpriteFactory.GetSprite(this);
        //mySprite.sprite = spr;
        // get coordinates of sprite and set it onto SpriteRenderer
        //myMask.sprite = mySprite.sprite;
        //string outp = " ";
        //foreach (Vector2 uv in spr.uv)
        //    outp += uv + " ";
        //Debug.Log(transform.parent.name + "  -outp uvs-" + spr.rect);
        Vector2 offset = new Vector2(spr.rect.x / 896, spr.rect.y / 896);
        myMaterial.SetVector("_GrassOffset", offset);
    }
    public void SetupSubTile(float x, float y) {
        float ts = ConstantsBucket.PlayfieldTileSize;
        float gs = ConstantsBucket.GridSize;
        float tilingDif = 1f / (ConstantsBucket.GridSize);
        Vector2 tiling = new Vector2(tilingDif / 2f, tilingDif / 2f);
        myMaterial = GetComponent<SpriteRenderer>().material;
        myMaterial.SetVector("_CrackTiling", tiling);
        float subX = 0f;
        float subY = 0f;
        float dif = (gs - 1f) / 2f;
        if (transform.localPosition.x > 0)
            subX = tilingDif / 2f;
        if (transform.localPosition.y > 0)
            subY = tilingDif / 2f;
        float tileX = x / ts + dif;
        float tileY = y / ts + dif;
        //this is position at the moment, which is wrong
        float div = gs * ts;
        //must be <1, so divide it by max and calculate off scale, because position is with minus
        //then also add another push because of subtile offsset under Tile obj(+ -0.5)
        Vector2 offset = new Vector2((tileX) / gs + subX, (tileY) / gs + subY);
        myMaterial.SetVector("_CrackOffset", offset);

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