using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;
public class TileSpriteFactory : Singleton<TileSpriteFactory>
{
    [SerializeField]
    private List<SpriteForLayout> layouts = new List<SpriteForLayout>();
    
    public static Sprite GetSprite(TileSpriteState inputState){
        List<Sprite> eligibleSprites = new List<Sprite>();
        foreach(SpriteForLayout item in Instance.layouts){
            FieldTileSpriteType[,] cells = item.layout.GetCells();
            if(inputState.IsEqualByState(cells)){
                eligibleSprites.Add(item.sprite);
            }
        }
        return eligibleSprites[Random.Range(0, eligibleSprites.Count)];
    }
}
