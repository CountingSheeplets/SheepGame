using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSmashHandler : MonoBehaviour {
    //List<Owner> smashers = new List<Owner>();
    void Start() {
        EventCoordinator.StartListening(EventName.Input.KingAbilities.Smash(), OnSmash);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.KingAbilities.Smash(), OnSmash);
    }

    void OnSmash(GameMessage msg) { // if there's a team mode, this needs to be handled differently
        /*         if (!msg.owner.InListByType(smashers))
                    smashers.Add(msg.owner);
                else
                    return; */

        PlayerProfile profile = PlayerProfileCoordinator.GetProfile(msg.owner);
        if (profile.Buy(PriceName.King.Smash())) {
            PriceCoordinator.IncreaseLevel(msg.owner, PriceName.King.Smash());
            KingUnit king = msg.owner.GetKing();

            EventCoordinator.TriggerEvent(EventName.System.King.StartSmash(),
                GameMessage.Write()
                .WithOwner(msg.owner)
                .WithKingUnit(king)
            );
        } else {
            EventCoordinator.TriggerEvent(EventName.System.King.NotStartSmash(),
                GameMessage.Write()
                .WithOwner(msg.owner)
            );
        }
        //Debug.Log("smashers: " + smashers.Count + "  alive:" + PlayerProfileCoordinator.GetAliveOwners().Count);
        /*         if (smashers.Count >= PlayerProfileCoordinator.GetAliveOwners().Count) {
                    EventCoordinator.TriggerEvent(EventName.System.King.SmashReset(), GameMessage.Write());
                    smashers.Clear();
                } */
    }
}