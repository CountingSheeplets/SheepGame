using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.GridLayoutGroup;

public class ControllerInputDemoLobby : MonoBehaviour
{
    private PlayerInputManager playerInputManager;

    private void Awake() {
        // Get the PlayerInputManager component
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void OnEnable() {
        // Subscribe to the player joined event
        playerInputManager.onPlayerJoined += OnPlayerJoined;
        playerInputManager.onPlayerLeft += OnPlayerLeft;
    }

    private void OnDisable() {
        // Unsubscribe to avoid memory leaks
        playerInputManager.onPlayerJoined -= OnPlayerJoined;
        playerInputManager.onPlayerLeft -= OnPlayerLeft;
    }

    private void OnPlayerJoined(PlayerInput playerInput) {
        //Debug.Log($"Player {playerInput.playerIndex} joined"); //with device: {playerInput.devices[0].name}");
        ControllerInputDemo demoInput = playerInput.GetComponent<ControllerInputDemo>();
        Owner newOwner = OwnersCoordinator.CreateEmptyOwner();
        demoInput.SetupOwner(newOwner);
        playerInput.transform.SetParent(transform);
        EventCoordinator.TriggerEvent(EventName.Input.Network.PlayerJoined(), GameMessage.Write().WithOwner(newOwner));
    }

    private void OnPlayerLeft(PlayerInput playerInput) {
        //Debug.Log($"Player {playerInput.playerIndex} left the game.");
        ControllerInputDemo demoInput = playerInput.GetComponent<ControllerInputDemo>();
        EventCoordinator.TriggerEvent(EventName.Input.Network.PlayerLeft(), GameMessage.Write().WithOwner(demoInput.playerOwner));
        OwnersCoordinator.DisconnectOwner(demoInput.playerOwner);
    }
}
