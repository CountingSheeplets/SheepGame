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
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.ChangeKingItem(), OnChangeItem);
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
            PersistentDataCoordinator.StoreData(msg.owner);
            //change visuals only if requirements are met:
            ChangeItemTo(currentItemIndex);
        }
    }
    void ChangeItemTo(int index) {
        if (itemType == KingItemType.hat)
            GetComponent<KingModel>().ChangeHat(index);
        if (itemType == KingItemType.scepter)
            GetComponent<KingModel>().ChangeScepter(index);

        /*         GameObject newItem = Instantiate(KingItemBucket.GetItem(currentItemIndex, itemType).gameObject);
                newItem.transform.parent = transform;
                newItem.transform.localPosition = currentItemGO.transform.localPosition;
                newItem.transform.localScale = currentItemGO.transform.localScale;
                Destroy(currentItemGO);
                currentItemGO = newItem; */
    }
}