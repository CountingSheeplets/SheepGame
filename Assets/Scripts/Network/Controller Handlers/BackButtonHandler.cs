using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class BackButtonHandler : MonoBehaviour
{
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
                string viewToSet = ControllerView.currentViewStates[from];
                if(ControllerView.currentViewStates[from] == "upgrade")
                    viewToSet = "match";
                if(ControllerView.currentViewStates[from] == "details")
                    viewToSet = "post";
                    
                NetworkCoordinator.SendShowView(from, viewToSet);
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
