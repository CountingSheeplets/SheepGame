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
                eligibleSprites.AddRange(item.sprites);
            }
        }
        return eligibleSprites[Random.Range(0, eligibleSprites.Count)];
    }

    public static bool[,] GetAffectedSprites(Vector2 location){
        bool[,] output = new bool[2,2];
        int x = (int)location.x;
        int y = (int)location.y;
        switch(x){
            case 0:
                output[0,0] = true;
                break;
            case 1:
                output[0,0] = true;
                output[0,1] = true;
                break;
            case 2:
                output[0,1] = true;
                break;
        }
        switch(y){
            case 0:
                output[1,0] = true;
                break;
            case 1:
                output[1,0] = true;
                output[1,1] = true;
                break;
            case 2:
                output[1,1] = true;
                break;
        }
        return output;
    }
}
