using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingHealthBar : MonoBehaviour
{
    public KingUnit king;
    Vector3 originalScale;
    void Start()
    {
        king = GetComponentInParent<KingUnit>();
        king.onReceivedDamage+=OnReceivedDamage;
        originalScale = transform.localScale;
    }
    void OnDestroy(){
        king.onReceivedDamage-=OnReceivedDamage;
    }
    void OnReceivedDamage(float damage)
    {
        float hpPerc = king.GetHealth / king.GetMaxHealth;
        Debug.Log("Updating healthbar:"+hpPerc);
        transform.localScale = new Vector3(originalScale.x * hpPerc, originalScale.y, originalScale.z);
    }
}
