using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class ControllerView : Singleton<ControllerView>
{
    public static Dictionary<int, string> currentViewStates = new Dictionary<int, string>();
    public static Dictionary<int, string> previousViewStates = new Dictionary<int, string>();
    void Start()
    {
        AirConsole.instance.onMessage += OnViewMessage;
    }

    void OnViewMessage(int from, JToken message)
    {
        if (message["element"] != null)
        {
            if (message["element"].ToString() == "view"){
                if(previousViewStates.ContainsKey(from))
                    if(previousViewStates[from] != currentViewStates[from])
                        previousViewStates[from] = currentViewStates[from];

                currentViewStates[from] = message["value"].ToString();
                if(!previousViewStates.ContainsKey(from))
                    previousViewStates[from] = currentViewStates[from];
                EventCoordinator.TriggerEvent(EventName.Input.Network.View(), GameMessage.Write().WithOwner(OwnersCoordinator.GetOwner(from)));
            }
        }
    }

    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnViewMessage;
        }
    }
}
