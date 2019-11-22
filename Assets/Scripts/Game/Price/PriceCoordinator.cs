using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceCoordinator : Singleton<PriceCoordinator>
{
    public PriceScriptable defaultPrices;
    [SerializeField]
    public Dictionary<Owner, PriceAttribute> priceAttributes = new Dictionary<Owner, PriceAttribute>();
    public static void AddPriceAttribute(Owner owner){
        if(!Instance.priceAttributes.ContainsKey(owner))
            Instance.priceAttributes.Add(owner, new PriceAttribute(Instance.defaultPrices));
        else Debug.Log("already contains owner for PriceAttribute!");
    }
    public static void RemovePriceAttribute(Owner owner){
        if(Instance.priceAttributes.ContainsKey(owner))
            Instance.priceAttributes.Remove(owner);
        else Debug.Log("no Such PRiceAttribute!");
    }
    public static int GetLevel(Owner owner, string priceName){
        PriceAttribute attr = Instance.GetAttribute(owner);
        if(attr != null)
        {
            PriceBase pBase = attr.GetPriceBase(priceName);
            if(pBase != null)
                return pBase.level;
        }
        return 0;
    }
    public static void IncreaseLevel(Owner owner, string priceName){
        PriceAttribute attr = Instance.GetAttribute(owner);
        if(attr != null)
        {
            PriceBase pBase = attr.GetPriceBase(priceName);
            if(pBase != null)
                pBase.level++;
        }
    }
    public static void DecreaseLevel(Owner owner, string priceName){
        PriceAttribute attr = Instance.GetAttribute(owner);
        if(attr != null)
        {
            PriceBase pBase = attr.GetPriceBase(priceName);
            if(pBase != null)
                pBase.level--;
        }
    }
    public static float GetPrice(Owner owner, string priceName){
        PriceAttribute attr = Instance.GetAttribute(owner);
        if(attr != null)
        {
            PriceBase pBase = attr.GetPriceBase(priceName);
            if(pBase != null)
                return pBase.basePrice + pBase.increment * pBase.level;
        }
        return 9999999f;
    }
    public PriceAttribute GetAttribute(Owner owner){
        if(priceAttributes.ContainsKey(owner))
            return priceAttributes[owner];
        else
            return null;
    }
}
