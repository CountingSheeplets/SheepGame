using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "PriceScriptable", menuName = "ScriptableObjects/PriceScriptable", order = 1)]
public class PriceScriptable : ScriptableObject
{
    //public float value;
    [SerializeField]
    public NamedPriceAttribute[] attributes;
    
    //[SerializeField]
    //public List<PriceBase> priceBases = new List<PriceBase>();
}
