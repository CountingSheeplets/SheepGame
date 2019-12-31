using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextSideScroll : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
        if(text == null) text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        string original = text.text;
        text.text = original.Remove(0,1) + original[0];
    }
}
