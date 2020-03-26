using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraSortMode : MonoBehaviour {
    void Start() {
        Camera cam = Camera.main;
        cam.transparencySortAxis = Vector3.up;
        cam.transparencySortMode = TransparencySortMode.CustomAxis;
    }
}