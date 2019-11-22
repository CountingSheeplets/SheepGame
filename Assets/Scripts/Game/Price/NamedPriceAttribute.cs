using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NamedPriceAttribute {

    [SerializeField]
    [StringInList(typeof(PropertyDrawersHelper), "AllPriceNames")]
    public string attributeName;

    [SerializeField]
    public PriceBase priceBase;

}
