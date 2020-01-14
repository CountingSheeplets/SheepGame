using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChangeHandler : MonoBehaviour
{
    public GameObject currentItemGO;
    public KingItemType itemType;

    [StringInList(typeof(PropertyDrawersHelper), "AllEventNames")]
    public string changeItemEventName;

    int currentItemIndex = 0; //0 = default
    void Start()
    {
        //EventName.Input.ChangeHat
        //EventName.Input.ChangeScepter
        EventCoordinator.StartListening(changeItemEventName, OnChangeItem);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(changeItemEventName, OnChangeItem);
    }

    void OnChangeItem(GameMessage msg)
    {
        if(GetComponentInParent<Owner>().EqualsByValue(msg.owner)){
            currentItemIndex = msg.owner.GetPlayerProfile().GetSelectedItem(itemType);
            if(msg.intMessage > 0){
                currentItemIndex ++;
                if(currentItemIndex >= KingItemBucket.ItemCount(itemType))
                    currentItemIndex = 0;
            }
            if(msg.intMessage < 0){
                currentItemIndex --;
                if(currentItemIndex < 0)
                    currentItemIndex = KingItemBucket.ItemCount(itemType)-1;
            }
            ChangeItemTo(currentItemIndex);
            msg.owner.GetPlayerProfile().SelectItem(itemType, currentItemIndex);
			PersistentDataCoordinator.StoreData(msg.owner);
        }
    }
    void ChangeItemTo(int index){
        currentItemIndex = index;
        //Debug.Log("currentitemindex: "+index);
        //Debug.Log("item: "+KingItemBucket.GetItem(currentItemIndex, itemType));

        GameObject newItem = Instantiate(KingItemBucket.GetItem(currentItemIndex, itemType).gameObject);
        newItem.transform.parent = transform;
        newItem.transform.localPosition = currentItemGO.transform.localPosition;
        newItem.transform.localScale = currentItemGO.transform.localScale;
        Destroy(currentItemGO);
        currentItemGO = newItem;
    }
}
