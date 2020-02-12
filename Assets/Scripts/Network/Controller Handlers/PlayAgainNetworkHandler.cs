using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;
public class PlayAgainNetworkHandler : MonoBehaviour
{
    void Awake()
    {
        if(AirConsole.instance != null)
            AirConsole.instance.onMessage += OnReady;
    }

    void OnReady(int from, JToken message)
    {
        if(!GameStateView.HasState(GameState.ended))
            return;
        if (message["element"] != null)
            if (message["element"].ToString() == "playAgain")
            {
                bool playAgain = (bool)(message["pressed"]);
                Owner readyOwner = OwnersCoordinator.GetOwner(from);
                if (readyOwner == null)
                    return;
                else{
                    readyOwner.playAgain = playAgain;
                }
                if(TryRestart(GameMessage.Write())){
                    AirConsole.instance.ShowAd();
                    SceneManager.LoadScene("main");
                    NetworkCoordinator.SendShowViewAll("menu");
                };
                Debug.Log("Ready:" + readyOwner);
            }
    }
    bool TryRestart(GameMessage msg)
    {
        foreach (Owner owner in OwnersCoordinator.GetOwners())
        {
            if (owner.playAgain == false)
            {
                Debug.Log("player not ready to restart:"+owner);
                return false;
            }
        }
        Debug.Log("players ready. restarting...");
        return true;
    }
    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnReady;
        }
    }
}
