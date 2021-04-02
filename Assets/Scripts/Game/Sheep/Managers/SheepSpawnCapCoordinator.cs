using System.Collections.Generic;

public class SheepSpawnCapCoordinator : Singleton<SheepSpawnCapCoordinator> {

    Dictionary<Owner, int> caps = new Dictionary<Owner, int>();

    public static bool IsCapped(Owner owner) {
        if (Instance.caps.ContainsKey(owner)) {
            //Debug.Log("IsCaped: " + Instance.caps[owner] + " owned: " + SheepCoordinator.GetSheeps(owner).Count);
            return (Instance.caps[owner] <= SheepCoordinator.GetSheeps(owner).Count);
        } else return true;
    }
    public static void RemoveOwner(Owner owner) {
        if (Instance.caps.ContainsKey(owner)) {
            Instance.caps.Remove(owner);
        }
    }
    public static void IncreaseCaps(int delta) {
        foreach (Owner owner in OwnersCoordinator.GetOwnersAlive()) {
            if (Instance.caps.ContainsKey(owner)) {
                Instance.caps[owner] += delta;
            }
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