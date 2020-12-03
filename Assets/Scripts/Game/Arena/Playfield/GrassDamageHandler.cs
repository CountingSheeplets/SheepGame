using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassDamageHandler : MonoBehaviour {
    Playfield playfield;
    public GameObject grassDamageParticleFxPrefab;
    void Start() {
        playfield = GetComponent<Playfield>();
        EventCoordinator.StartListening(EventName.System.Sheep.KingMissed(), OnSheepLand);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.KingMissed(), OnSheepLand);
    }

    void OnSheepLand(GameMessage msg) {
        if (msg.playfield != playfield)return;
        //Debug.Log("KingMissed");
        float damage = 0f;
        if (msg.sheepUnit.lastHandler == playfield.owner) {
            //Debug.Log("lastHandler is same");
            damage = DamageBucket.GetKickLandDamage(msg.sheepUnit.sheepType);
        } else {
            //Debug.Log("lastHandler not same!");
            damage = DamageBucket.GetBallistaLandDamage(msg.sheepUnit.sheepType);
        }
        if (damage > 0f) {
            GameObject newParticleFx = Instantiate(grassDamageParticleFxPrefab, msg.sheepUnit.transform.position, Quaternion.identity, transform);
            playfield.owner.GetPlayerProfile().ModifyGrass(-damage);
            Destroy(newParticleFx, 1f);
        }
    }
}