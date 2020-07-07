using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialProperty : MonoBehaviour {
    bool trigger;
    float blendSpeed = 0.5f;
    float visibility = 1f;
    SpriteRenderer rend;

    void Start() {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.A)) {
            Debug.Log("Start ChangeMaterialProperty");
            trigger = true;
            visibility = 1f;
            rend.material.SetFloat("_CurrentLevel", 1);
        }
        if (trigger) {
            visibility -= Time.deltaTime * blendSpeed;
            if (visibility >= 0)
                rend.material.SetFloat("_CurrentLevel", visibility);
            else {
                rend.material.SetFloat("_CurrentLevel", 0);
                trigger = false;
            }
        }
    }
}