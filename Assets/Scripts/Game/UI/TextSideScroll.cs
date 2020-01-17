using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextSideScroll : MonoBehaviour
{
    TextMeshProUGUI text;
    int counter = 0;
    int skip = 5;
    bool scroll = true;
    RectTransform myRectTr;
    public float scrollSpeed = 1f;
    void Start()
    {
        if(text == null) text = GetComponent<TextMeshProUGUI>();
        if(myRectTr == null) myRectTr = GetComponent<RectTransform>();
        float height = myRectTr.rect.height;
        Debug.Log(height);
        //myRectTr.sizeDelta = new Vector2(Screen.width, height);
        myRectTr.offsetMin = new Vector2(-Screen.width/2f, 0);
        myRectTr.offsetMax = new Vector2(Screen.width/2f, 0);
        Debug.Log(myRectTr.rect);

        StartCoroutine(Scroll());
    }

    IEnumerator Scroll()
    {
        while(scroll){
            transform.localPosition -= new Vector3(scrollSpeed, 0, 0);

            if(transform.localPosition.x <= -Screen.width)
                transform.localPosition = new Vector3(Screen.width, transform.localPosition.y, transform.localPosition.z);

            yield return 0;
        }
        yield return 0;
    }
    void OnDestroy(){scroll = false;}
}
