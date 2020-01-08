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
    }
}