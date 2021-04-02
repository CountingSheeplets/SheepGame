using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PersistentFactory : Singleton<PersistentFactory> {
    List<GameObject> unusedItems = new List<GameObject>();
    List<GameObject> usedItems = new List<GameObject>();

    public GameObject itemPrefab;

    public static GameObject GetOrCreateItem(Owner owner, Transform location) {
        GameObject newItem;
        if (Instance.unusedItems.Count == 0) {
            newItem = Instantiate(Instance.itemPrefab, Vector3.zero, Quaternion.identity, Instance.transform);
        } else {
            newItem = Instance.unusedItems[0];
            Instance.unusedItems.Remove(newItem);
            newItem.SetActive(true);
        }
        Instance.usedItems.Add(newItem);
        return newItem;
    }
    public static void HideObject(GameObject obj) {
        Instance.unusedItems.Add(obj);
        Instance.usedItems.Remove(obj);
        obj.SetActive(false);
    }
}