using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using System.Linq;
using TMPro;

public class OwnersCoordinator : Singleton<OwnersCoordinator>
{
    List<Owner> owners = new List<Owner>();
    public GameObject ownerTilePrefab;
    public Transform ownerPanelContainer;
    int counter = 0;
    public static List<Owner> GetOwners(){
        return Instance.owners;//when getting this, it shoulnt be able to change (but only wont change if instance into a new List),else the original will also change...
    }

    public static Owner TryCreateOwner(int device_id){
        Owner candidateOwner = Instance.owners.Where(x=>x.ownerId == AirConsole.instance.GetUID(device_id)).FirstOrDefault();
        if(candidateOwner == null){
            Debug.Log("didnt find owner, creating new one!");
            GameObject go = Instantiate(Instance.ownerTilePrefab, Instance.ownerPanelContainer);
            string nicknameOfJoined = AirConsole.instance.GetNickname (device_id);
            Owner newOwner = go.AddComponent<Owner>();
            Instance.owners.Add(newOwner);
            newOwner.Create(AirConsole.instance.GetUID(device_id), nicknameOfJoined, MenuNetworkHandler.Instance.premiumIds.Contains(device_id), device_id);
            go.name = newOwner.ownerName;
            go.GetComponentInChildren<TextMeshProUGUI>().text = newOwner.ownerName;
            return newOwner;
        } else {
            candidateOwner.deviceId = device_id;
            candidateOwner.connected = true;
            Debug.Log("Reconnect succesfull!");
            if((GameStateView.GetGameState() & GameState.started) != 0)
                NetworkCoordinator.SendShowView(device_id, "match");
            else {
                NetworkCoordinator.SendShowView(device_id, "menu");
            }
            if(GameStateView.GetGameState() != GameState.started)
                candidateOwner.gameObject.SetActive(true);
            return candidateOwner;
        }
    }
    public static Owner GetOwner(int device_id){
        return Instance.owners.Where(x=>x.deviceId == device_id).FirstOrDefault();
    }
    public static Owner GetRandomOwner(){
        if(Instance.owners.Count > 0){
            List<Owner> aliveOwners = new List<Owner> (Instance.owners.Where(x => x.GetPlayerProfile().isAlive).ToList());
            return aliveOwners[Random.Range(0, aliveOwners.Count)];
        }
        else return null;
    }
    public static Owner CreateEmptyOwner(){
        Instance.counter++;
        int device_id = Instance.counter;
        GameObject go = Instantiate(Instance.ownerTilePrefab, Instance.ownerPanelContainer);
        string nicknameOfJoined = Generate.RandomString(10);
        Owner newOwner = go.AddComponent<Owner>();
        Instance.owners.Add(newOwner);
        newOwner.Create(device_id.ToString(), nicknameOfJoined, true, device_id);
        go.name = newOwner.ownerName;
        go.GetComponentInChildren<TextMeshProUGUI>().text = newOwner.ownerName;
        return newOwner;
    }
    public static Owner DisconnectOwner(int device_id){
        Owner leftOwner = GetOwner(device_id);
        if(leftOwner == null)
            return null;

        Debug.Log("DisconnectOwner GetGameState:"+(int)GameStateView.GetGameState());
        
        if((GameStateView.GetGameState() & GameState.started) != 0){
            Debug.Log("disconnecting an owner..");
            leftOwner.connected = false;
            leftOwner.gameObject.SetActive(false);
        } else {
            Debug.Log("destroying an owner..");
            Instance.owners.Remove(leftOwner);
            Destroy(leftOwner.gameObject);
        }
            return leftOwner;
    }
}