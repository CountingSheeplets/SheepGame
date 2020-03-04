using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceController : MonoBehaviour
{
    public List<FenceTile> fenceTiles = new List<FenceTile>();
    void Start()
    {
        EventCoordinator.StartListening(EventName.System.Booster.Consumed(), OnBoosterConsumed);
    }

    void OnBoosterConsumed(GameMessage msg)
    {
        //check booster type, and perform action if it's fence booster
    }
    public void OnFenceTileAdd(FenceTile newTile){
        fenceTiles.Add(newTile);
    }
    public void FenceGenerationEnded(){
        gameObject.SetActive(false);
    }
}
