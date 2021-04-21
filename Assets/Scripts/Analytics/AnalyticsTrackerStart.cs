using System.Collections.Generic;
public class AnalyticsTrackerStart : BaseAnalyticsTracker {
    void Awake() {
        eventName = EventName.Input.StartGame();
    }

    public override void OnGameEvent(GameMessage msg) {
        List<string> items = new List<string>();
        foreach (Owner owner in OwnersCoordinator.GetOwners()) {
            int id = owner.GetPlayerProfile().GetSeenItem(KingItemType.hat);
            items.Add(KingItemBucket.GetItem(id, KingItemType.hat).spriteName);
            id = owner.GetPlayerProfile().GetSeenItem(KingItemType.scepter);
            items.Add(KingItemBucket.GetItem(id, KingItemType.scepter).spriteName);
        }
        foreach (string i in items) {
            if (parameters.ContainsKey(i.ToLower()))
                parameters[i.ToLower()] = (int)(parameters[i.ToLower()]) + 1;
            else
                parameters.Add(i.ToLower(), 1);
        }
        Dispatch("items");
    }

}