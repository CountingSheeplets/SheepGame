using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextSideScroll : MonoBehaviour
{
    TextMeshProUGUI text;
    int counter = 0;
    int skip = 5;
    void Start()
    {
        if(text == null) text = GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        counter++;
        if(counter >5){
            string original = text.text;
            text.text = original.Remove(0,1) + original[0];
            counter =0;
        }
    }
}
