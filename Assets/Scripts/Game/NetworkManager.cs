﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using NDream.AirConsole;

public class NetworkManager : Singleton<NetworkManager>
{
	void Awake (){
		AirConsole.instance.onMessage += OnSwipe;
		AirConsole.instance.onMessage += OnBack;
        AirConsole.instance.onMessage += OnOpenKingUpgrades;
        AirConsole.instance.onMessage += OnOpenSheepUpgrades;
	}
    void OnSwipe(int from, JToken message){
			Debug.Log ("int from: " + from+"   "+message);
			if (message ["data"]["element"] != null) {
					if (message ["element"].ToString () == "swipe-field") {
									Swipe newSwipe = new Swipe(message["data"]["element"]);
									EventManager.TriggerEvent(EventName.Input.Swipe(), GameMessage.Write().WithSwipe(newSwipe));
				}
			}
    }
    void OnBack(int from, JToken message){
		if (message ["element"] != null) {
		    if (message ["element"].ToString () == "sheep-upgrade-back" || message ["element"].ToString () == "king-upgrade-back") {
								var data = new Dictionary<string, string> { { "show_view_id", "view-0" } };
                AirConsole.instance.Message(from, data);
			}
		}
    }
    void OnOpenSheepUpgrades(int from, JToken message){
        if (message ["element"] != null) {
		    if (message ["element"].ToString () == "sheep-upgrades-button") {
								var data = new Dictionary<string, string> { { "show_view_id", "view-3" } };
                AirConsole.instance.Message(from, data);
			}
		}
    }
    void OnOpenKingUpgrades(int from, JToken message){
        if (message ["element"] != null) {
		    if (message ["element"].ToString () == "king-upgrades-button") {
								var data = new Dictionary<string, string> { { "show_view_id", "view-1" } };
                AirConsole.instance.Message(from, data);
			}
		}
    }
	private void OnDestroy(){
		if (AirConsole.instance != null){
			AirConsole.instance.onMessage -= OnSwipe;
			AirConsole.instance.onMessage -= OnBack;
			AirConsole.instance.onMessage -= OnOpenKingUpgrades;
			AirConsole.instance.onMessage -= OnOpenSheepUpgrades;
		}
	}
}
