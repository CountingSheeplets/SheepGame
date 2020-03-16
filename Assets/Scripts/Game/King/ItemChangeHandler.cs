using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChangeHandler : MonoBehaviour {
    //public GameObject currentItemGO;
    KingItemType itemType;

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
        itemType = msg.kingItemType;
        if (GetComponentInParent<Owner>().EqualsByValue(msg.owner)) {
            currentItemIndex = msg.owner.GetPlayerProfile().GetSelectedItem(itemType);
            if (msg.intMessage > 0) {
                currentItemIndex++;
                if (currentItemIndex >= KingItemBucket.ItemCount(itemType))
                    currentItemIndex = 0;
            }
            if (msg.intMessage < 0) {
                currentItemIndex--;
                if (currentItemIndex < 0)
                    currentItemIndex = KingItemBucket.ItemCount(itemType) - 1;
            }
            msg.owner.GetPlayerProfile().SelectItem(itemType, currentItemIndex);
            //change visuals only if requirements are met:
            if (KingItemBucket.IsItemAvailable(msg.owner, currentItemIndex, itemType)) {
                ChangeItemTo(currentItemIndex);
            } else {
                Debug.Log("requirements not met!: " + currentItemIndex);
            }
        }
    }
    void OnSetKingItem(GameMessage msg) {
        itemType = msg.kingItemType;
        if (GetComponentInParent<Owner>().EqualsByValue(msg.owner)) {
            currentItemIndex = Mathf.Clamp(msg.intMessage, 0, KingItemBucket.ItemCount(itemType) - 1);
            msg.owner.GetPlayerProfile().SelectItem(itemType, currentItemIndex);
            //change visuals only if requirements are met:
            if (KingItemBucket.IsItemAvailable(msg.owner, msg.owner.GetPlayerProfile().selectedHat, itemType)) {
                ChangeItemTo(currentItemIndex);
            } else {
                Debug.Log("requirements not met!: " + msg.owner.GetPlayerProfile().selectedHat);
            }
        }
    }
    void ChangeItemTo(int index) {
        if (itemType == KingItemType.hat)
            GetComponent<KingModel>().SetHat(index);
        if (itemType == KingItemType.scepter)
            GetComponent<KingModel>().SetScepter(index);
    }
}