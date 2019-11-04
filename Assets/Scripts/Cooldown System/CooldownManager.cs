using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//REMAKE this to follow messaging system instead of direct calls!!!
public class CooldownManager : Singleton<CooldownManager>
{
    public float cdTickInterval = 0.1f;
    public List<CooldownGroup> cooldownGroups = new List<CooldownGroup>();

/*     protected override void OnAwake(){
        foreach(CooldownGroup cd in cooldownGroups){
            if (cd.sharedCdAbilities.Count > 0)
                cd.maxCooldown = cd.sharedCdAbilities[0].baseCooldown;
        }
    } */


    void Update(){
        foreach(CooldownGroup cdg in cooldownGroups){
            if (cdg.IsGroupOnCooldown()){
                cdg.ReduceCooldownInGroup(Time.deltaTime);
                if (cdg.HasCooldownTicked(cdTickInterval)){
                    EventManager.TriggerEvent(EventName.System.Cooldown.Tick(), GameMessage.Write().WithCooldownGroup(cdg));
                }
            }
            if (cdg.HasCooldownEnded()){
                EventManager.TriggerEvent(EventName.System.Cooldown.Ended(), GameMessage.Write().WithCooldownGroup(cdg));
            }
        }
    }
    public static bool StartCooldown(BaseAbility ability, Owner owner){
        CooldownGroup group = Instance.GetCooldownGroup(ability);
        if(group == null)
            return false;
        if (group.IsAvailable(owner)){
            //Debug.Log("CD Available: "+ability);
            group.StartCooldown(owner);
            //EventManager.TriggerEvent(EventName.System.CooldownStarted(), GameMessage.Write().WithCooldownGroup(group));
            return true;
        } else {
            //show cooldown fail UI message:
            //Debug.Log(ability + " on CD:  " +group.currentCooldown);
            EventManager.TriggerEvent(EventName.UI.ShowCooldownNotReady(), GameMessage.Write().WithCooldownGroup(group));
            return false;
        }
        //if (ability != GhostManager.ghost.GetBase())
            //AddCdMarker(ability);
    }

    CooldownGroup GetCooldownGroup(BaseAbility ability){
        CooldownGroup group = null;
        foreach(CooldownGroup cdg in cooldownGroups){
            //Debug.Log(cdg);
            if (ability.IsInListByType(cdg.sharedCdAbilities))
                group = cdg;
        }
        if(group == null){
            Debug.LogError("CooldownGroup not found for: "+ability+". Add it to at least one of the Groups!");
        }
        return group;
    }

    public static float GetCooldown(BaseAbility ability, Owner owner){
        CooldownGroup group = Instance.GetCooldownGroup(ability);
        if (group != null)
            return group.CurrentCooldown(owner);
        else return 0;
    }
}
