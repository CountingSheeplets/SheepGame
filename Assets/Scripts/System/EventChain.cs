using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChain : MonoBehaviour {
    void Start () {
        //EventManager.Attach(EventName.Input.FactionSelected(), UpdateTalentTree);
        //last attached:
        //EventManager.Attach(EventName.System.BaseTargetUpdate(), PostTargetUpdate);
        //EventManager.Attach(EventName.System.PostTargetUpdate(), GraphicsTargetUpdate);
    }

    void UpdateTalentTree(GameMessage msg)
    {
        //EventManager.TriggerEvent(EventName.UI.UpdateTalentTree(), msg);
    }

}