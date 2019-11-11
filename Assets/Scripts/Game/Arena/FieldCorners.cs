using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCorners
{
    private Vector2 _topLeft;
    public Vector2 TopLeft{get{return _topLeft;}}
    private Vector2 _topRight;
    public Vector2 TopRight{get{return _topRight;}}
    private Vector2 _botLeft;
    public Vector2 BottomLeft{get{return _botLeft;}}
    private Vector2 _botRight;
    public Vector2 BottomRight{get{return _botRight;}}

    private Vector2 _center;
    public Vector2 Center{get{return _center;}}

    public FieldCorners(Vector2 size){
        _center = Vector2.zero;
        _topLeft = new Vector2(-size.x/2, size.y/2);
        _topRight = new Vector2(size.x/2, size.y/2);
        _botLeft = new Vector2(-size.x/2, -size.y/2);
        _botRight = new Vector2(size.x/2, -size.y/2);
    }
    public void Recenter(Vector2 newCenter){
        _topLeft += newCenter - _center;
        _topRight += newCenter - _center;
        _botLeft  += newCenter - _center;
        _botRight += newCenter - _center;
        _center = newCenter;
    }

    public bool IsWithinField(Vector2 point){
        Debug.Log("checking point: "+point+ " vs me: "+this);
        if(point.x > TopLeft.x && point.y < TopLeft.y &&
            point.x < TopRight.x && point.y < TopRight.y &&
            point.x > BottomLeft.x && point.y > BottomLeft.y &&
            point.x < BottomRight.x && point.y > BottomRight.y)
            return true;
        return false;
    }
    public bool IsWithinField(Vector2 point, float radius){
        Debug.Log("checking point: "+point+ " vs me: "+this);
        if(point.x > TopLeft.x + radius && point.y < TopLeft.y - radius &&
            point.x < TopRight.x - radius && point.y < TopRight.y - radius &&
            point.x > BottomLeft.x + radius && point.y > BottomLeft.y + radius &&
            point.x < BottomRight.x - radius && point.y > BottomRight.y + radius)
            return true;
        return false;
    }
    public override string ToString() {
        return
        " TopLeft:"+TopLeft+
        " TopRight:"+TopRight+
        " BottomLeft:"+BottomLeft+
        " BottomRight:"+BottomRight+
        " Center:"+Center;
    }
}
