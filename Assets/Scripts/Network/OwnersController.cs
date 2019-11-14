using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnersController : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnStartGame(GameMessage msg){
        foreach(Owner owner in OwnersCoordinator.GetOwners())
            owner.gameObject.SetActive(false);
    }
    //void OnResetGame?
    //setActive = true?
}