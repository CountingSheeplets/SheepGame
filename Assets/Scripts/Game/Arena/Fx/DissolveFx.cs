using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DissolveFx : DeathFx
{

    List<Material> materials = new List<Material>();

    void Start()
    {
        if(materials.Count == 0) materials = GetComponentsInChildren<SpriteRenderer>().Select(x => x.material).ToList();
        StartCoroutine(Disolve());
    }

    IEnumerator Disolve()
    {
        foreach(Material material in materials){
            material.SetColor("_DissolveColor", mainColor);
            //Debug.Log("color:"+mainColor);
        }

        float disolveAmount = 0;
        float startTime = Time.time;
        float disolveSpeed = 1f / ConstantsBucket.PlayfieldFadeTime;
        while(disolveAmount <= 1){
            disolveAmount = (Time.time - startTime) * disolveSpeed;
            foreach(Material material in materials)
                material.SetFloat("_DissolveAmount", disolveAmount);
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }
}
