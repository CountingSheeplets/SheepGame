﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PlayerOwnerTile : MonoBehaviour {
    public Owner myOwner;
    public TextMeshProUGUI crownCountText;
    Animator animator;
    public void Ready(bool state) {
        animator.SetTrigger("startLoop");
    }
    public void SetOwner(Owner owner) {
        myOwner = owner;
    }
    void Start() {
        //myOwner = GetComponentInParent<Owner>();
        EventCoordinator.StartListening(EventName.System.Player.ProfileUpdate(), OnProfileUpdate);
        animator = GetComponent<Animator>();
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Player.ProfileUpdate(), OnProfileUpdate);
    }
    void OnProfileUpdate(GameMessage msg) {
        if (myOwner == msg.owner)
            crownCountText.text = msg.playerProfile.permanentCrownCount.ToString();
    }
}