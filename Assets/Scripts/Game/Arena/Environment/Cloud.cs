using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : BaseUnitMove {
    /*     bool right;
        public void SetDir(bool isDirRight){
            right = isDirRight;
        } */

    public void StartFloating(float speed, Vector2 _destination) {
        //Debug.Log("Startfloating");
        SetDestination(_destination, false);
        MoveToDestination(speed, 0f);
    }

    public override void PostMoveAction() {
        //Debug.Log("cloud float eneded, stopping at:"+(Vector2)(transform.position));
        Destroy(gameObject);
    }

}