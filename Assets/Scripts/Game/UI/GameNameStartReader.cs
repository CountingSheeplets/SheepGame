using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNameStartReader : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnStartGame(GameMessage msg)
    {
        gameObject.SetActive(true);
    }
}
