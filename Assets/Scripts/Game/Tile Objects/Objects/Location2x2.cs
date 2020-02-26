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
    public Location2x2 Get(){
        if(_x < 0 || _y < 0 || _x > 1 || _y > 1)
            return null;
        else
            return this;
    }
    public Location3x3 To3x3(int affTileX, int affTileY){
        return new Location3x3(this, affTileX, affTileY).Get();
    }

    public bool Equals(Location2x2 loc2x2){
        return x == loc2x2.x && y== loc2x2.y;
    }
    public Location2x2 Inverted(){
        return new Location2x2(!(this.x != 0) ? 1 : 0, !(this.y != 0) ? 1 : 0);
    }
}