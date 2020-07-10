using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingVulnerableHitExplode : MonoBehaviour {
    public GameObject fxPrefab;
    void Start() {
        EventCoordinator.StartListening(EventName.System.King.Killed(), OnKilled);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.Killed(), OnKilled);
    }
    void OnKilled(GameMessage msg) {
        GameObject newFx = Instantiate(fxPrefab, msg.kingUnit.transform.position, Quaternion.identity, transform);
    }
}