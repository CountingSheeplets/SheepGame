using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlusGold : MonoBehaviour {

    public Color playerColor;
    public TextMeshProUGUI goldText;
    public Image icon;

    public void Setup(PlayerProfile profile, float amount) {
        playerColor = profile.playerColor;

        goldText.text = ((int)amount).ToString();
        goldText.color = playerColor;
        icon.color = playerColor;

        transform.position = transform.position + new Vector3(Random.Range(-0.1f, 0.1f), 0, 0);
        Destroy(gameObject, 0.66f);
    }

}