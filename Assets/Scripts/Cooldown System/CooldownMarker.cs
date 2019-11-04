/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownMarker : MonoBehaviour, IMarker
{
    public  bool _isMarked = false;
    public bool IsMarked{get {return _isMarked;}}

    Sprite filler;
    public GameObject abilityGO;
    public Color defaultCdColor = new Color();
    public float defaultLineRadius = 1f;
    public float defaultLineWidth = 0.5f;

    void Awake(){
        filler = GetComponentInChildren<Sprite>();
        filler.startColor = defaultCdColor;
        filler.endColor = defaultCdColor;
        UpdateToCooldown(0, 10);
    }

    public void Mark(GameObject _ability){
        _isMarked = true;
        abilityGO = _ability;
        transform.position = abilityGO.transform.position;
        
        EventManager.StartListening(EventName.System.Cooldown.Tick(), OnCdTick);
        EventManager.StartListening(EventName.System.Cooldown.PostEnd(), OnCdEnded);
    }

    void OnCdTick(GameMessage msg){
        if (BuildingIsEmpty(abilityGO))
            return;
        if (msg.cooldownGroup.IsMyGroup(GetBuilding(abilityGO))){
            Owner owner = abilityGO.GetComponent<Owner>();
            if (owner == null)
                owner = PlayerProfile.GetDefaultOwner();
            UpdateToCooldown(msg.cooldownGroup.CurrentCooldown(owner), msg.cooldownGroup.maxCooldown);
            transform.position = abilityGO.transform.position;
        }
    }

    void OnCdEnded(GameMessage msg){
        if (BuildingIsEmpty(abilityGO))
            return;
        if (msg.cooldownGroup.IsMyGroup(GetBuilding(ability))){// && ability.GetType() != typeof(IGhost)){
            gameObject.StoreMarker();
        }// else Debug.Log("false type:"+ability.GetType() + " GO name:"+ability.gameObject.name);
    }


    public void UpdateToCooldown(float current, float max){
        float lineArc = current/max * 360;
        //Debug.Log(lineArc);
        if (lineArc < 0) //just to be safe, though it shouldnt happen, it already did once...
            lineArc = 0;
        DrawCircle(defaultLineRadius, defaultLineWidth, lineArc);
    }

    public void DrawCircle(float radius, float lineWidth, float arc)
    {
        var segments = (int)arc;
        filler.useWorldSpace = false;
        filler.startWidth = lineWidth;
        filler.endWidth = lineWidth;
        filler.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i);// * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        filler.SetPositions(points);
    }
    void LookAtPlayer(){
        Vector3 targetPostition = new Vector3(Camera.main.transform.position.x, 
                                        this.transform.position.y, 
                                        Camera.main.transform.position.z) ;
        transform.LookAt(targetPostition);
    }

    public void UnMark(){
        abilityGO = null;
        UpdateToCooldown(0, 10);
        _isMarked = false;
        EventManager.StopListening(EventName.System.Cooldown.Tick(), OnCdTick);
        EventManager.StopListening(EventName.System.Cooldown.PostEnd(), OnCdEnded);
    }
    void OnDisable(){
        UnMark();
    }

    //helpers:
    BaseAbility GetBuilding(GameObject go){
        BaseAbility b = go.GetComponent<BaseAbility>();
        if (b != null)
            return b;
        return null;
    }

    bool BuildingIsEmpty(GameObject abilityGO){
        if (abilityGO == null){
            gameObject.StoreMarker();
            return true;
        } else {
            if (!abilityGO.activeInHierarchy){
                gameObject.StoreMarker();
                return true;
            }
        }
        return false;
    }
}
 */