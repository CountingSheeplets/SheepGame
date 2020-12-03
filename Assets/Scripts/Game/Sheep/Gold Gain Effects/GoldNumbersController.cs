using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldNumbersController : MonoBehaviour {
    public GameObject numberPrefab;
    Vector2 canvasWidthHeight;

    void Start() {
        EventCoordinator.StartListening(EventName.System.Economy.GoldChanged(), OnGoldChanged);
        RectTransform parentCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        canvasWidthHeight = new Vector2(parentCanvas.rect.width, parentCanvas.rect.height);
    }

    void OnGoldChanged(GameMessage msg) {
        if (msg.deltaFloat == 0 || msg.transform == null)return;
        GameObject newGoldObj = Instantiate(numberPrefab, Vector3.zero, Quaternion.identity, transform);
        SetPosition(msg.transform, newGoldObj.GetComponent<RectTransform>());
        newGoldObj.GetComponent<PlusGold>().Setup(msg.owner.GetPlayerProfile(), msg.deltaFloat);
    }

    public void SetPosition(Transform tr, RectTransform rect) {
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(tr.position);
        //rect.anchoredPosition = viewportPoint;
        //Debug.Log(viewportPoint + "   " + canvasWidthHeight);
        rect.anchoredPosition = viewportPoint * canvasWidthHeight - canvasWidthHeight / 2f;
    }
}