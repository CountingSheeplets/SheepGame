using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadButton : MonoBehaviour
{
    public void OnDownloadClick() {
        Debug.Log("Download button clicked");
        Application.OpenURL("https://cyberbakers.eu/sheepdom/demo/sheepdom.zip");
    }   
}
