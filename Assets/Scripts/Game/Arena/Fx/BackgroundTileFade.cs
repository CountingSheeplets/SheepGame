using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTileFade : MonoBehaviour
{
    SpriteRenderer rend;
    bool isClone = false;

    void OnDestroy(){
        if (isClone || ArenaCoordinator.Instance == null)
            return;
        GameObject newTempObj = Instantiate(gameObject, ArenaCoordinator.Instance.transform);
        newTempObj.name = "tempDeathFx of bkg tile";
        newTempObj.transform.position = transform.position;
        newTempObj.transform.localScale = new Vector3( transform.localScale.x, transform.localScale.y, 1);
        newTempObj.GetComponent<BackgroundTileFade>().StartFading();
    }
    public void StartFading(){
        if(rend == null) rend = GetComponent<SpriteRenderer>();
        rend.material.SetFloat("_FadeProportion", ConstantsBucket.PlayfieldFadeProportion);
        rend.material.SetVector("_GradientTiling", ConstantsBucket.PlayfieldFadeNoiseTiling);
        isClone = true;
        StartCoroutine(Fade());
    }
    IEnumerator Fade(){
        float startTime = Time.time;
        float delta = 0;
        float fadeSpeed = 2f/ ConstantsBucket.PlayfieldFadeTime;
        while(delta < ConstantsBucket.PlayfieldFadeTime){
            delta = (Time.time - startTime) * fadeSpeed;
            rend.material.SetFloat("_FadeLevel", 2f-delta);
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }
}
