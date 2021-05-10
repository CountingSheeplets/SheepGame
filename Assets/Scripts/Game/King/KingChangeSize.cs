using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingChangeSize : MonoBehaviour {
    public Vector3 defaultScale;
    public Vector3 targetScale;

    void Start() {
        defaultScale = transform.localScale;
    }
    public void StartIncreasingSize(int _steps) {
        targetScale = defaultScale * Mathf.Pow(1 + ConstantsBucket.KingHitRadiusIncrement, _steps);
        if (GetComponent<BaseUnitMove>())
            GetComponent<BaseUnitMove>().SetScale();
        StartCoroutine(IncreaseSize());
    }
    public void StartResetingSize() {
        targetScale = defaultScale;
        if (GetComponent<BaseUnitMove>())
            GetComponent<BaseUnitMove>().SetScale();
        StartCoroutine(IncreaseSize());
    }
    IEnumerator IncreaseSize() {
        Vector3 myScale = transform.localScale;
        float startTime = Time.time;
        float timePassed = 0f;
        while (timePassed < ConstantsBucket.KingScaleChangeTime) {

            timePassed = Time.time - startTime;
            float k = timePassed / ConstantsBucket.KingScaleChangeTime;

            float easedK = Easing.Elastic.InOut(k);
            transform.localScale = Vector3.Lerp(myScale, targetScale, easedK);

            yield return null;
        }
        transform.localScale = targetScale;
        PostScaleChangeAction();
        yield return null;
    }

    public virtual void PostScaleChangeAction() {}
}