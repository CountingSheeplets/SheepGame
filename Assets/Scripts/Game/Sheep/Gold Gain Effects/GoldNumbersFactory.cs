using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldNumbersFactory : PersistentFactory {
    Vector2 canvasWidthHeight;

    private void Start() {
        RectTransform parentCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        canvasWidthHeight = new Vector2(parentCanvas.rect.width, parentCanvas.rect.height);
    }

    public static PlusGold GetOrCreateGoldNumber(Owner owner, Transform location, float value) {
        PlusGold newGoldPlus;
        GameObject goldObj = GetOrCreateItem(owner, location);
        newGoldPlus = goldObj.GetComponent<PlusGold>();
        newGoldPlus.animator.SetTrigger("reset");

        ((GoldNumbersFactory)Instance).SetPosition(location, newGoldPlus.GetComponent<RectTransform>());
        newGoldPlus.Setup(owner.GetPlayerProfile(), value);

        return newGoldPlus;
    }

    public void SetPosition(Transform tr, RectTransform rect) {
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(tr.position);
        rect.anchoredPosition = viewportPoint * canvasWidthHeight - canvasWidthHeight / 2f;
    }
}