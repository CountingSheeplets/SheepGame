using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepFly : MonoBehaviour
{
    public Vector2 destination;
    public float totalDistance;
    float flySpeed;
    public float distanceLeft;
    public float distanceTraveled;

    public void StartFlying(float speed){
        flySpeed = speed;
        StartCoroutine(Fly());
    }

    IEnumerator Fly(){
        //run animation;

        //calculate parameters for movement:
        Vector3 myScale = transform.localScale;
        float midScale = 2f;
        totalDistance = ((Vector2)(transform.position) - destination).magnitude;
        Vector2 initialPos = (Vector2)(transform.position);
        distanceTraveled = ((Vector2)(transform.position) - initialPos).magnitude;
        Vector2 moveDir = (destination - (Vector2)(transform.position)).normalized;
        
        while(distanceTraveled < totalDistance){
            distanceTraveled = ((Vector2)(transform.position) - initialPos).magnitude;
            distanceLeft = ((Vector2)(transform.position) - destination).magnitude;

        //adjust size by distance
            float scaleComponent = midScale * Mathf.Sin(distanceTraveled / totalDistance * Mathf.PI);
            transform.localScale = myScale + myScale * scaleComponent;
        //move transform
            transform.Translate(moveDir * flySpeed * 0.02f);
            yield return null;
        }
        transform.localScale = myScale;
        //trigger to play Land animation
            Debug.Log("fly eneded, landing at:"+(Vector2)(transform.position));

        //trigger Land game event
        EventManager.TriggerEvent(EventName.System.Sheep.Land(), GameMessage.Write().WithSheepUnit(GetComponent<SheepUnit>()).WithPlayfield(ArenaManager.GetPlayfieldForPoint(transform.position)));
        //this is listened by Sheep Throw!
        yield return null;
    }
}
