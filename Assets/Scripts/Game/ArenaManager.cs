using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ArenaManager : MonoBehaviour
{
    public List<Playfield> playfields = new List<Playfield>();
    public GameObject playfieldPrefab;
    bool gameStarted = false;

    void Start()
    {
        EventManager.StartListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventManager.StartListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventManager.StartListening(EventName.Input.StartGame(), OnStartGame);
    }
    void OnDestroy(){
        EventManager.StopListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventManager.StopListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventManager.StopListening(EventName.Input.StartGame(), OnStartGame);
    }

    void OnPlayerJoined(GameMessage msg){
        //check if there is not already a player who has an arena under his name
        if(playfields.Where(x=>x.GetComponent<Owner>().EqualsByValue(msg.owner)).FirstOrDefault() == null){
            //, if not then create
            GameObject newFieldGO = Instantiate(playfieldPrefab);
            Playfield newField = newFieldGO.GetComponent<Playfield>();
            Owner newOwner = newField.GetComponent<Owner>().Create(msg.owner);
            playfields.Add(newField);
            newField.transform.parent = transform;
            newField.gameObject.name = "Playfield:"+newOwner.ownerName;
        }
        ArrangeFields();
    }
    void OnStartGame(GameMessage msg){gameStarted = true;}
    void OnPlayerLeft(GameMessage msg){
        //destroy arena if game has not started yet, if started, leave?
        if(!gameStarted){
            Debug.Log(msg.owner);
            Debug.Log(playfields.Count);
            Playfield pl = playfields.Where(x=>x.GetComponent<Owner>().EqualsByValue(msg.owner)).FirstOrDefault();
            playfields.Remove(pl);
            Debug.Log(pl.gameObject.name);
            if(pl != null)
                Destroy(pl.gameObject);
        }
    }

    void ArrangeFields(){
        if(playfields.Count>1){
            for(int i = 0; i < playfields.Count; i++){
                int incX = Mathf.FloorToInt(playfieldPrefab.GetComponent<Playfield>().fieldSize.x+2);
                int offsetX = -(incX * playfields.Count)/2;
                playfields[i].transform.position = new Vector2(offsetX + i * incX, 0);
            }
        }
    }
}
