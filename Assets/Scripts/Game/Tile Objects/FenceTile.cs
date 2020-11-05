using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceTile : MonoBehaviour {
    public GameObject spritePrefab;
    public bool isHorizontal = false;
    Vector2 locPos;
    //List<GameObject> sprites;
    /*     public void FillWithTiles(){
            float subdiv = ConstantsBucket.FenceFieldSubdivision;
            float size = ConstantsBucket.PlayfieldTileSize;
            for (int i = 0; i < subdiv; i++){
                GameObject newSprite = Instantiate(spritePrefab, transform);
                if(isHorizontal)
                    locPos = new Vector2 ((-(subdiv-1)/2f + i) * size, 0);
                else
                    locPos = new Vector2 (0, (-(subdiv-1)/2f + i) * size);
                newSprite.transform.localPosition = locPos;
            }
        } */
}