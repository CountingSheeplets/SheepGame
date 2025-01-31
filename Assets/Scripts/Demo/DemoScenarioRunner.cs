using System;
using UnityEngine;
using System.Collections.Generic;

public class DemoScenarioRunner : MonoBehaviour {
    private List<Action> commandBuffer = new List<Action>();
    int commandIndex = 0;
    public bool debug = false;
    float timer = 0.15f;
    float step = 0.25f;

    void Start() {
        AddCommand(() => AddPlayer());
        AddCommand(() => AddPlayer());
        AddCommand(() => StartGame());
    }

    void Update() {
        if(!debug) {
            timer += Time.deltaTime;
            if(timer > step) {
                timer = 0;
                ExecuteCommand(commandIndex);
                commandIndex += 1;
            }
        } else
            if (Input.GetKeyDown(KeyCode.Space)) {
                ExecuteCommand(commandIndex);
                commandIndex += 1;
            }
    }

    void AddPlayer() {
        Owner owner = OwnersCoordinator.CreateEmptyOwner();
        if (GameStateView.HasState(GameState.started)) {
            Debug.LogWarning("New cannot join, game already started");
            return;
        }
        if (owner) {
            EventCoordinator.TriggerEvent(EventName.Input.Network.PlayerJoined(), GameMessage.Write().WithOwner(owner));
        }
    }

    void StartGame() {
        if (!GameStateView.HasState(GameState.started))
            EventCoordinator.TriggerEvent(EventName.Input.StartGame(), GameMessage.Write());
    }

    public void AddCommand(Action command) {
        commandBuffer.Add(command);
    }

    private void ExecuteCommand(int commandIndex) {
        if(commandIndex >= commandBuffer.Count) {
            Debug.Log("Finished Bubble Scenario Execution, exiting player.");
            this.enabled = false;
            return;
        }
        commandBuffer[commandIndex].Invoke();
    }

    private void OnDestroy() {
        commandBuffer.Clear();
    }
}
