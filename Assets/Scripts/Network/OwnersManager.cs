using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using System.Linq;

public class OwnersManager : Singleton<OwnersManager>
{
    List<Owner> owners = new List<Owner>();
    public GameObject ownerTilePrefab;
    public Transform ownerPanelContainer;
    public static List<Owner> GetOwners(){
        return Instance.owners;
    }
    void Start()
    {
        EventManager.StartListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnDestroy(){
        EventManager.StopListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnStartGame(GameMessage msg){
        foreach(Owner owner in owners)
            owner.gameObject.SetActive(false);
    }
    public static Owner TryCreateOwner(int device_id){
        Owner candidateOwner = Instance.owners.Where(x=>x.ownerId == AirConsole.instance.GetUID(device_id)).FirstOrDefault();
        if(candidateOwner == null){
            GameObject go = Instantiate(Instance.ownerTilePrefab, Instance.ownerPanelContainer);
            string nicknameOfJoined = AirConsole.instance.GetNickname (device_id);
            Owner newOwner = go.AddComponent<Owner>();
            Instance.owners.Add(newOwner);
            newOwner.Create(AirConsole.instance.GetUID(device_id), nicknameOfJoined, MenuNetworkManager.Instance.premiumIds.Contains(device_id), device_id);
            go.name = newOwner.ownerName;
            return newOwner;
        } else {
            candidateOwner.deviceId = device_id;
            candidateOwner.connected = true;
            Debug.Log("Reconnect succesfull!");
            var data = new Dictionary<string, string> { { "show_view_id", "view-0" } };
            AirConsole.instance.Message(device_id, data);
            return candidateOwner;
        }
    }
    public static Owner GetOwner(int device_id){
        return Instance.owners.Where(x=>x.deviceId == device_id).FirstOrDefault();
    }

    public static Owner DisconnectOwner(int device_id){
        Owner leftOwner = GetOwner(device_id);
        if(leftOwner == null)
            return null;
        EventManager.TriggerEvent(EventName.Input.Network.PlayerLeft(), GameMessage.Write().WithOwner(leftOwner));
        leftOwner.connected = false;
        return leftOwner;
    }
}
