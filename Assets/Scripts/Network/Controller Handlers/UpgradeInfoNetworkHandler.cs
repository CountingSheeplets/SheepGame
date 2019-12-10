using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using NDream.AirConsole;
public class UpgradeInfoNetworkHandler : MonoBehaviour
{
    void Awake()
    {
        AirConsole.instance.onMessage += OnOpenUpgradeInfo;
    }

    void OnOpenUpgradeInfo(int from, JToken message)
    {
        if (message["element"] != null)
        {
            if (message["element"].ToString() == "upgrade1")
            {
                //load data on ability A:
                NetworkCoordinator.SendUpgradeData(from, "upgrade1");
                //send to show view for info:
                NetworkCoordinator.SendShowView(from, "upgrade");
            }
            if (message["element"].ToString() == "upgrade2")
            {
                //load data on ability B:
                NetworkCoordinator.SendUpgradeData(from, "upgrade2");
                //send to show view for info:
                NetworkCoordinator.SendShowView(from, "upgrade");
            }
        }
    }

    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnOpenUpgradeInfo;
        }
    }
}
