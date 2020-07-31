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
        GetComponent<BaseUnitMove>().SetScale();
        StartCoroutine(IncreaseSize());
    }
    public void StartResetingSize() {
        targetScale = defaultScale;
        GetComponent<BaseUnitMove>().SetScale();
        StartCoroutine(IncreaseSize());
    }
    IEnumerator IncreaseSize() {
        //Debug.Log("IncreaseSize");
        Vector3 myScale = transform.localScale;
        float startTime = Time.time;
        float timePassed = 0f;
        while (timePassed < ConstantsBucket.KingScaleChangeTime) {

            timePassed = Time.time - startTime;
            float k = timePassed / ConstantsBucket.KingScaleChangeTime;

            float easedK = Easing.Elastic.InOut(k);
            //Debug.Log(k+"  "+easedK);
            transform.localScale = Vector3.Lerp(myScale, targetScale, easedK);

            yield return null;
        }
        transform.localScale = targetScale;
        PostScaleChangeAction();
        //Debug.Log("IncreaseSize done!");
        yield return null;
    }

    public virtual void PostScaleChangeAction() { }
}