using System.Collections;
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
		EventCoordinator.Attach(EventName.Input.ChangeHat(), OnChangeHat);
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
	void OnChangeHat (GameMessage msg){
		PersistentDataCoordinator.StoreData(msg.owner);
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
		EventCoordinator.StopListening(EventName.System.Player.ProfileCreated(), OnProfileCreated);
		EventCoordinator.StopListening(EventName.UI.ShowScoreScreen(), OnShowScore);
		EventCoordinator.Detach(EventName.Input.ChangeHat(), OnChangeHat);
	}
}
