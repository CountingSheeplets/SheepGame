using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SheepModel : MonoBehaviour {
    SkeletonMecanim skMecanim;
    int currentUpgade;
    void Awake() {
        skMecanim = GetComponent<SkeletonMecanim>();
        DisableAllAttachments();
    }

    public void ChangeColor(int playerIndex) {
        if (playerIndex > 0 && playerIndex < 9) {
            if (skMecanim != null) {
                skMecanim.skeleton.SetSkin("Player" + playerIndex.ToString());
            }
        }
    }
    public void SetUpgrade(int upgradeIndex) {
        //disable all upgrades slots

        //enable set upgrade slot
        if (skMecanim != null)
            skMecanim.skeleton.SetAttachment("Staff", KingItemBucket.GetItem(upgradeIndex, KingItemType.scepter).spriteName);
        currentUpgade = upgradeIndex;
    }

    void DisableAllAttachments() {

    }
}