using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChangeHandler : MonoBehaviour {
    /*     [StringInList(typeof(PropertyDrawersHelper), "AllEventNames")]
        public string changeItemEventName; */

    int currentItemIndex = 0; //0 = default
    void Start() {
        EventCoordinator.StartListening(EventName.Input.ChangeKingItem(), OnChangeItem);
        EventCoordinator.StartListening(EventName.Input.SetKingItem(), OnSetKingItem);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.ChangeKingItem(), OnChangeItem);
        EventCoordinator.StopListening(EventName.Input.SetKingItem(), OnSetKingItem);
    }

    void OnChangeItem(GameMessage msg) {
        if (GetComponentInParent<PlayerOwnerTile>().myOwner.EqualsByValue(msg.owner)) {
            currentItemIndex = msg.owner.GetPlayerProfile().GetSeenItem(msg.kingItemType);
            if (msg.intMessage > 0) {
                currentItemIndex++;
                if (currentItemIndex >= KingItemBucket.ItemCount(msg.kingItemType))
                    currentItemIndex = 0;
            }
            if (msg.intMessage < 0) {
                currentItemIndex--;
                if (currentItemIndex < 0)
                    currentItemIndex = KingItemBucket.ItemCount(msg.kingItemType) - 1;
            }
            msg.owner.GetPlayerProfile().SeeItem(msg.kingItemType, currentItemIndex);
            //change the item only if requirements are met:
            if (KingItemBucket.IsItemAvailable(msg.owner, currentItemIndex, msg.kingItemType)) {
                msg.owner.GetPlayerProfile().SelectItem(msg.kingItemType, currentItemIndex);
                ChangeItemTo(currentItemIndex, msg.kingItemType);
            } else {
                Debug.Log("requirements not met!: " + currentItemIndex);
            }
        }
    }
    void OnSetKingItem(GameMessage msg) {
        if (GetComponentInParent<PlayerOwnerTile>().myOwner.EqualsByValue(msg.owner)) {
            currentItemIndex = Mathf.Clamp(msg.intMessage, 0, KingItemBucket.ItemCount(msg.kingItemType) - 1);
            //change visuals only if requirements are met:
            msg.owner.GetPlayerProfile().SeeItem(msg.kingItemType, currentItemIndex);
            if (KingItemBucket.IsItemAvailable(msg.owner, currentItemIndex, msg.kingItemType)) {
                msg.owner.GetPlayerProfile().SelectItem(msg.kingItemType, currentItemIndex);
                ChangeItemTo(currentItemIndex, msg.kingItemType);
            } else {
                Debug.Log("requirements not met!: " + currentItemIndex);
            }
        }
    }
    void ChangeItemTo(int index, KingItemType itemType) {
        if (itemType == KingItemType.hat)
            GetComponent<KingModel>().SetHat(index);
        if (itemType == KingItemType.scepter) {
            Debug.Log("changing scepter to i:" + index);
            GetComponent<KingModel>().SetScepter(index);
        }
    }
}