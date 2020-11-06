using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class Swipe {
    public float distance;
    public int angleEuler;
    public Vector2 rawVector;
    public Vector2 normalizedVector;
    //public Vector2 initial;

    public Swipe(JToken message) {
        //prototype generator by airconsole:
        /*         distance = (float)message["distance"];
                angle = (float)message["angle"];
                degree = (int)message["degree"];
                speed = (int)message["speed"];
                vector = new Vector2((float)message["x"], -(float)message["y"]); */
        //React-Swipe:
        angleEuler = (int)message["rotationEuler"];
        //vector = new Vector2(-(float)message["deltaX"], (float)message["deltaY"]).normalized; //ok?
        rawVector = new Vector2((float)message["endPointCentered"][0], (float)message["endPointCentered"][1]); //ok?
        normalizedVector = rawVector.normalized; //ok?
        //initial = new Vector2(-(float)message["initial"][0], (float)message["initial"][1]); //ok? 
        distance = 0.75f * rawVector.magnitude;
    }
    public Swipe() {
        //speed = Random.Range(10, 32); //ok?
        float deltaX = Random.Range(-1f, 1f);
        float deltaY = Random.Range(-1f, 1f);
        normalizedVector = new Vector2(deltaX, deltaY).normalized; //ok?
        //initial = new Vector2(-(float)Random.Range(0f, 100f), (float)Random.Range(0f, 100f)); //ok? 
        distance = Random.Range(50f, 80f);
    }
    public void ToZeroKing() {
        KingUnit king = KingCoordinator.GetKings()[0];
        //speed = Random.Range(10, 32); //ok?
        float deltaX = king.transform.localPosition.x;
        float deltaY = king.transform.localPosition.y;
        normalizedVector = new Vector2(-deltaX, -deltaY).normalized;
        //initial = new Vector2(0, 0);
        distance = king.transform.localPosition.magnitude * 80f;
    }
    public void ToZeroPlayfield(Owner originOwner) {
        Owner owner = OwnersCoordinator.GetOwners()[0];
        Owner owner1 = originOwner;
        Playfield playfield = owner.GetPlayfield();
        Playfield playfield1 = owner1.GetPlayfield();
        float deltaX = playfield.transform.position.x;
        float deltaX1 = playfield1.transform.position.x;
        float deltaY = playfield.transform.position.y;
        float deltaY1 = playfield1.transform.position.y;

        normalizedVector = new Vector2(deltaX - deltaX1, deltaY - deltaY1).normalized;
        //initial = new Vector2(deltaX1, deltaY1);
        distance = new Vector2(deltaX - deltaX1, deltaY - deltaY1).magnitude * 10;
    }
    public override string ToString() {
        return "Swipe(): \n" +
            "distance=" + distance + " \n" +
            "angleEuler=" + angleEuler + " \n" +
            "rawVector=" + rawVector + " \n" +
            "normalizedvector=" + normalizedVector;
    }
}