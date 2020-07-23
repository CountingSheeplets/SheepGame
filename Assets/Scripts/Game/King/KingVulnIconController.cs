using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingVulnIconController : MonoBehaviour {
    KingUnit king;
    public GameObject iconSpriteObj;
    void Start() {
        if (king == null) king = GetComponentInParent<KingUnit>();
        king.onVulnerabilityChange += OnStateChange;
    }

    void OnStateChange(bool state) {
        iconSpriteObj.SetActive(state);
    }
}