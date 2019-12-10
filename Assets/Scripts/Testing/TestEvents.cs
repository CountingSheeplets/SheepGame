using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class TestEvents : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D)){
            Debug.Log("Defeat Random Player...");
            List<Owner> owners = OwnersCoordinator.GetOwners();
            EventCoordinator.TriggerEvent(EventName.System.Player.Defeated(), GameMessage.Write().WithOwner(owners[Random.Range(0, owners.Count-1)]));
        }
        if(Input.GetKeyDown(KeyCode.A)){
            Debug.Log("Fake Hit a random King with random sheep...");
            List<KingUnit> kings = new List<KingUnit>( FindObjectsOfType<KingUnit>() );
            List<SheepUnit> sheeps = new List<SheepUnit>( FindObjectsOfType<SheepUnit>() );
            SheepUnit sheep = null;
            List<Owner> owners = OwnersCoordinator.GetOwners();
            if(sheeps.Count>0)
                sheep = sheeps[Random.Range(0, sheeps.Count-1)];
            EventCoordinator.TriggerEvent(EventName.System.King.Hit(), GameMessage.Write().WithKingUnit(kings[Random.Range(0, kings.Count-1)]).WithSheepUnit(sheep).WithOwner(owners[Random.Range(0, owners.Count-1)]));
        }
        if(Input.GetKeyDown(KeyCode.B)){
            Debug.Log("Fake eat grass...");
            EventCoordinator.TriggerEvent(EventName.System.Economy.EatGrass(), GameMessage.Write().WithFloatMessage(10f));
        }
        if(Input.GetKeyDown(KeyCode.S)){
            NetworkCoordinator.SendShowViewAll("match");
        }
    }
}