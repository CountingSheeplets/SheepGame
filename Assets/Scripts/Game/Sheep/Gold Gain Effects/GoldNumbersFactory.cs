using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldNumbersFactory : PersistentFactory {
    Vector2 canvasWidthHeight;
    private static GoldNumbersFactory _instance;
    public static GoldNumbersFactory Instance {
        get {

            if (_instance != null)
                return _instance;
            _instance = FindObjectOfType(typeof(GoldNumbersFactory))as GoldNumbersFactory;
            return _instance;
        }
    }

    private void Start() {
        RectTransform parentCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        canvasWidthHeight = new Vector2(parentCanvas.rect.width, parentCanvas.rect.height);
    }

    public static PlusGold GetOrCreateGoldNumber(Owner owner, Transform location, float value) {
        PlusGold newGoldPlus;
        GameObject goldObj = Instance.GetOrCreateItem();
        if (!goldObj.name.Contains("GoldPlus"))
            goldObj.name = "GoldPlus_" + goldObj.name;
        newGoldPlus = goldObj.GetComponent<PlusGold>();
        newGoldPlus.animator.SetTrigger("reset");

        Instance.SetPosition(location, newGoldPlus.GetComponent<RectTransform>());
        newGoldPlus.Setup(owner.GetPlayerProfile(), value);

        return newGoldPlus;
    }

    public void SetPosition(Transform tr, RectTransform rect) {
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(tr.position);
        rect.anchoredPosition = viewportPoint * canvasWidthHeight - canvasWidthHeight / 2f;
    }
    public static void DestroyGoldNumber(GameObject obj) {
        Instance.HideObject(obj);
    }
}