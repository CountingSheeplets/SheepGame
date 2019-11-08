using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public FieldCorners fieldCorners;

    public GameObject fieldTilePrefab;
    public GameObject fenceTilePrefab;
    public List<FieldTile> fieldTiles = new List<FieldTile>();
    public List<FenceTile> fenceTiles = new List<FenceTile>();
    public int currentHitpoints = 0;
    public bool generateFence = true;
    public Transform fieldParent;
    public Transform fenceParent;

    public Playfield Init(){
        GameObject newPlayfieldGO = Instantiate(gameObject);
        Playfield playfield = newPlayfieldGO.GetComponent<Playfield>();
        return playfield;
    }

    void Start(){
        fieldCorners = new FieldCorners(ArenaManager.fieldSize);
        //Debug.Log("object set: fieldCorners..."+fieldCorners);
        //EventManager.StartListening(EventName.Input.Swipe(), OnSwipe);
        EventManager.StartListening(EventName.Input.StartGame(), OnStart);
        EventManager.StartListening(EventName.System.Environment.SetField(), OnSetField);
        EventManager.StartListening(EventName.System.Environment.AdjustField(), OnAdjustField);
    }
    void OnDestroy(){
        //EventManager.StopListening(EventName.Input.Swipe(), OnSwipe);
        EventManager.StopListening(EventName.Input.StartGame(), OnStart);
        EventManager.StopListening(EventName.System.Environment.SetField(), OnSetField);
        EventManager.StopListening(EventName.System.Environment.AdjustField(), OnAdjustField);
    }
/*     void OnSwipe(GameMessage msg){
        Debug.Log("swiping:"+msg.swipe.vector);
    } */
    void OnStart(GameMessage msg){
        //generate field area:
        Spiral sp = new Spiral();
        for(int i = 0;i < ArenaManager.GridSize * ArenaManager.GridSize; i++){
            GameObject newTileGO = Instantiate(fieldTilePrefab, Vector2.zero, Quaternion.identity, fieldParent);
            newTileGO.transform.localPosition = sp.ThisPoint() * ArenaManager.TileSize;
            sp.Next();
            newTileGO.transform.localScale = ArenaManager.TileScale;
            fieldTiles.Add(newTileGO.GetComponent<FieldTile>());
        }
        //generate fences around field:
        //Vector2 offset = new Vector2(-(size.x+1)/2, (size.y+1)/2);
        if(generateFence)
            for(int i = 0; i < (ArenaManager.GridSize + 2 + ArenaManager.GridSize) * 2; i++){
                if((i+1) % (ArenaManager.GridSize + 1) != 0) {
                    GameObject newTileGO = Instantiate(fenceTilePrefab, Vector2.zero, Quaternion.identity, fenceParent);
                    newTileGO.transform.localPosition = sp.ThisPoint() * ArenaManager.TileSize;
                    newTileGO.transform.localScale = ArenaManager.TileScale;
                    Vector3 rotateBy = new Vector3(0, 0, 90f * Mathf.FloorToInt(i / (ArenaManager.GridSize + 1)));
                    //Debug.Log("rotateBy "+rotateBy);
                    newTileGO.transform.Rotate(rotateBy, Space.World);
                    fenceTiles.Add(newTileGO.GetComponent<FenceTile>());
                } else {
                    //Debug.Log("SP: "+sp.ThisPoint()+" i:"+ i+"  %="+(i % (ArenaManager.GridSize + 1) ));
                }
                sp.Next();
            }
        SetHitpointsTo(55);
    }
    void OnSetField(GameMessage msg){
        if(msg.owner.EqualsByValue(GetComponent<Owner>()));
            SetHitpointsTo(msg.intMessage);
    }
    public void SetHitpointsTo(int hitpoints){
        currentHitpoints = hitpoints;
        for(int i = fieldTiles.Count-1; i>=hitpoints; i-- ){
            fieldTiles[i].SetState(false);
        }
        for(int i = hitpoints-1; i>=0; i-- ){
            fieldTiles[i].SetState(true);
        }
    }
    void OnAdjustField(GameMessage msg){
        if(msg.owner.EqualsByValue(GetComponent<Owner>()));
            AdjustHitPoints(msg.intMessage);
    }
    public void AdjustHitPoints(int amount){
        if(amount > 0)
            for(int i = 0; i < amount; i++ ){
                fieldTiles[currentHitpoints + i].SetState(false);
            }
        if(amount < 0)
            for(int i = 0; i < Mathf.Abs(amount); i++ ){
                fieldTiles[currentHitpoints - i].SetState(false);
            }
    }
    public override string ToString(){
        return "["+gameObject.name+"]";
    }
    
}
public class Spiral
{
    public int x = 0;
    public int y = 0;
    public void Next()
    {
        if(x == 0 && y == 0)
        {
            x = 1;
            return;
        }
        if(Mathf.Abs(x) > Mathf.Abs(y)+0.5f*Mathf.Sign(x) && Mathf.Abs(x) > (-y+0.5f))
            y += (int)Mathf.Sign(x);
        else
            x -= (int)Mathf.Sign(y);
    }
    public Vector2 NextPoint()
    {
        Next();
        return new Vector2(x,y);
    }
    public Vector2 ThisPoint(){
        return new Vector2(x, y);
    }
    public void Reset()
    {
        x = 0;
        y = 0;
    }
}
