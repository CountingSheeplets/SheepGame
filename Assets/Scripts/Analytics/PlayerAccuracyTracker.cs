using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccuracyTracker : Singleton<PlayerAccuracyTracker> {
    Dictionary<Owner, int> hit = new Dictionary<Owner, int>();
    Dictionary<Owner, int> miss = new Dictionary<Owner, int>();
    public static float GetAccuracy(Owner owner) {
        if (!Instance.hit.ContainsKey(owner))
            Instance.hit.Add(owner, 0);
        if (!Instance.miss.ContainsKey(owner))
            Instance.miss.Add(owner, 1);

        float acc = (float)Instance.hit[owner] / ((float)Instance.miss[owner] + (float)Instance.hit[owner]);
        return Mathf.Round(acc * 100f) / 100f;
    }
    void Start() {
        EventCoordinator.StartListening(EventName.System.King.Hit(), OnHit);
        EventCoordinator.StartListening(EventName.System.Sheep.KingMissed(), OnMiss);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.Hit(), OnHit);
        EventCoordinator.StopListening(EventName.System.Sheep.KingMissed(), OnMiss);
    }
    void OnHit(GameMessage msg) {
        if (Instance.hit.ContainsKey(msg.owner))
            Instance.hit[msg.owner]++;
        else
            Instance.hit.Add(msg.owner, 1);
    }
    void OnMiss(GameMessage msg) {
        if (Instance.miss.ContainsKey(msg.owner))
            Instance.miss[msg.owner]++;
        else
            Instance.miss.Add(msg.owner, 1);
    }
}