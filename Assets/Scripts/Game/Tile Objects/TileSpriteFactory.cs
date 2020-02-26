using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Array2DEditor;
public class TileSpriteFactory : Singleton<TileSpriteFactory>
{
    [SerializeField]
    private List<SpriteForLayout> layouts = new List<SpriteForLayout>();
    [SerializeField]
    private List<Sprite> mudSprites = new List<Sprite>();
    public static Sprite GetSprite(SubFieldTile subFieldTile){
        List<Sprite> eligibleSprites = new List<Sprite>();
        if(subFieldTile.GetParentType() != FieldTileSpriteType.mud){
            foreach(SpriteForLayout item in Instance.layouts){
                if(subFieldTile.ExportState().IsInListByMatching(item.layouts.Select(x => x.GetCells()).ToList())){
                //if(inputState.IsEqualByState(item.layout.GetCells())){
                    eligibleSprites.AddRange(item.sprites);
                }
            }
        } else {
            eligibleSprites.AddRange(Instance.mudSprites);
        }
        int randIndex = Random.Range(0, eligibleSprites.Count);
        Debug.Log("eligibleSprites[randIndex]:"+eligibleSprites[randIndex]);
        return eligibleSprites[randIndex];
    }

}
