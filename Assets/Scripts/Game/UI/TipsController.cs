using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TipsController : MonoBehaviour
{
    TextMeshProUGUI tipText;
    float counter;
    void Start()
    {
        tipText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        counter += Time.deltaTime;
        if(counter > ConstantsBucket.TipLoopTimer){
            counter = 0;
            tipText.text = "Tip: "+TipsBucket.GetNextTip();
        }
    }
}
