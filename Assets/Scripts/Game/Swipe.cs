using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class Swipe
{
    public float distance;
    public float angle;
    public int degree;
    public int speed;
    public Vector2 vector;

    public Swipe(JToken message)
    {
        distance = (float)message["distance"];
        angle = (float)message["angle"];
        degree = (int)message["degree"];
        speed = (int)message["speed"];
        vector = new Vector2((float)message["x"], -(float)message["y"]);
    }
    public override string ToString(){
        return "Swipe():"+
        "distance="+distance+" "+
        "angle="+angle+" "+
        "degree="+degree+" "+
        "speed="+speed+" "+
        "vector="+vector;
    }
}
