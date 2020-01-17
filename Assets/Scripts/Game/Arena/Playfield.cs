using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public Owner owner;
    
    public FieldCorners fieldCorners;

    public GameObject fieldTilePrefab;
    public GameObject fenceTilePrefab;
    public GameObject backgroundSprite;
    public List<FieldTile> fieldTiles = new List<FieldTile>();
    public List<FenceTile> fenceTiles = new List<FenceTile>();
    public float currentHitpoints = 0f;
    public bool generateFence = true;
    public Transform fieldParent;
    public Transform fenceParent;
    public Transform sheepParent; // used by SheepFactory

    public bool isAnimating = false;
    
    public Playfield Init(){
        GameObject newPlayfieldGO = Instantiate(gameObject);
        Playfield playfield = newPlayfieldGO.GetComponent<Playfield>();
        return playfield;
    }

    void Start(){
        fieldCorners = new FieldCorners(ArenaCoordinator.fieldSize);
        //Debug.Log("object set: fieldCorners..."+fieldCorners);
        //EventManager.StartListening(EventName.Input.Swipe(), OnSwipe);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStart);
        EventCoordinator.StartListening(EventName.System.Environment.SetField(), OnSetField);
        EventCoordinator.StartListening(EventName.System.Environment.AdjustField(), OnAdjustField);
    }
    void OnDestroy(){
        //EventManager.StopListening(EventName.Input.Swipe(), OnSwipe);
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStart);
        EventCoordinator.StopListening(EventName.System.Environment.SetField(), OnSetField);
        EventCoordinator.StopListening(EventName.System.Environment.AdjustField(), OnAdjustField);
        ScoreCoordinator.SetTechTier2Counts(owner, GetComponent<SheepUpgrade>().tier2UpgradeCount);
    }
/*     void OnSwipe(GameMessage msg){
        Debug.Log("swiping:"+msg.swipe.vector);
    } */
    void OnStart(GameMessage msg){
        //adjust background size:
        backgroundSprite.transform.localScale = ConstantsBucket.PlayfieldTileScale * ConstantsBucket.GridSize;
        //generate field area:
        Spiral sp = new Spiral();
        for(int i = 0;i < ConstantsBucket.GridSize * ConstantsBucket.GridSize; i++){
            GameObject newTileGO = Instantiate(fieldTilePrefab, Vector2.zero, Quaternion.identity, fieldParent);
            newTileGO.transform.localPosition = sp.ThisPoint() * ConstantsBucket.PlayfieldTileSize;
            sp.Next();
            newTileGO.transform.localScale = ConstantsBucket.PlayfieldTileScale;
            fieldTiles.Add(newTileGO.GetComponent<FieldTile>());
        }
        //generate fences around field:
        //Vector2 offset = new Vector2(-(size.x+1)/2, (size.y+1)/2);
        if(generateFence)
            for(int i = 0; i < (ConstantsBucket.GridSize + 2 + ConstantsBucket.GridSize) * 2; i++){
                if((i+1) % (ConstantsBucket.GridSize + 1) != 0) {
                    GameObject newTileGO = Instantiate(fenceTilePrefab, Vector2.zero, Quaternion.identity, fenceParent);
                    newTileGO.transform.localPosition = sp.ThisPoint() * ConstantsBucket.PlayfieldTileSize;
                    newTileGO.transform.localScale = ConstantsBucket.PlayfieldTileScale;
                    Vector3 rotateBy = new Vector3(0, 0, 90f * Mathf.FloorToInt(i / (ConstantsBucket.GridSize + 1)));
                    //Debug.Log("rotateBy "+rotateBy);
                    newTileGO.transform.Rotate(rotateBy, Space.World);
                    newTileGO.GetComponentInChildren<SpriteRenderer>().transform.Rotate(-rotateBy, Space.World);
                    fenceTiles.Add(newTileGO.GetComponent<FenceTile>());
                } else {
                    //Debug.Log("SP: "+sp.ThisPoint()+" i:"+ i+"  %="+(i % (ArenaManager.GridSize + 1) ));
                }
                sp.Next();
            }
        SetHitpointsTo(55);
    }
    void OnSetField(GameMessage msg){
        if(msg.owner.EqualsByValue(GetComponent<Owner>()))
            SetHitpointsTo(msg.intMessage);
    }
    public float GetHitpoints(){
        return currentHitpoints;
    }
    public void SetHitpointsTo(int hitpoints){
        if(hitpoints > fieldTiles.Count-1)
            currentHitpoints = fieldTiles.Count -1;
        else
            currentHitpoints = hitpoints;

        for(int i = fieldTiles.Count-1; i>=currentHitpoints; i-- ){
            fieldTiles[i].SetState(false);
        }
        for(int i = Mathf.FloorToInt(currentHitpoints-1); i>=0; i-- ){
            fieldTiles[i].SetState(true);
        }
    }
    void OnAdjustField(GameMessage msg){
        if(msg.owner.EqualsByValue(GetComponent<Owner>())){
            AdjustHitPoints(msg.floatMessage);
        }
    }
    public float AdjustHitPoints(float amount){
        if(amount + currentHitpoints > fieldTiles.Count-1)
            amount = fieldTiles.Count - 1 - currentHitpoints;

        if(amount > 0)
            for(int i = 0; i < Mathf.FloorToInt(currentHitpoints+amount) - Mathf.FloorToInt(currentHitpoints); i++ ){
                if(Mathf.CeilToInt(currentHitpoints - 1) + i < fieldTiles.Count)
                    fieldTiles[Mathf.CeilToInt(currentHitpoints-1) + i].SetState(true);
            }
        if(amount < 0)
            for(int i = 0; i < Mathf.FloorToInt(currentHitpoints) - Mathf.FloorToInt(currentHitpoints+amount); i++ ){
                if(Mathf.FloorToInt(currentHitpoints) - i >=1)
                    fieldTiles[Mathf.FloorToInt(currentHitpoints-1) - i].SetState(false);
            }
        currentHitpoints+=amount;
        if(currentHitpoints < 0)
            currentHitpoints = 0;

        return currentHitpoints;
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
