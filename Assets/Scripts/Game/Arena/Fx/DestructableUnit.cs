using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableUnit : MonoBehaviour {
    public delegate void OnSheepDestroy();
    public event OnSheepDestroy sheepDestroyCallback;
    public void TrigSheepDestroy() {
        sheepDestroyCallback();
    }
}