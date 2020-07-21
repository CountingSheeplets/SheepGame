using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfieldGrassAmountHandler : MonoBehaviour {
    SpriteRenderer rend;
    Owner owner;
    void Start() {
        EventCoordinator.StartListening(EventName.System.Economy.GrassChanged(), OnGrassChanged);
        rend = GetComponent<SpriteRenderer>();
        owner = GetComponentInParent<Owner>();
        rend.material.SetFloat("_RandomSeed", Random.Range(0, 10));
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Economy.GrassChanged(), OnGrassChanged);
    }
    void OnGrassChanged(GameMessage msg) {
        if (!owner.EqualsByValue(msg.owner))
            return;
        float fillPart = msg.targetFloatMessage / ConstantsBucket.MaxPlayfieldGrass;
        rend.material.SetFloat("_GrassFill", Mathf.Clamp(fillPart, 0, 1));
    }

}