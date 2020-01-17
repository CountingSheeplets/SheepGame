﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KingSmash : MonoBehaviour
{
    public List<Vector2> destinations = new List<Vector2>();
    public List<Vector2> initPos = new List<Vector2>();
    Owner owner;
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
            SendSheepFly(msg.sheepUnits);
        }
    }

    void AnimateSmash(){
    }
    
    void Update(){
        for(int i=0; i < destinations.Count;i++)
            Debug.DrawLine(initPos[i], destinations[i], king.owner.GetPlayerProfile().playerColor);
    }
    void SendSheepFly(List<SheepUnit> sheeps){
        destinations.Clear();
        initPos.Clear();
        foreach(SheepUnit sheep in sheeps){
            SheepFly fly = sheep.GetComponent<SheepFly>();
            Vector2 destination = GetDestinatinoVector(sheep.transform);
            destinations.Add(destination);
            initPos.Add(sheep.transform.position);
            fly.StartFlying(SpeedBucket.GetKnockbackSpeed(sheep.sheepType), destination);
        }
    }
    Vector2 GetDestinatinoVector(Transform sheepTr){
        Vector2 direction = (sheepTr.position - transform.position).normalized;
        //Debug.Log("SMASH: king:"+transform.position+" sheep:"+sheepTr.position+"  dir:"+direction);
        Vector2 destination = direction * flyDistance;
        return destination + (Vector2)sheepTr.position;
    }
}