using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class Swipe
{
    public float distance;
    public float angle;
    public int degree;
    public float speed;
    public Vector2 vector;
    public Vector2 initial;

    public Swipe(JToken message)
    {
        //prototype generator by airconsole:
/*         distance = (float)message["distance"];
        angle = (float)message["angle"];
        degree = (int)message["degree"];
        speed = (int)message["speed"];
        vector = new Vector2((float)message["x"], -(float)message["y"]); */
        //React-Swipe:
        //angle = (float)message["angle"];
        //degree = (int)message["degree"];
        speed = (float)message["velocity"]; //ok?
        vector = new Vector2(-(float)message["deltaX"], (float)message["deltaY"]).normalized;//ok?
        initial = new Vector2(-(float)message["initial"][0], (float)message["initial"][1]);//ok? 
        distance = 0.2f * (float)new Vector2((float)message["deltaX"], (float)message["deltaY"]).magnitude;
    }
    public Swipe(){
        speed = Random.Range(100, 321); //ok?
        float deltaX = Random.Range(-100f, 100f);
        float deltaY = Random.Range(-100f, 100f);
        vector = new Vector2(deltaX, deltaY).normalized;//ok?
        initial = new Vector2(-(float)Random.Range(0f, 100f), (float)Random.Range(0f, 100f));//ok? 
        distance = (float)new Vector2(deltaX, deltaY).magnitude;
    }
    public override string ToString(){
        return "Swipe():"+
        "distance="+distance+" "+
/*         "angle="+angle+" "+ */
        "initial="+initial+" "+
        "speed="+speed+" "+
        "vector="+vector;
    }
}
