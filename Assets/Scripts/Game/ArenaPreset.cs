using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaPreset : MonoBehaviour
{
    public List<PresetSocket> sockets;
    public int presetSize {
        get {
            sockets = new List<PresetSocket>(GetComponentsInChildren<PresetSocket>());
            return sockets.Count;
        }
    }
    
}
