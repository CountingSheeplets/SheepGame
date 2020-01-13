﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class PersistentDataController : MonoBehaviour
{
    void Start()
    {
        AirConsole.instance.onPersistentDataStored += OnDataStored;
        AirConsole.instance.onPersistentDataLoaded += OnDataLoaded;
		EventCoordinator.StartListening(EventName.System.Player.ProfileCreated(), OnProfileCreated);
		EventCoordinator.StartListening(EventName.UI.ShowScoreScreen(), OnShowScore);
    }
	void OnProfileCreated(GameMessage msg){//perhaps add the requested owner, and keep asking for update until its received?
		PersistentDataCoordinator.RequestData(msg.playerProfile.owner);
	}
	void OnShowScore(GameMessage msg){
		foreach(Owner owner in OwnersCoordinator.GetOwners()){
			Debug.Log("storing data for owner:"+owner);
			PersistentDataCoordinator.StoreData(owner);
		}
	}
	void OnDataLoaded (JToken data) {
        PersistentDataCoordinator.OnReceivedData(data);
	}
	void OnDataStored (string uid) {
        OwnersCoordinator.GetOwner(uid).GetPlayerProfile().CleanNetworkDirty();
	}
	void OnDestroy () {
		if (AirConsole.instance != null) {
			AirConsole.instance.onPersistentDataStored -= OnDataStored;
			AirConsole.instance.onPersistentDataLoaded -= OnDataLoaded;
		}
	}
}
