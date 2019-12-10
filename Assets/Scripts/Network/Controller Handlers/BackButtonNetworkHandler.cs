using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class BackButtonNetworkHandler : MonoBehaviour
{
    string currentViewState = "menu";
    void Start()
    {
        AirConsole.instance.onMessage += OnBack;
    }

    void OnBack(int from, JToken message)
    {
        if (message["element"] != null)
        {
            if (message["element"].ToString() == "view" && message["value"].ToString() == "back")
            {
                string viewToSet = "menu";
                if(currentViewState == "upgrade")
                    viewToSet = "match";
                if(currentViewState == "details")
                    viewToSet = "post";
                    
                NetworkCoordinator.SendShowView(from, viewToSet);
            } else {
                if (message["element"].ToString() == "view"){
                    currentViewState = message["value"].ToString();
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnBack;
        }
    }
}
