using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSFixedUpdate : MonoBehaviour {
    float counter;
    float frames;
    public TextMeshProUGUI fps;
    private void FixedUpdate() {
        counter += Time.deltaTime;
        if (counter > 1) {
            counter -= 1f;
            fps.text = frames.ToString();
            frames = 0;
        }
        frames += 1;
    }
}