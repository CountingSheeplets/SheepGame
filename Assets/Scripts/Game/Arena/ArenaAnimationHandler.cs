using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaAnimationHandler : MonoBehaviour
{
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Environment.PlayfieldAnimated(), OnPlayfieldAnimated);
    }
    void OnPlayfieldAnimated(GameMessage msg){
        bool allAnimationsFinished = false;
        foreach(Playfield playfield in ArenaCoordinator.GetPlayfields){
            allAnimationsFinished = true;
            if(playfield.isAnimating)
                allAnimationsFinished = false;
            //Debug.Log("playfield check:"+playfield.isAnimating);
        }
        if(allAnimationsFinished){
            EventCoordinator.TriggerEvent(EventName.System.Environment.ArenaAnimated(), GameMessage.Write());
        }
    }
}
