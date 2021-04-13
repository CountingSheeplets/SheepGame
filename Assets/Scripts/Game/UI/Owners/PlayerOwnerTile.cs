using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PlayerOwnerTile : MonoBehaviour {
    public Owner myOwner;
    public TextMeshProUGUI crownCountText;
    Animator animator;
    public GameObject readyTextGO;
    public void Ready(bool state) {
        readyTextGO.GetComponentInChildren<TextMeshProUGUI>().text = TranslationsHandler.GetReadyTranslation() + "!";
        readyTextGO.SetActive(state);
        if (animator == null)
            return;
        if (state)
            animator.SetTrigger("startLoop");
        else
            animator.SetTrigger("stopLoop");
    }
    public void SetOwner(Owner owner) {
        myOwner = owner;
    }
    void Start() {
        EventCoordinator.StartListening(EventName.System.Player.ProfileUpdate(), OnProfileUpdate);
        animator = GetComponentInChildren<Animator>();
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Player.ProfileUpdate(), OnProfileUpdate);
    }
    void OnProfileUpdate(GameMessage msg) {
        if (myOwner == msg.owner)
            crownCountText.text = msg.playerProfile.permanentCrownCount.ToString();
    }
}