using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
public class MenuInfoTile : MonoBehaviour {
    public TextMeshProUGUI textComponent;

    void Awake() {
        EventCoordinator.StartListening(EventName.Input.Network.PlayerRecalculate(), OnChange);
        textComponent.text = TranslationsHandler.GetInfoTileMoreTr();
    }
    private void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.Network.PlayerRecalculate(), OnChange);
    }
    void OnChange(GameMessage msg) {
        if (GameStateView.HasState(GameState.started))
            return;
        int count = OwnersCoordinator.GetOwners().Where(x => x.connected).ToList().Count;
        if (count <= 1) {
            textComponent.text = TranslationsHandler.GetInfoTileMoreTr();
            gameObject.SetActive(true);
        } else {
            if (count < 8) {
                textComponent.text = TranslationsHandler.GetInfoTileNeedTr();
                gameObject.SetActive(true);
            } else {
                gameObject.SetActive(false);
            }
        }
        transform.SetAsLastSibling();
    }
}