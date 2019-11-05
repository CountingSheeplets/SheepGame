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
            List<Owner> owners = new List<Owner>(GetComponentsInChildren<Owner>());
            EventManager.TriggerEvent(EventName.System.Player.Defeated(), GameMessage.Write().WithOwner(owners[Random.Range(0, owners.Count-1)]));
        }
    }
}
