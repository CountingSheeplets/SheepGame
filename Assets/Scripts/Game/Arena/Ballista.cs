using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour
{
    SheepThrow sheepThrow;
    void Start()
    {
        sheepThrow = GetComponentInParent<SheepThrow>();
        EventCoordinator.StartListening(EventName.Input.Swipe(), OnSwipe);
        EventCoordinator.StartListening(EventName.System.Sheep.ReadyToLaunch(), OnSheepReady);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.Input.Swipe(), OnSwipe);
        EventCoordinator.StopListening(EventName.System.Sheep.ReadyToLaunch(), OnSheepReady);
    }

    void OnSwipe(GameMessage msg)
    {
        transform.rotation.SetLookRotation(msg.swipe.vector);
    }
    void OnSheepReady(GameMessage msg){
        //change animation state to ReadyToShoow
        transform.rotation.SetLookRotation(Vector2.up);
    }
}
