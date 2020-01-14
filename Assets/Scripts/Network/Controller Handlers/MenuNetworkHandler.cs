using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System.Linq;

public class MenuNetworkHandler : Singleton<MenuNetworkHandler>
{
    public List<int> premiumIds = new List<int>();
    void Awake()
    {
        if(AirConsole.instance == null)
			return;
		AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onPremium += OnPremium;
        AirConsole.instance.onDisconnect += OnDisconnect;
    }
    void OnConnect(int device_id){
		Owner owner = OwnersCoordinator.TryCreateOwner(device_id);
		if(owner){
			//then anounce the new owner
        	EventCoordinator.TriggerEvent(EventName.Input.Network.PlayerJoined(), GameMessage.Write().WithOwner(owner));
			//then add hero model to the Owner as Child:
			KingFactory.TryCreateHeroModel(owner);
		}
		else
			Debug.LogError("OnConnect returned null Owner!");
    }
    void OnDisconnect(int device_id){
		Owner owner = OwnersCoordinator.DisconnectOwner(device_id);
		if(!owner)
			Debug.LogWarning("OnDisconnect returned null Owner! Nothing to disconnect...");
        EventCoordinator.TriggerEvent(EventName.Input.Network.PlayerLeft(), GameMessage.Write().WithOwner(owner));

		foreach(Owner stayingOwner in OwnersCoordinator.GetOwners()){
			stayingOwner.ready = false;
		}
    }

	private void OnDestroy(){
		if (AirConsole.instance != null){
			AirConsole.instance.onConnect -= OnConnect;
            AirConsole.instance.onPremium -= OnPremium;
			AirConsole.instance.onDisconnect -= OnDisconnect;
		}
	}
	void OnPremium(int device_id){
		//Debug.Log("On Premium (device " + device_id + ") \n \n");
        premiumIds.Add(device_id);
	}
    //unused::::
	void OnHighScores (JToken highscores) {
		Debug.Log("On High Scores " + highscores + " \n \n");
		//logWindow.text = logWindow.text.Insert (0, "Converted Highscores: " + HighScoreHelper.ConvertHighScoresToTables(highscores).ToString() + " \n \n");
	}

	void OnHighScoreStored (JToken highscore) {
		if (highscore == null) {
			Debug.Log("On High Scores Stored (null)\n \n");
		} else {
			Debug.Log("On High Scores Stored " + highscore + "\n \n");
		}		
	}

	void OnPersistentDataStored (string uid) {
		//Log to on-screen Console
		Debug.Log("Stored persistentData for uid " + uid + " \n \n");
	}

	void OnPersistentDataLoaded (JToken data) {
		//Log to on-screen Console
		Debug.Log("Loaded persistentData: " + data + " \n \n");
	}
}