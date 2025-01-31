using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInputDemo : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public void GrassRegrow() {

    }

    public void OnTargetMove(InputValue value) {
        // The given InputValue is only valid for the duration of the callback. Storing the InputValue references somewhere and calling Get<T>() later does not work correctly.
        var v = value.Get<Vector2>();


    }

    public void OnShoot() {
        Debug.Log("Shoot");
    }
}
