using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : MonoBehaviour
{
    public Color activeCol;
    public Color passiveCol;
    public SpriteRenderer mySprite;
    public void SetState(bool state){
        if(state){
            mySprite.color = activeCol;
        } else {
            mySprite.color = passiveCol;
        }
    }
}
