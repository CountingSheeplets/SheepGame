using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using NDream.AirConsole;
using System.Linq;
public class KingButtonsNetwork : MonoBehaviour
{
    void Awake()
    {
        AirConsole.instance.onMessage += OnButtonClick;
    }
    void OnButtonClick(int from, JToken message)
    {
        Debug.Log("int from: " + from + "   " + message);
        if (message["data"] != null)
                Debug.Log("contains...");
            if   (message["element"].ToString().Contains("king-upgrade"))
            {
                if (message["data"]["upgrade"] != null)
                {
                Debug.Log("cases...");
                    Owner triggerOwner = OwnersManager.GetOwner(from);
                    if(triggerOwner == null)
                        return;
                    int upgrade = (int)message["data"]["upgrade"];
                    switch(upgrade){
                        case 1:
                            EventManager.TriggerEvent(EventName.Input.KingAbilities.SpawnSheep(), GameMessage.Write().WithOwner(triggerOwner));
                            break;
                        case 2:
                            EventManager.TriggerEvent(EventName.Input.KingAbilities.BuyLawn(), GameMessage.Write().WithOwner(triggerOwner));
                        break;
                        case 3:
                        break;
                        case 4:
                        break;
                        case 5:
                        break;
                        case 6:
                        break;
                    }
                }
            }
    }

    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnButtonClick;
        }
    }
}
