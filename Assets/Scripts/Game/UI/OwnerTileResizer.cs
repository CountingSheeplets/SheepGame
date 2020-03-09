using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OwnerTileResizer : MonoBehaviour {
    public float height;

    void Start() {
        Resize(GameMessage.Write());
    }

    void Resize(GameMessage msg) {
        GridLayoutGroup layout = GetComponent<GridLayoutGroup>();
        height = this.gameObject.GetComponent<RectTransform>().rect.height;
        Debug.Log("Resizing...:height:" + height);
        layout.spacing = new Vector2(height / 45, height / 45);
        Vector2 newSize = new Vector2(height / 2f - layout.spacing.x, height / 2.5f - layout.spacing.y);
        layout.cellSize = newSize;
    }
}