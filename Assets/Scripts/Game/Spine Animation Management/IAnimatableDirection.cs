using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimatableDirection {
    void WalkTo(Vector2 target);
    void StopWalking();
}