using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Array2DEditor;
public class TileSpriteFactory : Singleton<TileSpriteFactory>
{
    [SerializeField]
    private List<SpriteForLayout> layouts = new List<SpriteForLayout>();
    
    public static Sprite GetSprite(TileSpriteState inputState){
        List<Sprite> eligibleSprites = new List<Sprite>();
        foreach(SpriteForLayout item in Instance.layouts){
            if(inputState.IsInListByState(item.layouts.Select(x => x.GetCells()).ToList())){
            //if(inputState.IsEqualByState(item.layout.GetCells())){
                eligibleSprites.AddRange(item.sprites);
            }
        }
        int randIndex = Random.Range(0, eligibleSprites.Count);
        Debug.Log("eligibleSprites[randIndex]:"+eligibleSprites[randIndex]);
        return eligibleSprites[randIndex];
    }

}
