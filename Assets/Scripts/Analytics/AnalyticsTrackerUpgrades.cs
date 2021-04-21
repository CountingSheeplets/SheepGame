using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class AnalyticsTrackerUpgrades : BaseAnalyticsTracker {
    Dictionary<string, int> upgrades = new Dictionary<string, int>();
    void Awake() {
        eventName = EventName.UI.ShowScoreScreen();
    }

    public override void OnGameEvent(GameMessage msg) {
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            List<SheepType> shTypes = SheepCoordinator.Instance.sheepStacks[owner].sheepSlots.Select(x => x.slotType).ToList();
            foreach (SheepType st in shTypes)
                AddSheepType(st);
        }
        foreach (KeyValuePair<string, int> pair in upgrades) {
            parameters.Add(pair.Key, pair.Value);
        }
        Dispatch("upgrades");
    }

    void AddSheepType(SheepType sheep) {
        string sName = sheep.ToString().ToLower();
        if (!upgrades.ContainsKey(sName))
            upgrades.Add(sName, 1);
        else
            upgrades[sName] += 1;
    }
}