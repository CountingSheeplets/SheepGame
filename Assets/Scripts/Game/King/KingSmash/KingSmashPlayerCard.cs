using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KingSmashPlayerCard : MonoBehaviour
{
    Owner owner;
    Image image;
    public Color smashAvailable;
    public Color smashNotAvailable;
    void Start()
    {
        if(owner == null) owner = GetComponentInParent<PlayerCard>().owner;
        image = GetComponent<Image>();
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnSmash);
        EventCoordinator.StartListening(EventName.System.King.SmashReset(), OnSmashReset);
        image.color = smashAvailable;
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnSmash);
        EventCoordinator.StopListening(EventName.System.King.SmashReset(), OnSmashReset); 
    }
    void OnSmash(GameMessage msg)
    {
        if(owner.EqualsByValue (msg.owner)){
            image.color = smashNotAvailable;
        }
    }
    void OnSmashReset(GameMessage msg){
            image.color = smashAvailable;
    }
}
