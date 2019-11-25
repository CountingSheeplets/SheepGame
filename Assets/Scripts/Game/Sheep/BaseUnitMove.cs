using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnitMove : MonoBehaviour
{
    public Vector2 destination;
    public float totalDistance;
    float moveSpeed;
    public float distanceLeft;
    public float distanceTraveled;
    public Vector3 myScale;
    public float midScale = 2f;
    public AnimatorContainer animator;

    public void MoveToDestination(float speed, float _midScale){
        if(animator == null)
            animator = GetComponentInChildren<AnimatorContainer>();
        midScale = _midScale;
        moveSpeed = speed;
        myScale = transform.localScale;
        StartCoroutine(Move());
    }

    IEnumerator Move(){
        //calculate parameters for movement:
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
            transform.Translate(moveDir * moveSpeed * 0.02f);
            yield return null;
        }
        transform.localScale = myScale;
        PostMoveAction();
        yield return null;
    }

    public virtual void PostMoveAction(){}
}
