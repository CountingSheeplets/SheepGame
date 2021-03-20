using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldNumbersFactory : Singleton<GoldNumbersFactory> {
    List<PlusGold> unusedNumbers = new List<PlusGold>();
    List<PlusGold> usedNumbers = new List<PlusGold>();

    public GameObject numberPrefab;
    Vector2 canvasWidthHeight;

    private void Start() {
        RectTransform parentCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        canvasWidthHeight = new Vector2(parentCanvas.rect.width, parentCanvas.rect.height);
    }

    public static PlusGold GetOrCreateGoldNumber(Owner owner, Transform location, float value) {
        PlusGold newGoldPlus;
        if (Instance.unusedNumbers.Count == 0) {
            newGoldPlus = Instantiate(Instance.numberPrefab, Vector3.zero, Quaternion.identity, Instance.transform).GetComponent<PlusGold>();
        } else {
            newGoldPlus = Instance.unusedNumbers[0];
            Instance.unusedNumbers.Remove(newGoldPlus);
            newGoldPlus.animator.SetTrigger("reset");
            newGoldPlus.animator.gameObject.SetActive(true);
        }
        Instance.usedNumbers.Add(newGoldPlus);
        Instance.SetPosition(location, newGoldPlus.GetComponent<RectTransform>());
        newGoldPlus.Setup(owner.GetPlayerProfile(), value);
        return newGoldPlus;
    }
    public static void HideObject(PlusGold obj) {
        Instance.unusedNumbers.Add(obj);
        Instance.usedNumbers.Remove(obj);
        obj.animator.gameObject.SetActive(false);
    }
    public void SetPosition(Transform tr, RectTransform rect) {
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(tr.position);
        rect.anchoredPosition = viewportPoint * canvasWidthHeight - canvasWidthHeight / 2f;
    }
}