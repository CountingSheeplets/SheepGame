using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlusGold : MonoBehaviour {

    public Color playerColor;
    public TextMeshProUGUI goldText;
    public Image icon;
    public Animator animator;

    public Transform animTransform;

    public void Setup(PlayerProfile profile, float amount) {
        playerColor = profile.playerColor;

        goldText.text = ((int)amount).ToString();
        goldText.color = playerColor;
        icon.color = playerColor;

        animator.SetFloat("seed", Random.Range(0f, 3f));

        transform.position = transform.position + new Vector3(Random.Range(-0.1f, 0.1f), 0, 0);
        StartCoroutine(SleepObject(0.66f));
    }

    IEnumerator SleepObject(float delay) {
        yield return new WaitForSeconds(delay);
        GoldNumbersFactory.DestroyGoldNumber(this.gameObject);
    }

}