using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerOwnerTile : MonoBehaviour
{
    Owner myOwner;
    public TextMeshProUGUI crownCountText;

    public GameObject shade;
    public void Ready(bool state){
        shade.SetActive(state);
    }
    void Start(){
        myOwner = GetComponentInParent<Owner>();
        EventCoordinator.StartListening(EventName.System.Player.ProfileUpdate(), OnProfileUpdate);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.Player.ProfileUpdate(), OnProfileUpdate);
    }
    void OnProfileUpdate(GameMessage msg){
        if(myOwner == msg.owner);
            crownCountText.text = msg.playerProfile.permanentCrownCount.ToString();
    }
}
