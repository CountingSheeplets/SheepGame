using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using NDream.AirConsole;
public class UpgradeInfoNetworkHandler : MonoBehaviour
{
    void Awake()
    {
        AirConsole.instance.onMessage += OnBack;
        AirConsole.instance.onMessage += OnOpenUpgradeInfo;
    }

    void OnOpenUpgradeInfo(int from, JToken message)
    {
        if (message["element"] != null)
        {
            if (message["element"].ToString() == "info-A")
            {
                //load data on ability A:

                //send to show view for info:
                var data = new Dictionary<string, string> { { "show_view_id", "view-2" } };
                AirConsole.instance.Message(from, data);
            }
            if (message["element"].ToString() == "info-B")
            {
                //load data on ability B:

                //send to show view for info:
                var data = new Dictionary<string, string> { { "show_view_id", "view-2" } };
                AirConsole.instance.Message(from, data);
            }
        }
    }
    void OnBack(int from, JToken message)
    {
        if (message["element"] != null)
        {
            if (message["element"].ToString() == "info-back")
            {
                var data = new Dictionary<string, string> { { "show_view_id", "view-1" } };
                AirConsole.instance.Message(from, data);
            }
        }
    }
    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnBack;
            AirConsole.instance.onMessage -= OnOpenUpgradeInfo;
        }
    }
}
