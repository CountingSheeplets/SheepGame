using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimatableDirection {
    void WalkTo(Vector2 target);
    void FlyTo(Vector2 target);
    void Die();
    void Attack();
    void StopWalking();
    void StopFlying();
}