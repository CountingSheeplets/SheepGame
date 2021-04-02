using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepUnit : DestructableUnit {
    public Owner owner;
    public Owner lastHandler;
    public bool bounced;
    public bool canBeThrown {
        get {
            //Debug.Log("isReadying:" + isReadying + "  isReadyToFly:" + isReadyToFly + "   isFlying:" + isFlying + "   " + isSwimming);
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
    public SheepType sheepType = SheepType.Base;
    bool _isTrenching = false;
    public delegate void OnStateChange(bool state);
    public OnStateChange onIsTrenchingChange;
    public bool isTrenching {
        get { return _isTrenching; }
        set {
            if (_isTrenching == value)
                return;
            _isTrenching = value;
            if (onIsTrenchingChange != null)
                onIsTrenchingChange(_isTrenching);
            if (_isTrenching)
                foreach (BaseUnitMove move in GetComponents<BaseUnitMove>()) {
                    move.StopMove();
                }
        }
    }
    private void OnDestroy() {
        Debug.Log("sheep destroyed");
    }
    public void ResetContainer() {
        if (isFlying || isSwimming) {
            transform.parent = SheepCoordinator.Instance.transform;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        } else
        if (currentPlayfield != null) {
            transform.parent = currentPlayfield.sheepParent;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        }
    }
}