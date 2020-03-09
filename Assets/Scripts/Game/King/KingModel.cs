using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
public class KingModel : MonoBehaviour {
    public SpriteRenderer targetColoredSprite;
    SkeletonMecanim skMecanim;
    void Awake() {
        skMecanim = GetComponent<SkeletonMecanim>();
    }
    /*     public void ChangeColor(Color color) {
            targetColoredSprite.color = color;
        } */
    public void ChangeColor(int playerIndex) {
        if (playerIndex > 0 && playerIndex < 9) {
            if (skMecanim != null) {
                skMecanim.skeleton.SetSkin("Player" + playerIndex.ToString());
                //Debug.Log("setting king color to: " + playerIndex.ToString());
            }
        }
    }
    public void ChangeScepter(int scepterIndex) {
        if (skMecanim != null)
            skMecanim.skeleton.SetAttachment("Staff", KingItemBucket.GetItem(scepterIndex, KingItemType.scepter).spriteName);
    }
    public void ChangeHat(int hatIndex) {
        if (skMecanim != null)
            skMecanim.skeleton.SetAttachment("Crown", KingItemBucket.GetItem(hatIndex, KingItemType.hat).spriteName);
    }
}