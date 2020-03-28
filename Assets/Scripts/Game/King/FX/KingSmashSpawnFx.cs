using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSmashSpawnFx : MonoBehaviour {
    public GameObject smashFxPrefab;
    void Start() {
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnSmashed);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnSmashed);
    }
    void OnSmashed(GameMessage msg) {
        if (GetComponentInParent<KingUnit>() != msg.kingUnit)
            return;
        GameObject newFx = Instantiate(smashFxPrefab, transform);
        newFx.transform.localPosition = Vector3.zero;
        newFx.transform.SetParent(msg.kingUnit.myPlayfield.transform);
        Destroy(newFx, 2f);
    }
}