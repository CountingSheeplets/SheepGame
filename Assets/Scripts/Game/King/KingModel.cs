using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
public class KingModel : MonoBehaviour {
    int _hatIndex;
    public int HatIndex { get { return _hatIndex; } }
    int _scepterIndex;
    public int ScepterIndex { get { return _scepterIndex; } }
    SkeletonMecanim skMecanim;
    void Awake() {
        skMecanim = GetComponent<SkeletonMecanim>();
    }
    public void ChangeColor(int playerIndex) {
        if (playerIndex > 0 && playerIndex < 9) {
            if (skMecanim != null) {
                skMecanim.skeleton.SetSkin("Player" + playerIndex.ToString());
                //Debug.Log("setting king color to: " + playerIndex.ToString());
            }
        }
    }
    public void SetScepter(int scepterIndex) {
        _scepterIndex = scepterIndex;
        if (skMecanim != null)
            skMecanim.skeleton.SetAttachment("Staff", KingItemBucket.GetItem(scepterIndex, KingItemType.scepter).spriteName);
    }
    public void SetHat(int hatIndex) {
        _hatIndex = hatIndex;
        if (skMecanim != null)
            skMecanim.skeleton.SetAttachment("Crown", KingItemBucket.GetItem(hatIndex, KingItemType.hat).spriteName);
    }
}