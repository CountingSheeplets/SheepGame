using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawnCapCoordinator : Singleton<SheepSpawnCapCoordinator> {

    Dictionary<Owner, int> caps = new Dictionary<Owner, int>();

    public static bool IsCapped(Owner owner) {
        if (Instance.caps.ContainsKey(owner)) {
            return (Instance.caps[owner] <= SheepCoordinator.GetSheeps(owner).Count);
        } else return true;
    }
    public static void RemoveOwner(Owner owner) {
        if (Instance.caps.ContainsKey(owner)) {
            Instance.caps.Remove(owner);
        }
        foreach (Owner key in Instance.caps.Keys) {
            Instance.caps[key] += ConstantsBucket.SheepSpawnCapIncrement;
        }
    }
    void Start() {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
    }

    void OnStartGame(GameMessage msg) {
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            Instance.caps.Add(owner, ConstantsBucket.SheepSpawnCapBase);
        }
    }
}