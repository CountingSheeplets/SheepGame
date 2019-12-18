using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class KingSmash : MonoBehaviour
{
    public List<Vector2> destinations = new List<Vector2>();
    public List<Vector2> initPos = new List<Vector2>();
    Owner owner;
    public float hitRange;
    public float flyDistance = 2f;
    public float knockFlySpeed = 2f;
    KingUnit king;
    void Start()
    {
        if(king == null) king = GetComponentInParent<KingUnit>();
        if(owner == null) owner = king.owner;
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnSmash);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnSmash);
    }
    void OnSmash(GameMessage msg)
    {
        if(owner.EqualsByValue (msg.owner)){
            AnimateSmash();
            SendSheepFly();
        }
    }

    void AnimateSmash(){

    }
    void Update(){
        for(int i=0; i < destinations.Count;i++)
            Debug.DrawLine(initPos[i], destinations[i], king.owner.GetPlayerProfile().playerColor);
    }
    void SendSheepFly(){
        destinations.Clear();
        initPos.Clear();
        List<SheepUnit> sheepWithinRange = SheepCoordinator.GetSheepInField(king.myPlayfield).Where(x => (x.GetComponent<Transform>().position-transform.position).magnitude <= hitRange).ToList();
        foreach(SheepUnit sheep in sheepWithinRange){
            SheepFly fly = sheep.GetComponent<SheepFly>();
            Vector2 destination = GetDestinatinoVector(sheep.transform);
            destinations.Add(destination);
            initPos.Add(sheep.transform.position);
            fly.StartFlying(knockFlySpeed, destination);
        }
    }
    Vector2 GetDestinatinoVector(Transform sheepTr){
        Vector2 direction = (sheepTr.position - transform.position).normalized;
        //Debug.Log("SMASH: king:"+transform.position+" sheep:"+sheepTr.position+"  dir:"+direction);
        Vector2 destination = direction * flyDistance;
        return destination + (Vector2)sheepTr.position;
    }
}
