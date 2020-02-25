using UnityEngine;
[System.Serializable]
public class TileLocation {
    [SerializeField]
    protected int _x;
    public int x {
        get{return _x;}
    }
    [SerializeField]
    protected int _y;
    public int y {
        get{return _y;}
    }

    public TileLocation(int xInput, int yInput){
        _x = xInput;
        _y = yInput;
    }
    public TileLocation(Vector2 vec2){
        _x = (int)vec2.x;
        _y = (int)vec2.y;
    }
}