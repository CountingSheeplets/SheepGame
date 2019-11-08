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
            return playfieldSockets.Count;
        }
    }
    void Start(){
        playfieldSockets = new List<PresetSocket>(GetComponentsInChildren<PresetSocket>()).Where(x=>x.socketType == SocketType.playfield).ToList();
        vortexSockets = new List<PresetSocket>(GetComponentsInChildren<PresetSocket>()).Where(x=>x.socketType == SocketType.vortex).ToList();
    }
    

    public void SelectThisPreset(){
        List<ArenaPreset> allPresets = FindObjectsOfType<ArenaPreset>().ToList();
        foreach(ArenaPreset preset in allPresets){
            if(preset != this)
                preset.gameObject.SetActive(false);
            else
                preset.gameObject.SetActive(true);
        }
    }
}
