using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class SheepUpgradeFxSpawner : MonoBehaviour {
    public GameObject fxPrefabLvl1;
    public GameObject fxPrefabLvl2;
    Playfield playfield;
    void Start() {
        playfield = GetComponentInParent<Playfield>();
        EventCoordinator.StartListening(EventName.System.Sheep.Upgraded(), OnUpgrade);
    }
    private void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Upgraded(), OnUpgrade);
    }
    void OnUpgrade(GameMessage msg) {
        if (msg.sheepUnit.currentPlayfield == playfield) {
            GameObject fxPrefab = fxPrefabLvl1;
            if (msg.sheepUnit.sheepType != (SheepType.Small) && msg.sheepUnit.sheepType != (SheepType.Armored)) {
                fxPrefab = fxPrefabLvl2;
            }
            GameObject newFx = Instantiate(fxPrefab, transform);
            SetColor(newFx, msg.sheepUnit.currentPlayfield.owner.GetPlayerProfile().playerColor);
            Destroy(newFx, 2f);
        }
    }
    void SetColor(GameObject fx, Color color) {
        foreach (ParticleSystem pt in fx.GetComponentsInChildren<ParticleSystem>()) {
            Debug.Log("color: " + color);
            pt.GetComponent<Renderer>().material.SetColor("_TintColor", color);
        }
    }
}