using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
public class OwnerTileController : MonoBehaviour {
    public Transform ownerPanelContainer;
    public GameObject ownerTilePrefab;

    void Awake() {
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
    }
    void OnPlayerJoined(GameMessage msg) {
        GameObject go = Instantiate(ownerTilePrefab, ownerPanelContainer);
        go.name = msg.owner.ownerName;
        go.GetComponent<PlayerOwnerTile>().SetOwner(msg.owner);
        go.GetComponentInChildren<TextMeshProUGUI>().text = msg.owner.ownerName;
        KingFactory.TryCreateHeroModel(msg.owner, go.transform);
    }
    void OnPlayerLeft(GameMessage msg) {
        PlayerOwnerTile tile = ownerPanelContainer.GetComponentsInChildren<PlayerOwnerTile>().Where(x => x.myOwner == msg.owner).FirstOrDefault();
        Destroy(tile);
    }
}