using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public List<Playfield> playfields = new List<Playfield>();
    public GameObject playfieldPrefab;

    void Start()
    {
        EventManager.StartListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventManager.StartListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
    }
    void OnDestroy(){
        EventManager.StopListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventManager.StopListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
    }

    void OnPlayerJoined(GameMessage msg){
        //check if there is not already a player who has an arena under his name
        
        
        //, if not then create
        GameObject newFieldGO = Instantiate(playfieldPrefab);
        Playfield newField = newFieldGO.GetComponent<Playfield>();
        Owner newOwner = newField.GetComponent<Owner>().Create(msg.owner);
        playfields.Add(newField);
        newField.transform.parent = transform;
        newField.gameObject.name = "Playfield:"+newOwner.ownerName;
    }

    void OnPlayerLeft(GameMessage msg){
        //destroy arena if game has not started yet, if started, leave?
    }

}
