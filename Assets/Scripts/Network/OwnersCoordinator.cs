using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NDream.AirConsole;
using UnityEngine;

public class OwnersCoordinator : Singleton<OwnersCoordinator> {
    void Awake() {
        isIdUsed = new bool[ConstantsBucket.PlayerColors.Count];
    }
    public List<Owner> owners = new List<Owner>();
    int counter = 0;
    public static List<Owner> GetOwners() {
        return Instance.owners.Where(x => x.GetPlayerProfile() != null).ToList();
    }
    public static List<Owner> GetOwnersAll() {
        return Instance.owners; //when getting this, it shoulnt be able to change (but only wont change if instance into a new List),else the original will also change...
    }
    public bool[] isIdUsed;
    public static int GetNewTeamId() {
        for (int i = 0; i < ConstantsBucket.PlayerColors.Count; i++) {
            if (Instance.isIdUsed[i] == false) {
                Instance.isIdUsed[i] = true;
                return i + 1;
            }
        }
        return 0;
    }
    public static void UnUseTeamId(int id) {
        if (id > 0)
            Instance.isIdUsed[id - 1] = false;
    }
    public static void ClearTeamIds() {
        Instance.isIdUsed = new bool[ConstantsBucket.PlayerColors.Count];
    }
    public static List<Owner> GetOwnersAlive() {
        return Instance.owners.Where(x => x.GetPlayerProfile() != null).Where(x => x.GetPlayerProfile().isAlive).ToList();
    }
    public static Owner TryCreateOwner(int device_id) {
        GameObject go = new GameObject();
        go.transform.parent = Instance.transform;
        string nicknameOfJoined = AirConsole.instance.GetNickname(device_id);
        Owner newOwner = go.AddComponent<Owner>();
        newOwner.Create(AirConsole.instance.GetUID(device_id), nicknameOfJoined, device_id);
        foreach (Owner owner in GetOwnersAll()) {
            if (owner.deviceId == newOwner.deviceId) {
                Destroy(go);
                return owner;
            }
        }
        Instance.owners.Add(newOwner);
        newOwner.teamId = GetNewTeamId();
        go.name = newOwner.ownerName;
        //go.GetComponentInChildren<TextMeshProUGUI>().text = newOwner.ownerName;
        return newOwner;
    }
    public static Owner ReconnectOwner(int device_id) {
        Owner recOwner = GetOwner(AirConsole.instance.GetUID(device_id));
        if (recOwner == null)return null;
        if (recOwner.GetPlayerProfile() == null)return null;
        Debug.Log(recOwner);
        Debug.Log(recOwner.ownerId + "   GetUID: " + AirConsole.instance.GetUID(device_id));
        if (recOwner != null) {
            GetOwner(AirConsole.instance.GetUID(device_id)).deviceId = device_id;
            OwnersCoordinator.GetOwner(device_id).connected = true;
            Debug.Log("Reconnect succesfull!");
            if (GameStateView.HasState(GameState.started)) {
                if (OwnersCoordinator.GetOwner(device_id).GetPlayerProfile().isAlive) {
                    NetworkCoordinator.SendShowView(device_id, "match");
                    NetworkCoordinator.SendUpgradeData();
                }

                if (GameStateView.HasState(GameState.ended)) {
                    PlayerScores playerScores = ScoreCoordinator.GetPlayerScores(recOwner);
                    int total = ScoreCoordinator.GetTotalPlayerScores(recOwner);
                    NetworkCoordinator.SendPlayerScores(recOwner.deviceId, OwnersCoordinator.GetOwner(device_id).GetPlayerProfile().isAlive, playerScores.scores, total);
                    NetworkCoordinator.SendShowView(device_id, "post");
                }
            } else {
                NetworkCoordinator.SendShowView(device_id, "menu");
            }
            return OwnersCoordinator.GetOwner(device_id);
        }
        return null;
    }
    public static Owner GetOwner(int device_id) {
        return Instance.owners.Where(x => x.deviceId == device_id).FirstOrDefault();
    }
    public static Owner GetOwner(string uid) {
        return Instance.owners.Where(x => x.ownerId == uid).FirstOrDefault();
    }
    public static Owner GetRandomOwner() {
        if (Instance.owners.Count > 0) {
            List<Owner> aliveOwners = new List<Owner>(GetOwners().Where(x => x.GetPlayerProfile().isAlive && x.connected).ToList());
            return aliveOwners[Random.Range(0, aliveOwners.Count)];
        } else return null;
    }
    public static Owner CreateEmptyOwner() {
        Instance.counter++;
        int device_id = Instance.counter;
        //GameObject go = Instantiate(Instance.ownerTilePrefab, Instance.ownerPanelContainer);
        GameObject go = new GameObject();
        go.transform.parent = Instance.transform;
        string nicknameOfJoined = Generate.RandomString(10);
        Owner newOwner = go.AddComponent<Owner>();
        Instance.owners.Add(newOwner);
        newOwner.Create(device_id.ToString(), nicknameOfJoined, device_id);
        go.name = newOwner.ownerName;
        newOwner.teamId = Instance.counter;
        //go.GetComponentInChildren<TextMeshProUGUI>().text = newOwner.ownerName;
        return newOwner;
    }
    public static Owner DisconnectOwner(int device_id) {
        Owner leftOwner = GetOwner(device_id);
        if (leftOwner == null)
            return null;

        Debug.Log("DisconnectOwner GetGameState:" + (int)GameStateView.GetGameState());

        if (GameStateView.HasState(GameState.started) && leftOwner.GetPlayerProfile() != null) {
            leftOwner.connected = false;
        } else {
            UnUseTeamId(leftOwner.teamId);
            Instance.owners.Remove(leftOwner);
            Destroy(leftOwner.gameObject);
        }
        return leftOwner;
    }
    public static bool ContainsActiveFirstOwner() {
        foreach (Owner owner in GetOwners()) {
            if (owner.IsFirstOwner)
                return true;
        }
        return false;
    }
}