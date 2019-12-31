using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KingSmiteHandler : MonoBehaviour
{
    public float hitRange = 1f;
    public List<Owner> smashers = new List<Owner>();
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.KingAbilities.Smash(), OnSmash);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.Input.KingAbilities.Smash(), OnSmash);
    }

    void OnSmash(GameMessage msg){ // if there's a team mode, this needs to be handled differently
        if(!msg.owner.InListByType(smashers))
            smashers.Add(msg.owner);
        else
            return;

        PlayerProfile profile = PlayerProfileCoordinator.GetProfile(msg.owner);
        if(profile.Buy(PriceName.King.Smash())){
            PriceCoordinator.IncreaseLevel(msg.owner, PriceName.King.Smash());
            KingUnit king = msg.owner.GetKing();
            List<SheepUnit> sheepWithinRange = SheepCoordinator.GetSheepInField(king.myPlayfield)
                .Where(x => (x.GetComponent<Transform>().position-transform.position).magnitude <= hitRange)
                .Where(x => x.sheepType == SheepType.Tank)
                .ToList();
            EventCoordinator.TriggerEvent(EventName.System.King.Smashed(),
                GameMessage.Write()
                    .WithOwner(msg.owner)
                    .WithSheepUnits(sheepWithinRange)
                    .WithKingUnit(king)
                );
        }
        Debug.Log("smashers: "+smashers.Count+"  alive:"+PlayerProfileCoordinator.GetAliveOwners().Count);
        if(smashers.Count >= PlayerProfileCoordinator.GetAliveOwners().Count){
            EventCoordinator.TriggerEvent(EventName.System.King.SmashReset(), GameMessage.Write());
            smashers.Clear();
        }
    }
}