using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSalto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(EventName.System.King.Hit(), OnKingHit);
    }
    void OnKingHit(GameMessage msg)
    {
        Debug.Log("sheep doing a salto, in similar manner coords as roam, but animation of salto/fly");
    }
}
