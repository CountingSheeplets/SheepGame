using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FadeFx : DeathFx {
    List<Material> materials = new List<Material>();

    void Start() {
        if (materials.Count == 0)materials = GetComponentsInChildren<SpriteRenderer>().Select(x => x.material).ToList();
        StartFading();
    }
    public void StartFading() {
        foreach (Material material in materials) {
            material.SetFloat("_FadeProportion", ConstantsBucket.PlayfieldFadeProportion);
            material.SetVector("_GradientTiling", ConstantsBucket.PlayfieldFadeNoiseTiling);
        }
        StartCoroutine(Fade());
    }
    IEnumerator Fade() {
        float startTime = Time.time;
        float delta = 0;
        float fadeSpeed = 2f / ConstantsBucket.PlayfieldFadeTime;
        while (delta < ConstantsBucket.PlayfieldFadeTime) {
            delta = (Time.time - startTime) * fadeSpeed;
            foreach (Material material in materials)
                material.SetFloat("_FadeLevel", 2f - delta);
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }
}