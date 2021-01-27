using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorCoordinator : Singleton<PlayerColorCoordinator> {
    public bool[] isUsed;

    void Awake() {
        isUsed = new bool[ConstantsBucket.PlayerColors.Count];
    }

    public static Color UseFirstUnused() {
        if (Instance.isUsed == null)
            Instance.isUsed = new bool[ConstantsBucket.PlayerColors.Count];

        for (int i = 0; i < ConstantsBucket.PlayerColors.Count; i++) {
            if (Instance.isUsed[i] == false) {
                Instance.isUsed[i] = true;
                return ConstantsBucket.PlayerColors[i];
            }
        }
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public static void UnUse(Color color) {
        for (int i = 0; i < ConstantsBucket.PlayerColors.Count; i++) {
            if (ConstantsBucket.PlayerColors[i] == color) {
                Instance.isUsed[i] = false;
            }
        }
    }

}