using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public class CooldownGroup
{
    //base stuff:
    public List<BaseAbility> sharedCdAbilities = new List<BaseAbility>();
    private Dictionary<Owner, float> currentCooldowns = new Dictionary<Owner, float>();
    public float maxCooldown = 1f;
    private Dictionary<Owner, bool> isOnCooldown = new Dictionary<Owner, bool>();

    //for messages:
    private Dictionary<Owner, float> previousCooldownTicks = new Dictionary<Owner, float>();
    public List<Owner> tickedOwners = new List<Owner>();
    public List<Owner> endedOwners = new List<Owner>();

    public float CurrentCooldown(Owner owner){
/*         if (owner == null)
            return 0; */
        if (currentCooldowns.ContainsKey(owner)) // wrong evaluation....
            return currentCooldowns[owner];
        else{
            Debug.Log(owner.ownerType+" dosnt exist in: "+CollectionHelper.PrintDict(currentCooldowns));
            currentCooldowns[owner] = 0;
            return 0;
        }
    }

    public bool HasCooldownTicked(float deltaTick){
        tickedOwners.Clear();
        List<Owner> owners = new List<Owner>(currentCooldowns.Keys);
        foreach(Owner owner in owners){
            if (previousCooldownTicks.ContainsKey(owner) && previousCooldownTicks[owner] > 0){
                if ((previousCooldownTicks[owner] - CurrentCooldown(owner)) > deltaTick){
                    tickedOwners.Add(owner);
                    previousCooldownTicks[owner] = CurrentCooldown(owner);
                }
            } else {
                previousCooldownTicks[owner] = maxCooldown;
            }
        }
        if(tickedOwners.Count > 0)
            return true;
        return false;
    }
    public bool HasCooldownEnded(){
        endedOwners.Clear();
        List<Owner> owners = new List<Owner>(currentCooldowns.Keys);
        foreach(Owner owner in owners){
            if (WasOnCooldown(owner) ){
                Debug.Log("WasOnCooldown");
                EndCooldown(owner);
                endedOwners.Add(owner);
            }
        }
        if(endedOwners.Count > 0)
            return true;
        return false;
    }

    public bool IsGroupOnCooldown(){
        foreach(float cd in currentCooldowns.Values){
            if (cd > 0)
                return true;
        }
        return false;
    }

    public void ReduceCooldownInGroup(float delta){
        List<Owner> owners = new List<Owner>(currentCooldowns.Keys);
        foreach(Owner owner in owners){
            if (currentCooldowns[owner] > 0)
                currentCooldowns[owner] -= delta;
        }
    }

    bool CheckOnCooldown(Owner owner){
        if (isOnCooldown.ContainsKey(owner))
            return isOnCooldown[owner];
        else{
            isOnCooldown[owner] = false;
            return false;
        }
    }
    public bool IsOnCooldown(Owner owner){
        if(CurrentCooldown(owner) > 0f && CheckOnCooldown(owner))
            return true;
        else return false;
    }
    private bool WasOnCooldown(Owner owner){
        if(CheckOnCooldown(owner) && CurrentCooldown(owner) <= 0)
            return true;
        else return false;
    }

    public bool IsAvailable(Owner owner){
        return !IsOnCooldown(owner);
    }

    public void StartCooldown(Owner owner){
        currentCooldowns[owner] = maxCooldown;
        previousCooldownTicks[owner] = 0;
        isOnCooldown[owner] = true;
    }
/*     public void StartAllCooldowns(){
        foreach(Owner owner in currentCooldowns.Keys){
            StartCooldown(owner);
        }  
    } */
    private void EndCooldown(Owner owner){
        currentCooldowns[owner] = 0;
        isOnCooldown[owner] = false;
    }
/*     public void EndAllCooldowns(){
        foreach(Owner owner in currentCooldowns.Keys){
            EndCooldown(owner);
        }  
    } */
    public bool IsMyGroup(BaseAbility ability){
        if (ability.IsInListByType(sharedCdAbilities)){
            return true;
        } else {
            return false;
        }
    }

    public override string ToString()
    {
        string buildings = "[";
        foreach(BaseAbility b in sharedCdAbilities)
            buildings+=b+";";
        buildings+="] ";

        string cdsPerOwner = "[";
        foreach(Owner owner in currentCooldowns.Keys)
            cdsPerOwner+=owner + ":" +CurrentCooldown(owner)+";";
        cdsPerOwner+="] ";

        return "Buildings:" + buildings + "|| CDs:" + cdsPerOwner + "|| maxCd:"+ maxCooldown;
    }
}
