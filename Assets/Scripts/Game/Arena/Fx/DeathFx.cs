using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFx : MonoBehaviour
{
    public Color mainColor = Color.white;
    public void SetColor(Color color){
        mainColor = new Color(color.r, color.g, color.b, color.a);
    }
}
