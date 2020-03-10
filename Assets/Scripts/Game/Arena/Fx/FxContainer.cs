using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxContainer : Singleton<FxContainer> {
    public static Transform GetTransform() {
        return Instance.transform;
    }
}