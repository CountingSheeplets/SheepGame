using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class KingModel : MonoBehaviour
{
    public SpriteRenderer targetColoredSprite;
    SkeletonMecanim skMecanim;
    void Awake(){
        skMecanim = GetComponent<SkeletonMecanim>();
    }
    public void ChangeColor(Color color){
        targetColoredSprite.color = color;
    }
    public void ChangeColor(int playerIndex){
        if(playerIndex > 0 && playerIndex < 9){
            if(skMecanim != null)
                skMecanim.skeleton.SetSkin("Player"+playerIndex.ToString());
        } else {
            ChangeColor(ConstantsBucket.PlayerColors[playerIndex]);
        }
    }
    public void ChangeScepter(int scepterIndex){
        if(skMecanim != null)
            skMecanim.skeleton.SetAttachment("scepter", KingItemBucket.GetItem(scepterIndex, KingItemType.scepter).spriteName);
    }
    public void ChangeHat(int hatIndex){
        if(skMecanim != null)
            skMecanim.skeleton.SetAttachment("hat", KingItemBucket.GetItem(hatIndex, KingItemType.hat).spriteName);
    }
}
