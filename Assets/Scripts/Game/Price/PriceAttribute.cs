using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceAttribute
{
    [SerializeField]
    public Dictionary<string, PriceBase> priceBases = new Dictionary<string, PriceBase>();

    public PriceBase GetPriceBase (string priceName) {
        if(priceBases.ContainsKey(priceName))
            return priceBases[priceName];
        else return null;
    }
    
    public PriceAttribute(PriceScriptable priceScriptable){
        foreach(NamedPriceAttribute attr in priceScriptable.attributes){
            priceBases.Add(attr.attributeName, new PriceBase(attr.priceBase));
        }
    }
}