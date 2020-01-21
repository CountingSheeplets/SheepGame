using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChanger : MonoBehaviour
{
    public SetActive toState;
    [StringInList(typeof(PropertyDrawersHelper), "AllEventNames")]
    public string changeStateEventName;
    //public Component changeTarget;

    void Start(){//EventName.Input.StartGame()
        EventCoordinator.StartListening(changeStateEventName, OnStart);
        switch(toState){
            case SetActive.enable:
                gameObject.SetActive(false);
                break;
            case SetActive.disable:
                gameObject.SetActive(true);
                break;
        }
    }
    void OnDestroy(){ 
        EventCoordinator.StopListening(changeStateEventName, OnStart);
    }
    void OnStart(GameMessage msg){
        //activate visuals here
        switch(toState){
            case SetActive.enable:
                gameObject.SetActive(true);
                break;
            case SetActive.disable:
                gameObject.SetActive(false);
                break;
        }
    }

}
public enum SetActive {
    enable,
    disable
}
