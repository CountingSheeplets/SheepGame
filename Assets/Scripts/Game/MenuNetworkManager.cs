using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System.Linq;

public class MenuNetworkManager : Singleton<MenuNetworkManager>
{
    List<int> premiumIds = new List<int>();
    void Awake()
    {
		AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onPremium += OnPremium;
        AirConsole.instance.onDisconnect += OnDisconnect;
    }
    void OnConnect(int device_id){
        Debug.Log("OnConnect");
		//To get the controller's name right, we get their nickname by using the device id we just saved
		string nicknameOfJoined = AirConsole.instance.GetNickname (device_id);
        GameObject go = new GameObject();
        go.transform.parent = gameObject.transform;
        Owner newOwner = go.AddComponent<Owner>();
        newOwner.Create(device_id, nicknameOfJoined, premiumIds.Contains(device_id));
        go.name = newOwner.ownerName;
        EventManager.TriggerEvent(EventName.Input.Network.PlayerJoined(), GameMessage.Write().WithOwner(newOwner));
    }
    void OnDisconnect(int device_id){
        List<Owner> owners = new List<Owner>(GetComponentsInChildren<Owner>());
        Owner leftOwner = owners.Where(x=>x.ownerId == device_id).FirstOrDefault();
        if(leftOwner == null)
            return;
        EventManager.TriggerEvent(EventName.Input.Network.PlayerLeft(), GameMessage.Write().WithOwner(leftOwner));
        Destroy(leftOwner.gameObject);
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
