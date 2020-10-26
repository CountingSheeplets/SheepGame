using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : BaseUnitMove {
    Vector2 destination;
    float speed;

    void Start() {
        SetDestination(destination, true);
        MoveToDestination(speed, 0f);
    }
    public void StartFloating(float _speed, Vector2 _destination) {
        destination = _destination;
        speed = _speed;
    }

    public override void PostMoveAction() {
        //Debug.Log("cloud float eneded, stopping at:"+(Vector2)(transform.position));
        Destroy(gameObject);
    }

}