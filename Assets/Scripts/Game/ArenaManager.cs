using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ArenaManager : MonoBehaviour
{
    public List<Playfield> playfields = new List<Playfield>();
    public List<ArenaPreset> presets = new List<ArenaPreset>();
    public GameObject playfieldPrefab;
    public int emptySpacesBetweenFields = 1;
    bool gameStarted = false;

    void Start()
    {
        presets.AddRange(GetComponentsInChildren<ArenaPreset>());
        EventManager.StartListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventManager.StartListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventManager.StartListening(EventName.Input.StartGame(), OnStartGame);
        EventManager.StartListening(EventName.System.Player.Defeated(), OnPlayerDefeated);
    }
    void OnDestroy(){
        EventManager.StopListening(EventName.Input.Network.PlayerJoined(), OnPlayerJoined);
        EventManager.StopListening(EventName.Input.Network.PlayerLeft(), OnPlayerLeft);
        EventManager.StopListening(EventName.Input.StartGame(), OnStartGame);
        EventManager.StopListening(EventName.System.Player.Defeated(), OnPlayerDefeated);
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
    void OnStartGame(GameMessage msg){
        gameStarted = true;
        ArrangeFields();
    }
    void OnPlayerLeft(GameMessage msg){
        //destroy arena if game has not started yet, if started, leave?
        if(!gameStarted){
            RemoveField(msg.owner);
        }
    }
    void RemoveField(Owner owner){
        Debug.Log(owner);
        Debug.Log(playfields.Count);
        Playfield pl = playfields.Where(x=>x.GetComponent<Owner>().EqualsByValue(owner)).FirstOrDefault();
        playfields.Remove(pl);
        Debug.Log(pl.gameObject.name);
        if(pl != null)
            Destroy(pl.gameObject);
    }
    void OnPlayerDefeated(GameMessage msg){
        RemoveField(msg.owner);
        ArrangeFields();
    }
    void ArrangeFields(){
        List<ArenaPreset> availablePresets = presets.Where(x=>x.presetSize == playfields.Count).ToList();
        List<PresetSocket> sockets = new List<PresetSocket>(availablePresets[Random.Range(0, availablePresets.Count-1)].sockets);
        List<Playfield> leftoverFields = new List<Playfield>(playfields);
        bool hasFence = leftoverFields[0].generateFence;
        for(int i = 0; i < playfields.Count; i++){
            int rS = Random.Range(0, sockets.Count-1);
            int rF = Random.Range(0, leftoverFields.Count-1);

            Playfield p = playfieldPrefab.GetComponent<Playfield>();
            float fieldWidth = p.fieldSize.x; 
            fieldWidth += emptySpacesBetweenFields * p.tileSize;
            if(hasFence)
                fieldWidth += 2f * p.tileSize;
            Vector2 offset = sockets[rS].transform.position;

            leftoverFields[rF].transform.position = offset * fieldWidth;
            leftoverFields.RemoveAt(rF);
            sockets.RemoveAt(rS);
        }
    }
}
