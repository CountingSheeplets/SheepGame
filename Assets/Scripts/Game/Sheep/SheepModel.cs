using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SheepModel : MonoBehaviour {
    SkeletonMecanim skMecanim;
    int currentUpgade;
    public SheepUnit sheep;
    Vector3 modelScale;
    //public Color color = Color.white;
    void Awake() {
        modelScale = transform.localScale;
        skMecanim = GetComponent<SkeletonMecanim>();
        DisableAllAttachments();
        EventCoordinator.StartListening(EventName.System.Sheep.Upgraded(), OnUpgrade);
    }

    public void ChangeColor(int playerIndex) {
        //sheep will be 'invisible' if teamId is wrong (0)
        sheep = GetComponentInParent<SheepUnit>();
        if (playerIndex > 0 && playerIndex < 9) {
            if (skMecanim != null) {
                skMecanim.skeleton.SetSkin("Player" + playerIndex.ToString());

                //if (ConstantsBucket.PlayerColors.Count > playerIndex)
                //    color = ConstantsBucket.PlayerColors[playerIndex - 1];
            }
        }
    }

    void OnUpgrade(GameMessage msg) {
        if (msg.sheepUnit != sheep)
            return;
        DisableAllAttachments();
        EnabeUpgradeSlot();
    }
    public void EnabeUpgradeSlot() {
        if (sheep == null)return;
        string slot = UpgradeBucket.GetAttachmentSlot(sheep.sheepType).Split('.')[0];
        string attachment = UpgradeBucket.GetAttachmentSlot(sheep.sheepType).Split('.')[1];
        if (slot == "Small") {
            transform.localScale = modelScale * ConstantsBucket.SmallUpgradeShrinkSize;
            return;
        } else {
            transform.localScale = modelScale;
        }
        if (skMecanim != null)
            if (slot != " ") {
                skMecanim.skeleton.SetAttachment(slot, attachment);
            }
    }
    public void DisableAllAttachments() {
        skMecanim.skeleton.SetAttachment("Divine", null);
        skMecanim.skeleton.SetAttachment("Crown", null);
        skMecanim.skeleton.SetAttachment("Shovel", null);
        skMecanim.skeleton.SetAttachment("Shield", null);
    }
}