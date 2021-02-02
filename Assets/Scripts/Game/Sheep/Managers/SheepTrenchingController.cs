using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepTrenchingController : MonoBehaviour {
    SheepUnit sheep;
    public GameObject blob;
    public GameObject front;
    public GameObject model;
    bool colorIsSet = false;
    void Start() {
        if (sheep == null)sheep = GetComponentInParent<SheepUnit>();
        sheep.onIsTrenchingChange += OnTrenchingChange;
    }

    void OnTrenchingChange(bool state) {
        if (model == null)model = transform.parent.GetComponentInChildren<SheepModel>().gameObject;
        TrySetColor();
        model.SetActive(!state);
        front.SetActive(state);
        blob.SetActive(state);

    }
    void TrySetColor() {
        if (colorIsSet)
            return;
        if (model != null && blob != null) {
            blob.GetComponent<SpriteRenderer>().color = sheep.owner.GetPlayerProfile().playerColor;
            colorIsSet = true;
        }
    }
}