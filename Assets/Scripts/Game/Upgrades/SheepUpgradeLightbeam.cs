using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepUpgradeLightbeam : MonoBehaviour {
    public Animator anim;
    void Start() {
        if (anim == null)
            anim = GetComponent<Animator>();
        EventCoordinator.StartListening(EventName.System.Sheep.Upgraded(), OnUpgrade);
    }
    private void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Sheep.Upgraded(), OnUpgrade);
    }
    void OnUpgrade(GameMessage msg) {
        if (msg.sheepUnit.currentPlayfield == GetComponentInParent<Playfield>())
            anim.SetTrigger("Show");
    }
}