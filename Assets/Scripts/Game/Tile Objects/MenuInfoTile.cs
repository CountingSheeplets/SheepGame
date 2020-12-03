using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
public class MenuInfoTile : MonoBehaviour {
    public string needMoreText = "";
    public string upTo8Text = "";
    public TextMeshProUGUI textComponent;

    void Awake() {
        EventCoordinator.StartListening(EventName.Input.Network.PlayerRecalculate(), OnChange);
        textComponent.text = needMoreText;
    }
    private void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.Network.PlayerRecalculate(), OnChange);
    }
    void OnChange(GameMessage msg) {
        if (GameStateView.HasState(GameState.started))
            return;
        int count = OwnersCoordinator.GetOwners().Where(x => x.connected).ToList().Count;
        if (count <= 1) {
            textComponent.text = needMoreText;
            gameObject.SetActive(true);
        } else {
            if (count < 8) {
                textComponent.text = upTo8Text;
                gameObject.SetActive(true);
            } else {
                gameObject.SetActive(false);
            }
        }
        transform.SetAsLastSibling();
        //Debug.Log("last sibling");
    }
}