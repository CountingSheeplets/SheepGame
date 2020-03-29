using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NamedPriceAttribute {

#if UNITY_EDITOR
    [StringInList(typeof(PropertyDrawersHelper), "AllPriceNames")]
#endif
    [SerializeField]
    public string attributeName;

    [SerializeField]
    public PriceBase priceBase;

}