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
    public Location2x2 To2x2(int affTileX, int affTileY){
        return new Location2x2(this, affTileX, affTileY);
    }
    //this is not working, to avoid confusion
    //move this function to:
    //subtile.isAffectedBy(vec3x3)
    public bool[,] GetAffectedSubTiles(){ //input 3x3 loc
        bool[,] output = new bool[2,2];
        bool[] i = new bool[2];
        bool[] j = new bool[2];
        switch(_x){
            case 0:
                i[0] = true;
                break;
            case 1:
                i[0] = true;
                i[1] = true;
                break;
            case 2:
                i[1] = true;
                break;
        }
        switch(_y){
            case 0:
                j[0] = true;
                break;
            case 1:
                j[0] = true;
                j[1] = true;
                break;
            case 2:
                j[1] = true;
                break;
        }
        for(int k = 0; k < 2; k++){
            for(int l = 0; l < 2; l++){
                output[l,k] = i[l] & j[k];
                Debug.Log(l+":L/K:"+k+ " affected setup: "+output[l,k]+" i/j:"+i[l]+" "+j[k]);
            }
        }
        return output;
    }
}