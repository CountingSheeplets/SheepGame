using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SheepUnit : MonoBehaviour
{
    public Owner owner;
    public bool canBeThrown = true;
    public Playfield currentPlayfield;
    public bool isReadying;
    public bool isSwimming = false;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(EventName.System.Sheep.Land(), OnLand);
    }
    void OnLand(GameMessage msg){
        if(msg.sheepUnit == this){
            Transform nearestVortex = FindObjectsOfType<Vortex>().Select(x=>x.transform).ToList().FindNearest(transform);
            GetComponent<SheepSwim>().StartSwiming(0.25f, nearestVortex.position);
        }
    }
}
