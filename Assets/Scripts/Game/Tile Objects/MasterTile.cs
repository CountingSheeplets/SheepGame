using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterTile : MonoBehaviour {
    public Texture2D texture;
    int height = 128;
    int width = 128;
    Sprite sprite;
    int offset;
    void Start() {
        //mySprite.sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f));
        offset = (ConstantsBucket.GridSize - 1) / 2;
        sprite = Sprite.Create(
            new Texture2D(width * ConstantsBucket.GridSize, height * ConstantsBucket.GridSize),
            new Rect(0, 0, width * ConstantsBucket.GridSize, height * ConstantsBucket.GridSize),
            new Vector2(0.5f, 0.5f),
            1024);
        GetComponent<SpriteRenderer>().sprite = sprite;
        texture = sprite.texture;
    }

    public void UpdateTexture(int x, int y, Color[] pixels) {
        Debug.Log("x*w: " + (x + offset) * width + " y*h: " + (y + offset) * height);
        sprite.texture.SetPixels((offset + x) * width, (y + offset) * height, height, width, pixels);
    }

    public void ApplyTexture() {
        sprite.texture.Apply();
        //sprite.texture.SetPixels = texture;
    }
}