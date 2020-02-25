using UnityEngine;
[System.Serializable]
public class Location2x2 : TileLocation {
    public Location2x2(int _x, int _y) : base(_x, _y){}
    public Location2x2(Vector2 vec) : base(vec){}
    public Location2x2(Location2x2 loc) : base(loc.x, loc.y){}

    public Location2x2(Location3x3 loc3x3, int affTileX, int affTileY) : base(loc3x3.x, loc3x3.y){
        _x -= affTileX;
        _y -= affTileY;
    }
    public Location3x3 To3x3(int affTileX, int affTileY){
        return new Location3x3(this, affTileX, affTileY);
    }
}