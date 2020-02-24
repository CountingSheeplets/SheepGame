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

    public static bool[,] GetAffectedSprites(Vector2 location){ //input 3x3 loc
        bool[,] output = new bool[2,2];
        int x = (int)location.x;
        int y = (int)location.y;
        bool[] i = new bool[2];
        bool[] j = new bool[2];
        switch(x){
            case 0:
                i[0] = true;
                break;
            case 1:
                i[0] = true;
                i[1] = true;
                break;
            case 2:
                i[1] = true;
                break;
        }
        switch(y){
            case 0:
                j[0] = true;
                break;
            case 1:
                j[0] = true;
                j[1] = true;
                break;
            case 2:
                j[1] = true;
                break;
        }
        for(int k = 0; k < 2; k++){
            for(int l = 0; l < 2; l++){
                output[k,l] = i[k] & j[l];
            }
        }
        return output;
    }
}
