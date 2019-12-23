using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEliminationHandler : MonoBehaviour
{
    Owner spreeOwner;
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.King.Killed(), OnKingKilled);
        //GameMessage.Write().WithKingUnit(this).WithOwner(killer).WithTargetOwner(eliminated));
    }
    void OnDestroy()
    {
        EventCoordinator.StopListening(EventName.System.King.Killed(), OnKingKilled);
    }
    void OnKingKilled(GameMessage msg)
    {
        if(msg.owner.EqualsByValue(spreeOwner))
            PriceCoordinator.IncreaseLevel(msg.owner, PriceName.Player.KingElimStar());

        int starReward = Mathf.FloorToInt(PriceCoordinator.GetPrice(msg.owner, PriceName.Player.KingElimStar()));
        PlayerProfileCoordinator.GetProfile(msg.owner).AddCrowns(starReward);

        float moneyReward = PriceCoordinator.GetPrice(msg.owner, PriceName.Player.KingElimGold());
        PlayerProfileCoordinator.GetProfile(msg.owner).AddMoney(moneyReward);

        if(PriceCoordinator.GetLevel(msg.owner, PriceName.Player.KingElimGold()) < 5);
            PriceCoordinator.IncreaseLevel(msg.owner, PriceName.Player.KingElimGold());
        PriceCoordinator.IncreaseLevel(msg.owner, PriceName.Player.KingElimGold());

        EventCoordinator.TriggerEvent(EventName.System.Player.Defeated(), msg);

        spreeOwner = msg.owner;
    }
}
