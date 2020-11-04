using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSheep : MonoBehaviour {
    float RotateSpeed = 0.2f;
    float _angle;
    Vector2 _centre;
    float radius = 35f;
    float width = 10f;
    float startingAngle;

    public int myIndex;
    public SpineContainerBlendsEight blendsEight;
    public Animator animator;
    float screenWidth;
    void Start() {
        animator = GetComponent<Animator>();
        _centre = Vector2.zero;
        screenWidth = this.gameObject.GetComponentInParent<RectTransform>().rect.width;
    }
    public void Setup(int i) {
        myIndex = i;
        startingAngle = (float)myIndex / 8 * Mathf.PI * 2;
        blendsEight = GetComponent<SpineContainerBlendsEight>();
        blendsEight.WalkTo(GetPosition(startingAngle));
    }

    void Update() {
        _angle += RotateSpeed * Time.deltaTime;

        var offset = GetPosition(startingAngle + _angle);
        Vector2 nextOffset = GetPosition(startingAngle + _angle + 1.5f);
        transform.localPosition = _centre + offset;
        Vector2 newDir = blendsEight.EnumToAnimVec(blendsEight.GetAnimEnum(nextOffset));
        //if (myIndex == 0)
        //    Debug.Log("dif: " + blendsEight.GetAnimEnum(nextOffset) + "   newdir: " + nextOffset);
        animator.SetFloat("dirX_blend", (newDir).x);
        animator.SetFloat("dirY_blend", (newDir).y);
    }

    Vector2 GetPosition(float angle) {
        return new Vector2(Mathf.Sin(angle) * width, Mathf.Cos(angle)) * radius * screenWidth / 1920f;
    }

    Vector3 PosInCircle(int index) {
        Vector3 center = Vector3.zero;
        float ang = (float)index / 8;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad) * 2f;
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}