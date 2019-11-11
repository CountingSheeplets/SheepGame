using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ArenaManager : Singleton<ArenaManager>
{[SerializeField]
    int _gridSize;
    public static int GridSize {get {return Instance._gridSize;}}
    [SerializeField]
    float _tileSize = 0.5f;
    public static float TileSize {get {return Instance._tileSize;}}
    [SerializeField]
    Vector2 _tileScale;
    public static Vector2 TileScale {get {return Instance._tileScale;}}
    public static Vector2 fieldSize {
        get{
            float width = Instance._gridSize * Instance._tileSize;
            return new Vector2(width, width);
        }
    }

    public List<Playfield> playfields = new List<Playfield>();
    public List<Vortex> currentVortexes = new List<Vortex>();
    public List<ArenaPreset> presets = new List<ArenaPreset>();
    public GameObject playfieldPrefab;
    public GameObject vortexPrefab;
    public int emptySpacesBetweenFields = 1;
    bool gameStarted = false;

    void Start()
    {
        presets.AddRange(GetComponentsInChildren<ArenaPreset>(true));
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
    }
    void OnStartGame(GameMessage msg){
        gameStarted = true;
        RearrangeArena();
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
        RearrangeArena();
    }
    void RearrangeArena(){
        List<ArenaPreset> availablePresets = presets.Where(x=>x.presetSize == playfields.Count).ToList();
        ArenaPreset randomPreset = availablePresets[Random.Range(0, availablePresets.Count-1)];
        randomPreset.SelectThisPreset();
        ArrangeFields(randomPreset);
        RemoveVortexes();
        AddVortexes(randomPreset);
    }
    void ArrangeFields(ArenaPreset randomPreset){
        List<PresetSocket> sockets = new List<PresetSocket>(randomPreset.playfieldSockets);
        List<Playfield> leftoverFields = new List<Playfield>(playfields);
        Playfield p = playfieldPrefab.GetComponent<Playfield>();
        float fieldWidth = fieldSize.x + emptySpacesBetweenFields * _tileSize;

        for(int i = 0; i < playfields.Count; i++){
            int rS = Random.Range(0, sockets.Count-1);
            int rF = Random.Range(0, leftoverFields.Count-1);
            Vector2 offset = sockets[rS].transform.position;
            leftoverFields[rF].transform.position = offset * fieldWidth;
            if(leftoverFields[rF].fieldCorners !=null)
                leftoverFields[rF].fieldCorners.Recenter(offset * fieldWidth);
            leftoverFields.RemoveAt(rF);
            sockets.RemoveAt(rS);
        }
    }
    void AddVortexes(ArenaPreset randomPreset){
        List<PresetSocket> vortexSockets = new List<PresetSocket>(randomPreset.vortexSockets);
        float fieldWidth = fieldSize.x + emptySpacesBetweenFields * _tileSize;
        for(int i = 0; i < vortexSockets.Count; i++){
            GameObject newVortex = Instantiate(vortexPrefab);
            newVortex.transform.parent = transform;
            newVortex.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
            currentVortexes.Add(newVortex.GetComponent<Vortex>());
            Vector2 offset = vortexSockets[i].transform.position;
            newVortex.transform.position = offset * fieldWidth; 
        }
        Debug.Log("AddVortexes:"+currentVortexes.Count);
    }
    void RemoveVortexes(){
        if(currentVortexes.Count > 0)
            for(int i = currentVortexes.Count - 1; i>=0; i--){
                Animator anim = currentVortexes[i].GetComponent<Animator>();
                anim.SetBool("close", true);
                Destroy(currentVortexes[i].gameObject, 2);
                currentVortexes.RemoveAt(i);
                Debug.Log("RemoveVortexes:"+currentVortexes.Count);
            }
    }
    public static Playfield GetPlayfieldForPoint(Vector2 point){
        foreach(Playfield playfield in Instance.playfields){
            if(playfield.fieldCorners.IsWithinField(point))
                return playfield;
        }
        Debug.Log("Not in any playfield");
        return null;
    }
}
