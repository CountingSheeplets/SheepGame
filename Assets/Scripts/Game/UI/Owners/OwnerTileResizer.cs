using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OwnerTileResizer : MonoBehaviour {
    public float _height;
    GridLayoutGroup layout;
    void Awake() {
        layout = GetComponent<GridLayoutGroup>();
        Resize(GameMessage.Write());
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), Resize);
        EventCoordinator.StartListening(EventName.Input.ChangeKingItem(), Resize);
    }
    void OnApplicationFocus(bool hasFocus) {
        Resize(GameMessage.Write());
    }
    void Resize(GameMessage msg) {
        float height = this.gameObject.GetComponent<RectTransform>().rect.height;
        if (height == _height)
            return;
        _height = height;
        Debug.Log("Resizing...:height:" + height);
        layout.spacing = new Vector2(height / 50, height / 50);
        Vector2 newSize = new Vector2(height / 2.7f - layout.spacing.x, height / 3.3f - layout.spacing.y);
        layout.cellSize = newSize;
        foreach (KingModel m in GetComponentsInChildren<KingModel>()) {
            m.transform.localPosition = Vector3.zero;
            m.transform.localScale = m.GetComponent<KingModel>().playerTileScale;
        }
    }
}