using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisconnectedTranslHandler : MonoBehaviour {
    void OnEnable() {
        GetComponent<TextMeshProUGUI>().text = TranslationsHandler.GetDisconnectedTranslation();
    }
}