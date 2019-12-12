using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingModel : MonoBehaviour
{
    public SpriteRenderer targetColoredSprite;

    public void ChangeColor(Color color){
        targetColoredSprite.color = color;
    }
}
