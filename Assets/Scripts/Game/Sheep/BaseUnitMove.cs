﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnitMove : MonoBehaviour {
    Vector2 destination;
    public float totalDistance;
    float moveSpeed;
    public float distanceLeft;
    public float distanceTraveled;
    public Vector3 myScale;
    public float midScale = 2f;
    //public AnimatorContainer animator;
    public IAnimatableDirection animator;

    public bool isMoving = false;
    public bool impactScale = true;
    bool isLocal = true;
    void Awake() {
        if (impactScale)
            SetScale();
    }
    public void SetScale() {
        myScale = transform.localScale;
    }
    public void SetDestination(Vector2 _destination, bool _isLocal) {
        isLocal = _isLocal;
        if (isLocal) {
            destination = _destination - (Vector2) transform.parent.transform.position;
        } else {
            destination = _destination;
        }
    }
    public void MoveToDestination(float speed, float _midScale) {
        if (animator == null)
            animator = GetComponentInChildren<IAnimatableDirection>();
        midScale = _midScale;
        moveSpeed = speed;
        StartCoroutine(Move());
    }
    //perhaps split into GlobalMove and LocalMove, and instead of public property call via appropriate function in inheritor
    IEnumerator Move() {
        //stop other enumerators, which are already moving the object
        isMoving = true;
        yield return null;
        isMoving = false;
        //calculate parameters for movement:
        Vector2 initialPos;
        Vector2 moveDir;
        if (isLocal) {
            totalDistance = ((Vector2) (transform.localPosition) - destination).magnitude;
            initialPos = (Vector2) (transform.localPosition);
            distanceTraveled = ((Vector2) (transform.localPosition) - initialPos).magnitude;
            moveDir = (destination - (Vector2) (transform.localPosition)).normalized;
        } else {
            totalDistance = ((Vector2) (transform.position) - destination).magnitude;
            initialPos = (Vector2) (transform.position);
            distanceTraveled = ((Vector2) (transform.position) - initialPos).magnitude;
            moveDir = (destination - (Vector2) (transform.position)).normalized;
        }
        while (distanceTraveled < totalDistance && !isMoving) {
            if (isLocal) {
                distanceTraveled = ((Vector2) (transform.localPosition) - initialPos).magnitude;
                distanceLeft = ((Vector2) (transform.localPosition) - destination).magnitude;
            } else {
                distanceTraveled = ((Vector2) (transform.position) - initialPos).magnitude;
                distanceLeft = ((Vector2) (transform.position) - destination).magnitude;
            }
            //adjust size by distance
            if (impactScale) {
                float scaleComponent = midScale * Mathf.Sin(distanceTraveled / totalDistance * Mathf.PI);
                transform.localScale = myScale + myScale * scaleComponent;
            }
            //move transform
            transform.Translate(moveDir * moveSpeed * 0.02f);
            yield return null;
        }
        if (impactScale)
            transform.localScale = myScale;
        if (!isMoving)
            if (isLocal)
                transform.localPosition = destination;
            else
                transform.position = destination;
        PostMoveAction();
        yield return null;
    }

    public virtual void PostMoveAction() { }
}