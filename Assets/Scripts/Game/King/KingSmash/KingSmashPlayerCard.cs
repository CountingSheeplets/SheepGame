using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KingSmashPlayerCard : MonoBehaviour {
    Owner owner;
    Image image;
    public Color smashAvailable;
    public Color smashNotAvailable;
    void Start() {
        if (owner == null)owner = GetComponentInParent<PlayerCard>().owner;
        image = GetComponent<Image>();
        //EventCoordinator.StartListening(EventName.System.King.StartSmash(), OnSmash);
        //EventCoordinator.StartListening(EventName.System.King.SmashReset(), OnSmashReset);
        EventCoordinator.StartListening(EventName.System.Economy.GoldChanged(), OnMoneyChange);
        image.color = smashAvailable;
    }
    void OnDestroy() {
        //EventCoordinator.StopListening(EventName.System.King.StartSmash(), OnSmash);
        //EventCoordinator.StopListening(EventName.System.King.SmashReset(), OnSmashReset);
        EventCoordinator.StopListening(EventName.System.Economy.GoldChanged(), OnMoneyChange);
    }
    void OnSmash(GameMessage msg) {
        if (owner.EqualsByValue(msg.owner)) {
            image.color = smashNotAvailable;
        }
    }
    void OnSmashReset(GameMessage msg) {
        image.color = smashAvailable;
    }

    void OnMoneyChange(GameMessage msg) {
        if (msg.owner.GetPlayerProfile().GetMoney() > PriceCoordinator.GetPrice(owner, PriceName.King.Smash())) {
            image.color = smashAvailable;
        } else
            image.color = smashNotAvailable;
    }
}