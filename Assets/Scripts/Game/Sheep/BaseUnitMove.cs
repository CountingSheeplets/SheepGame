using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnitMove : MonoBehaviour {
    Vector2 destination;
    public Vector2 parentPosition;
    public Vector2 trueDestination;
    public float totalDistance;
    float moveSpeed;
    public Vector2 initialPos;
    public Vector2 localDestination;
    float distanceTraveled = 0f;
    public Vector3 myScale;
    public float midScale = 2f;
    public float endScale = 0f;
    //public AnimatorContainer animator;
    public IAnimatableDirection animator;

    public bool isMoving = false;
    public bool impactScale = true;
    bool isLocal = true;
    Vector2 localMoveDirection;
    public Vector2 GetLocalMoveDir() { return localMoveDirection; }
    void Awake() {
        if (impactScale)
            SetScale();
    }
    public void SetScale() {
        myScale = transform.localScale;
    }
    public void SetDestination(Vector2 _destination, bool _isLocal) {
        isLocal = _isLocal;
        trueDestination = _destination;
        localMoveDirection = _destination - (Vector2) transform.position;
    }
    public void MoveToDestination(float speed, float _midScale, float _endScale) {
        endScale = _endScale;
        MoveToDestination(speed, _midScale);
    }
    public void MoveToDestination(float speed, float _midScale) {
        if (animator == null)
            animator = GetComponentInChildren<IAnimatableDirection>();
        midScale = _midScale;
        moveSpeed = speed;
        distanceTraveled = 0f;
        if (isLocal) {
            StartCoroutine(MoveLocal());
        } else {
            StartCoroutine(MoveGlobal());
        }
    }
    //perhaps split into GlobalMove and LocalMove, and instead of public property call via appropriate function in inheritor
    IEnumerator MoveGlobal() {
        //stop other enumerators, which are already moving the object
        foreach (BaseUnitMove move in GetComponents<BaseUnitMove>()) {
            move.StopMove();
        }
        yield return new WaitForFixedUpdate();
        isMoving = false;
        //calculate parameters for movement:
        Vector2 initialPos;
        Vector2 moveDir;

        totalDistance = ((Vector2) (transform.position) - trueDestination).magnitude;
        initialPos = (Vector2) (transform.position);
        moveDir = (trueDestination - (Vector2) (transform.position)).normalized;
        while (distanceTraveled < totalDistance && !isMoving) {
            //adjust size by distance
            if (impactScale) {
                if (endScale == 0f) {
                    float scaleComponent = midScale * Mathf.Sin(distanceTraveled / totalDistance * Mathf.PI);
                    transform.localScale = myScale + myScale * scaleComponent;
                } else {
                    float scaleComponent = (1 - endScale) * distanceTraveled / totalDistance;
                    transform.localScale = myScale - myScale * scaleComponent;
                }
            }
            //move transform
            // remake this to use .localPosition if local is tagged
            transform.Translate(moveDir * moveSpeed * 0.02f);
            distanceTraveled += (moveSpeed * 0.02f);
            yield return new WaitForFixedUpdate();
        }
        if (impactScale)
            transform.localScale = myScale;
        if (!isMoving)
            transform.position = trueDestination;
        PostMoveAction();
        isMoving = false;
        yield return new WaitForFixedUpdate();
    }
    IEnumerator MoveLocal() {
        //stop other enumerators, which are already moving the object
        foreach (BaseUnitMove move in GetComponents<BaseUnitMove>()) {
            move.StopMove();
        }
        yield return new WaitForFixedUpdate();
        isMoving = false;
        //calculate parameters for movement:
        initialPos = (Vector2) (transform.localPosition);
        parentPosition = (Vector2) transform.parent.transform.position;
        localDestination = trueDestination - (Vector2) transform.parent.transform.position;
        float progress = 0f;
        float distance = (initialPos - localDestination).magnitude;
        while (progress < 1f && !isMoving) {
            //adjust size by distance
            if (impactScale) {
                if (endScale == 0f) {
                    float scaleComponent = midScale * Mathf.Sin(progress * Mathf.PI);
                    transform.localScale = myScale + myScale * scaleComponent;
                } else {
                    float scaleComponent = (1 - endScale) * progress;
                    transform.localScale = myScale - myScale * scaleComponent;
                }
            }
            //move transform
            transform.localPosition = Vector3.Lerp(initialPos, localDestination, progress);
            progress += Time.deltaTime * moveSpeed / distance;
            yield return new WaitForFixedUpdate();
        }
        if (impactScale)
            transform.localScale = myScale;
        if (!isMoving)
            transform.localPosition = localDestination;

        PostMoveAction();
        isMoving = false;
        yield return new WaitForFixedUpdate();
    }
    public void StopMove() {
        isMoving = true;
    }
    public virtual void PostMoveAction() { }
}