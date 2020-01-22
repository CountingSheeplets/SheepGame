using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFx : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color mainColor = Color.white;
    public void SetColorHDR(Color color, float glowIntensity){
        mainColor = new Color(color.r * glowIntensity, color.g * glowIntensity, color.b * glowIntensity, color.a);
    }
}
