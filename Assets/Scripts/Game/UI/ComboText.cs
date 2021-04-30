using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class ComboText : MonoBehaviour {
    TextMeshProUGUI comboText;
    public Animator animator;
    Owner owner;
    void Start() {
        comboText = GetComponentInChildren<TextMeshProUGUI>();
        EventCoordinator.StartListening(EventName.System.Economy.ComboChanged(), OnComboChanged);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.Economy.ComboChanged(), OnComboChanged);
    }
    void OnComboChanged(GameMessage msg) {
        if (owner == null)owner = GetComponentInParent<PlayerCard>().owner;
        if (owner.EqualsByValue(msg.owner)) {
            if (msg.intMessage > 1) {
                animator.SetTrigger("pop");
                comboText.text = "X" + msg.intMessage;
            } else
                comboText.text = "";
        }
    }
}