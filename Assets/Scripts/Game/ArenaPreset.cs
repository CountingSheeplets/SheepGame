using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArenaPreset : MonoBehaviour
{
    public List<PresetSocket> playfieldSockets;
    public List<PresetSocket> vortexSockets;
    public int presetSize {
        get {
        playfieldSockets = new List<PresetSocket>(GetComponentsInChildren<PresetSocket>()).Where(x=>x.socketType == SocketType.playfield).ToList();
            return playfieldSockets.Count;
        }
    }

    public void SelectThisPreset(){
        vortexSockets = new List<PresetSocket>(GetComponentsInChildren<PresetSocket>()).Where(x=>x.socketType == SocketType.vortex).ToList();
        List<ArenaPreset> allPresets = FindObjectsOfType<ArenaPreset>().ToList();
        foreach(ArenaPreset preset in allPresets){
            if(preset != this)
                preset.gameObject.SetActive(false);
            else
                preset.gameObject.SetActive(true);
        }
    }
}
