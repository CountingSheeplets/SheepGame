using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingHeadbutStars : MonoBehaviour {
    KingUnit king;
    public GameObject stars;
    float counter;
    bool hit;
    void Start() {
        king = GetComponentInParent<KingUnit>();
        stars.SetActive(false);
        EventCoordinator.StartListening(EventName.System.King.Hit(), OnHit);
    }
    private void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.Hit(), OnHit);
    }
    void OnHit(GameMessage msg) {
        if (msg.kingUnit == king) {
            hit = true;
            stars.SetActive(true);
        }
    }
    void Update() {
        if (hit)
            if (counter < ConstantsBucket.HeadbutStarsTimer)
                counter += Time.deltaTime;
            else {
                counter = 0;
                hit = false;
                stars.SetActive(false);
            }
    }
}