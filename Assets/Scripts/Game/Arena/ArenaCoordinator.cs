using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ArenaCoordinator : Singleton<ArenaCoordinator>
{
    public float playfieldArrangeFloatSpeedConstant = 5f;
    [SerializeField]
    int _gridSize = 11;
    public static int GridSize {get {return Instance._gridSize;}}
    [SerializeField]
    float _tileSize = 0.5f;
    public static float TileSize {get {return Instance._tileSize;}}
    [SerializeField]
    Vector2 _tileScale = new Vector2(2.5f,2.5f);
    public static Vector2 TileScale {get {return Instance._tileScale;}}
    public static Vector2 fieldSize {
        get{
            float width = Instance._gridSize * Instance._tileSize;
            return new Vector2(width, width);
        }
    }

    private List<Playfield> playfields = new List<Playfield>();
    public static List<Playfield> GetPlayfields{
        get{return Instance.playfields;}
    }
    public List<Vortex> currentVortexes = new List<Vortex>();
    public List<ArenaPreset> presets = new List<ArenaPreset>();
    public GameObject playfieldPrefab;
    public GameObject vortexPrefab;
    public int emptySpacesBetweenFields = 1;

    void Start(){
        presets.AddRange(GetComponentsInChildren<ArenaPreset>(true));
    }
    public static Playfield GetOrCreateField(Owner owner){
        //check if there is not already a player who has an arena under his name
        Playfield hasPlayfield = ArenaCoordinator.GetPlayfields.Where(x=>x.GetComponent<Owner>().EqualsByValue(owner)).FirstOrDefault();
        if(hasPlayfield == null){
            GameObject newFieldGO = Instantiate(Instance.playfieldPrefab);
            Playfield newField = newFieldGO.GetComponent<Playfield>();
            Owner newOwner = newField.GetComponent<Owner>().Create(owner);
            newField.owner = owner;
            Instance.playfields.Add(newField);
            newField.transform.parent = Instance.transform;
            newField.gameObject.name = "Playfield:"+newOwner.ownerName;
            return newField;
        } else
        return hasPlayfield;
    }
    public static void RemoveField(Owner owner){
        //Debug.Log(owner);
        //Debug.Log(playfields.Count);
        Playfield pl = Instance.playfields.Where(x=>x.GetComponent<Owner>().EqualsByValue(owner)).FirstOrDefault();
        Instance.playfields.Remove(pl);
        if(pl != null)
            Destroy(pl.gameObject);
    }

    public static void RearrangeArena(){
        List<ArenaPreset> availablePresets = Instance.presets.Where(x=>x.presetSize == Instance.playfields.Count).ToList();
        if(availablePresets.Count <= 0)
            Debug.LogError("Zero Arena Presets found for this player amount! Exiting...");
        ArenaPreset randomPreset = availablePresets[Random.Range(0, availablePresets.Count-1)];
        randomPreset.SelectThisPreset();
        Instance.ArrangeFields(randomPreset);

        EventCoordinator.TriggerEvent(EventName.System.Environment.ArenaAnimating(), GameMessage.Write());

        Instance.RemoveVortexes();
        Instance.AddVortexes(randomPreset);
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
            //leftoverFields[rF].transform.position = offset * fieldWidth;
            FieldFloat pFloat = leftoverFields[rF].GetComponent<FieldFloat>();
            pFloat.StartFloating(playfieldArrangeFloatSpeedConstant, offset * fieldWidth);
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
    public static Playfield GetPlayfield(Vector2 point){
        foreach(Playfield playfield in Instance.playfields){
            if(playfield.fieldCorners.IsWithinField(point))
                return playfield;
        }
        Debug.Log("Not in any playfield");
        return null;
    }
    public static Playfield GetPlayfield(Owner owner){
        foreach(Playfield pf in Instance.playfields){
            if(pf.GetComponent<Owner>().EqualsByValue(owner))
                return pf;
        }
        return null;
    }
}
