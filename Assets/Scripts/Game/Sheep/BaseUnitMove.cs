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
    //public AnimatorContainer animator;
    public SpineContainer animator;

    public bool isMoving = false;
    public bool impactScale = true;
    void Awake(){
        if(impactScale)
            SetScale();
    }
    public void SetScale(){
        myScale = transform.localScale;
    }
    public void MoveToDestination(float speed, float _midScale){
        if(animator == null)
            animator = GetComponentInChildren<SpineContainer>();
        midScale = _midScale;
        moveSpeed = speed;
        StartCoroutine(Move());
    }

    IEnumerator Move(){
        //stop other enumerators, which are already moving the object
        isMoving = true;
        yield return null;
        isMoving = false;
        //calculate parameters for movement:
        totalDistance = ((Vector2)(transform.position) - destination).magnitude;
        Vector2 initialPos = (Vector2)(transform.position);
        distanceTraveled = ((Vector2)(transform.position) - initialPos).magnitude;
        Vector2 moveDir = (destination - (Vector2)(transform.position)).normalized;
        
        while(distanceTraveled < totalDistance && !isMoving){
            distanceTraveled = ((Vector2)(transform.position) - initialPos).magnitude;
            distanceLeft = ((Vector2)(transform.position) - destination).magnitude;

        //adjust size by distance
            if(impactScale){
                float scaleComponent = midScale * Mathf.Sin(distanceTraveled / totalDistance * Mathf.PI);
                transform.localScale = myScale + myScale * scaleComponent;
            }
        //move transform
            transform.Translate(moveDir * moveSpeed * 0.02f);
            yield return null;
        }
        if(impactScale)
         transform.localScale = myScale;
        transform.position = destination;
        PostMoveAction();
        yield return null;
    }

    public virtual void PostMoveAction(){}
}
