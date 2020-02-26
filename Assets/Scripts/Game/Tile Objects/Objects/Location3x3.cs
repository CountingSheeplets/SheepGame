using UnityEngine;
[System.Serializable]
public class Location3x3 : TileLocation {
    public Location3x3(int _x, int _y) : base(_x, _y){}
    public Location3x3(Vector2 vec) : base(vec){}

    bool _isLocal;
    public bool isLocal {
        get{return _isLocal;}
    }
    public Location3x3 GetLocal(){
        if(!_isLocal)
            return new Location3x3(x+1,y+1);
        else return this;
    }
    public Location3x3 ToLocal(){
        if(!_isLocal){
            _x ++;
            _y ++;
            _isLocal = true;
        }
        return this;
    }
    public Location3x3 GetCentral(){
        if(_isLocal){
            return new Location3x3(x-1, y-1);
        } else return this;
    }
    public Location3x3 ToCentral(){
        if(_isLocal){
            _x --;
            _y --;
            _isLocal = false;
        }
        return this;
    }
    public Location3x3(Location2x2 loc2x2, int affTileX, int affTileY) : base(loc2x2.x, loc2x2.y){
        _x += affTileX;
        _y += affTileY;
    }
    public Location2x2 To2x2(Location2x2 loc2x2){
        return new Location2x2(this, loc2x2.x, loc2x2.y).Get();
    }
    public Location2x2 To2x2(int affTileX, int affTileY){
        return new Location2x2(this, affTileX, affTileY).Get();
    }
    public bool Equals(Location3x3 loc3x3){
        return x == loc3x3.x && y== loc3x3.y;
    }
    public Location3x3 Get(){
        if(_x < 0 || _y < 0 || _x > 2 || _y > 2)
            return null;
        else
            return this;
    }
    public bool IsCenterTile(){
        if(isLocal){
            if(x == 1 && y == 1){
                return true;
            }
        } else {
            if(x == 0 && y == 0){
                return true;
            }
        }
        return false;
    }
}