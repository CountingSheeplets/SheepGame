using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class Swipe {
    public float relativeDistance;
    public float elementWidth;
    public int angleEuler;
    public Vector2 rawVector;
    public Vector2 normalizedVector;
    public float distance;
    public bool isOverWheelMin {
        get {
            return relativeDistance > ConstantsBucket.SwipeWheelDistMin;
        }
    }

    public Swipe(JToken message) {
        angleEuler = (int)message["rotationEuler"];
        rawVector = new Vector2((float)message["endPointCentered"][0], (float)message["endPointCentered"][1]); //ok?
        normalizedVector = rawVector.normalized;
        elementWidth = (int)message["elementWidth"];
        relativeDistance = Mathf.Clamp(rawVector.magnitude / elementWidth, 0f, 1f);
        distance = relativeDistance * ConstantsBucket.SwipeDistanceMax + ConstantsBucket.SwipeDistanceMin;
    }
    public Swipe() {
        float deltaX = Random.Range(-1f, 1f);
        float deltaY = Random.Range(-1f, 1f);
        normalizedVector = new Vector2(deltaX, deltaY).normalized;
        angleEuler = (int)SignedAngleBetween(Vector2.up, normalizedVector);
        relativeDistance = ConstantsBucket.SwipeWheelDistMin * 2f;
        distance = Random.Range(50f, 80f);
    }
    float SignedAngleBetween(Vector3 a, Vector3 b) {
        Vector3 n = Vector3.forward;
        float angle = Vector3.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));
        float signed_angle = angle * sign;
        return signed_angle;
    }
    public void ToZeroKing() {
        KingUnit king = KingCoordinator.GetKings()[0];
        float deltaX = king.transform.localPosition.x;
        float deltaY = king.transform.localPosition.y;
        normalizedVector = new Vector2(-deltaX, -deltaY).normalized;
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
        distance = new Vector2(deltaX - deltaX1, deltaY - deltaY1).magnitude * 10;
    }
    public override string ToString() {
        return "Swipe(): \n" +
            "distance=" + distance + " \n" +
            "relativeDistance=" + relativeDistance + " \n" +
            "elementWidth=" + elementWidth + " \n" +
            "isOverWheelMin=" + isOverWheelMin + " \n" +
            "angleEuler=" + angleEuler + " \n" +
            "rawVector=" + rawVector + " \n" +
            "normalizedvector=" + normalizedVector;
    }
}