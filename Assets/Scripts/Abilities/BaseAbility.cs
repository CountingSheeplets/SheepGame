using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AbilityType{
    sheep,
    king,
    upgrade,
    action,
    purchase
}
public class BaseAbility : MonoBehaviour
{
    public string abilityName;
    public Sprite icon;

    [BitMask(typeof(AbilityType))]
    public AbilityType abilityType;

    public void Init(BaseAbility newBase){
        abilityName = newBase.abilityName;
        icon = newBase.icon;
        abilityType = newBase.abilityType;
    }

    public BaseAbility GetBase(){
        return (BaseAbility)this;
    }

    public bool EqualsByType(BaseAbility obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null)) 
        {
            //Debug.Log("null or bad Type");
            return false;
        }
        else {
            BaseAbility p = (BaseAbility) obj; 
            bool theCheck = (abilityName == p.abilityName) && (abilityType == p.abilityType);
            //if (theCheck)
                //Debug.Log(this.ToString()+" vs "+obj.ToString());
            return theCheck;
        }   
    }
    public override string ToString()
    {
        return "BaseAbility:" + abilityName + ":" + abilityType;// + ": no id implementation";//+ Id;
    }

        public bool IsInListByType(List<BaseAbility> list){
        foreach(BaseAbility b in list){
            if (EqualsByType(b)){
                return true;
            }
        }
        return false;
    }
    public BaseAbility InListByType(List<BaseAbility> list){
        foreach(BaseAbility b in list){
            if (EqualsByType(b)){

                return b;
            }
        }
        return null;
    }
}
