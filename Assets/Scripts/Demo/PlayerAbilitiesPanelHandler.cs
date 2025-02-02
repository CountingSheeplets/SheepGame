using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesPanelHandler : MonoBehaviour
{
    public GameObject playerAbilitiesCardPrefab;
    public GameObject playerAbilitiesPanel;
    public List<GameObject> panels = new List<GameObject>();

    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.Network.PlayerJoined(), OnJoined);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerLeft(), OnLeft);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StartListening(EventName.System.Environment.EndMatch(), OnEndMatch);
    }
    private void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.Network.PlayerJoined(), OnJoined);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerLeft(), OnLeft);
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
        EventCoordinator.StopListening(EventName.System.Environment.EndMatch(), OnEndMatch);
    }

    void OnJoined(GameMessage msg)
    {
        GameObject newPanel = Instantiate(playerAbilitiesCardPrefab, playerAbilitiesPanel.transform);
        panels.Add(newPanel);
        newPanel.GetComponent<AbilitiesPanelTile>().SetOwner(msg.owner);
        Debug.Log("created the panel: "+newPanel);
    }

    void OnLeft(GameMessage msg) { 
        for (int i = panels.Count - 1; i >= 0; i--) {
            if (panels[i].GetComponent<AbilitiesPanelTile>().owner == msg.owner) {
                Destroy(panels[i]);
                panels.RemoveAt(i);
            }
        }
    }

    void OnStartGame(GameMessage msg) {
        playerAbilitiesPanel.SetActive(true);
        Debug.Log("started the game!");
    }
    void OnEndMatch(GameMessage msg) {
        playerAbilitiesPanel.SetActive(false);
    }
}
