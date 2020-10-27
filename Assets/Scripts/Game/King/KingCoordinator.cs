using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KingCoordinator : Singleton<KingCoordinator> {
    public List<KingUnit> kings = new List<KingUnit>();
    public List<KingModel> kingModels = new List<KingModel>();
    void Start() {
        EventCoordinator.StartListening(EventName.System.King.Spawned(), OnSpawn);
        EventCoordinator.StartListening(EventName.System.King.Killed(), OnKill);
        EventCoordinator.StartListening(EventName.Input.Network.PlayerRecalculate(), OnPlayerRecalculate);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.Spawned(), OnSpawn);
        EventCoordinator.StopListening(EventName.System.King.Killed(), OnKill);
        EventCoordinator.StopListening(EventName.Input.Network.PlayerRecalculate(), OnPlayerRecalculate);
    }

    void OnSpawn(GameMessage msg) {
        kings.Add(msg.kingUnit);
    }
    void OnKill(GameMessage msg) {
        kings.Remove(msg.kingUnit);
    }
    void OnPlayerRecalculate(GameMessage msg) {
        kingModels = new List<KingModel>(FindObjectsOfType<KingModel>());
    }
    public static KingUnit GetKing(Playfield playfield) {
        foreach (KingUnit king in Instance.kings) {
            if (king.owner.GetPlayfield() == playfield)
                return king;
        }
        return null;
    }
    public static KingUnit GetKing(Owner owner) {
        foreach (KingUnit king in Instance.kings) {
            if (king.owner.EqualsByValue(owner))
                return king;
        }
        return null;
    }
    public static List<KingUnit> GetKings() {
        return Instance.kings;
    }
    public static KingModel GetSourceKingModel(Owner owner) {
        PlayerOwnerTile tile = FindObjectsOfType<PlayerOwnerTile>().Where(x => x.myOwner == owner).FirstOrDefault();
        KingModel model = tile.GetComponentsInChildren<KingModel>(true)[0];
        if (model != null) {
            return model;
        }
        return null;
    }
}