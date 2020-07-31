using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepUnit : MonoBehaviour {
    public Owner owner;
    public Owner lastHandler;
    public bool canBeThrown {
        get {
            Debug.Log("isReadying:" + isReadying + "  isReadyToFly:" + isReadyToFly + "   isFlying:" + isFlying + "   " + isSwimming);
            return (!isReadying && !isReadyToFly && !isFlying && !isSwimming);
        }
    }
    public Playfield currentPlayfield;
    public bool isReadying = false;
    public bool isReadyToFly = false;
    public bool isFlying = false;
    public bool isSwimming = false;
    public bool isRoaming = false;
    public float radius = 0.5f;
    //[BitMask(typeof(SheepType))]
    public SheepType sheepType;
    bool _skippedByTrenching = false;
    public delegate void OnStateChange(bool state);
    public OnStateChange onIsTrenchingChange;
    public bool skippedByTrenching {
        get { return _skippedByTrenching; }
        set {
            _skippedByTrenching = value;
            if (onIsTrenchingChange != null)
                onIsTrenchingChange(_skippedByTrenching);
            if (_skippedByTrenching)
                foreach (BaseUnitMove move in GetComponents<BaseUnitMove>()) {
                    move.StopMove();
                }
        }
    }

    public void ResetContainer() {
        if (isFlying || isSwimming)
            transform.parent = SheepCoordinator.Instance.transform;
        else
        if (currentPlayfield != null)
            transform.parent = currentPlayfield.sheepParent;
    }
}