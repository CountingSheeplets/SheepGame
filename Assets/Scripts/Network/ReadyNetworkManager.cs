using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System.Linq;

public class ReadyNetworkManager : MonoBehaviour
{
    void Awake()
    {
		AirConsole.instance.onMessage += OnReady;
    }

	void OnReady(int from, JToken message){
		if (message ["element"]!= null)
			if (message ["element"].ToString () == "ready-button") {
				if (message ["data"]["pressed"]!= null) {
					bool ready = (bool)(message["data"]["pressed"]);
					//check if all ready then start
					List<Owner> owners = new List<Owner>(GetComponentsInChildren<Owner>());
					Owner readyOwner = owners.Where(x=>x.ownerId == from).FirstOrDefault();
					if(readyOwner == null)
						return;
					else
						readyOwner.ready = ready;
					TryStart();
				}
			}
	}
	void TryStart(){
		List<Owner> owners = new List<Owner>(GetComponentsInChildren<Owner>());
		foreach(Owner owner in owners){
			if (owner.ready == false){
				return;
			}
		}
		EventManager.TriggerEvent(EventName.Input.StartGame(), GameMessage.Write());
        var data = new Dictionary<string, string> { { "show_view_id", "view-0" } };
        AirConsole.instance.Broadcast(data);
	}
    private void OnDestroy(){
		if (AirConsole.instance != null){
			AirConsole.instance.onMessage -= OnReady;
		}
	}
}
