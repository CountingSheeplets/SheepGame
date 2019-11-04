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

    public Swipe(JToken message){
		distance = (float)message ["direction"]["distance"];
		angle = (float)message ["direction"]["angle"];
		degree = (int)message ["direction"]["degree"];
		speed = (int)message ["direction"]["speed"];
		vector = new Vector2 ((float)message ["data"]["x"], -(float)message ["data"]["y"]);
    }
}
