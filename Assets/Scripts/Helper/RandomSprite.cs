using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public SpriteRenderer rend;
    public List<Sprite> sprites;

    void Start()
    {
        if(rend == null)    rend = GetComponentInChildren<SpriteRenderer>();
        SelectSprite();
    }

    void SelectSprite()
    {
        if(sprites.Count<=0){
            Debug.LogWarning("Zero Sprites to select a random from, not randoming any sprite...");
            return;
        }
        int index = Random.Range(0, sprites.Count-1);
        rend.sprite = sprites[index];
    }
}
