using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D)){
            Debug.Log("Defeat Random Player...");
            List<Owner> owners = OwnersManager.GetOwners();
            EventManager.TriggerEvent(EventName.System.Player.Defeated(), GameMessage.Write().WithOwner(owners[Random.Range(0, owners.Count-1)]));
        }
        if(Input.GetKeyDown(KeyCode.A)){
            Debug.Log("Fake Hit a random King with random sheep...");
            List<KingUnit> kings = new List<KingUnit>( FindObjectsOfType<KingUnit>() );
            List<SheepUnit> sheeps = new List<SheepUnit>( FindObjectsOfType<SheepUnit>() );
            SheepUnit sheep = null;
            List<Owner> owners = OwnersManager.GetOwners();
            if(sheeps.Count>0)
                sheep = sheeps[Random.Range(0, sheeps.Count-1)];
            EventManager.TriggerEvent(EventName.System.King.Hit(), GameMessage.Write().WithKingUnit(kings[Random.Range(0, kings.Count-1)]).WithSheepUnit(sheep).WithOwner(owners[Random.Range(0, owners.Count-1)]));
        }
    }
}