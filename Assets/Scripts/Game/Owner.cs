﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Owner : MonoBehaviour {
    public string ownerId = "";
    public int deviceId = 0;
    public string ownerName = "";
    public int teamId = 0;
    public bool connected = false;
    bool _ready = false;
    bool _isFirstOwner = false;
    public bool IsFirstOwner {
        get { return _isFirstOwner; }
        set {
            if (_isFirstOwner != value) {
                //NetworkCoordinator.SendFirstOwner(deviceId, value);
                _isFirstOwner = value;
            }
        }
    }
    public bool ready {
        get { return connected ? _ready : true; }
        set {
            if (!GameStateView.HasState(GameState.started)) {
                GetMyTile().Ready(value);
                //if (_ready != value)
                //NetworkCoordinator.SendConfirmReady(deviceId, value);
            }
            _ready = value;
        }
    }
    bool _playAgain = false;
    public bool playAgain {
        get { return _playAgain; }
        set {
            _playAgain = value;
            ScoreRow row = FindObjectsOfType<ScoreRow>().Where(x => x.playerName.text == ownerName).FirstOrDefault();
            if (row != null)
                row.SetPlayAgain();
            //NetworkCoordinator.SendConfirmPlayAgain(deviceId, value);
        }
    }
    PlayerOwnerTile myTile = null;
    public PlayerOwnerTile GetMyTile() {
        if (myTile == null) {
            myTile = FindObjectsOfType<PlayerOwnerTile>().Where(x => x.myOwner.EqualsByValue(this)).FirstOrDefault();
        }
        return myTile;
    }

    [BitMask(typeof(OwnerType))]
    public OwnerType ownerType;
    public Owner Create(Owner newOwner) {
        ownerId = newOwner.ownerId;
        ownerName = newOwner.ownerName;
        teamId = newOwner.teamId;
        ownerType = newOwner.ownerType;
        if (IsPlayer()) {
            connected = true;
        }

        return this;
    }
    public void Create(string id, string nm, int devId) {
        ownerId = id;
        ownerName = nm;
        ownerType = OwnerType.Player;
        deviceId = devId;
        if (IsPlayer())
            connected = true;
    }
    public void AddMask(OwnerType[] newTypes) {
        ownerType.Set(newTypes);
    }
    public KingUnit GetKing() {
        return KingCoordinator.GetKing(this);
    }
    private Playfield _playfield;
    public Playfield GetPlayfield() {
        if (_playfield != null)
            return _playfield;
        else
            _playfield = ArenaCoordinator.GetPlayfield(this);
        return _playfield;
    }
    public PlayerProfile GetPlayerProfile() {
        return PlayerProfileCoordinator.GetProfile(this);
    }
    public string GetToken(int crownCount) {
        string token = "sheep" + ownerId + "_!" + crownCount;
        //Debug.Log("token:"+token);
        return CryptoHelper.md5(token);
    }
    public bool IsMine() {
        //int mask = 0x01 | 12345;
        /*/if ((mask & ownerType) == mask) {
        //true if, and only if, bitmask contains 8 | 12345
        }*/
        if ((OwnerType.Me & ownerType) != 0)
            return true;
        else return false;
    }
    public bool IsAlly() {
        if ((OwnerType.Ally & ownerType) != 0)
            return true;
        else return false;
    }
    public bool IsEnemy() {
        if ((OwnerType.Enemy & ownerType) != 0)
            return true;
        else return false;
    }
    public bool IsPlayer() {
        if ((OwnerType.Player & ownerType) != 0)
            return true;
        else return false;
    }
    public bool IsNPC() {
        if ((OwnerType.NPC & ownerType) != 0)
            return true;
        else return false;
    }
    public bool IsNeutral() {
        if ((OwnerType.Neutral & ownerType) != 0)
            return true;
        else return false;
    }
    public bool IsAI() {
        if ((OwnerType.AI & ownerType) != 0)
            return true;
        else return false;
    }
    public bool IsNone() {
        if ((OwnerType.None & ownerType) != 0)
            return true;
        else return false;
    }
    public bool EqualsByValue(System.Object obj) {
        return EqualsByValue(obj as Owner);
    }
    //use this instead of override obj, cause that is needed still to compare same component!
    public bool EqualsByValue(Owner obj) {
        //Debug.Log("ReferenceEquals:null:"+ReferenceEquals(null, obj));
        if (ReferenceEquals(null, obj))return false;
        //Debug.Log("ReferenceEquals:this:"+ReferenceEquals(this, obj));
        if (ReferenceEquals(this, obj))return true;
        //Debug.Log("GetType():this:"+ReferenceEquals(this, obj));
        if (obj.GetType() != this.GetType())return false;
        Owner c = obj as Owner;
        if ((System.Object)c == null) {
            //Debug.Log("obj is null, false");
            return false;
        }
        // Debug.Log("passed all, proper checking..."+((ownerType & c.ownerType) != 0 && ownerName == c.ownerName && teamId == c.teamId)+" vs "+c);
        //return ((ownerType & c.ownerType) != 0 && ownerName == c.ownerName && teamId == c.teamId);
        return (ownerName == c.ownerName && teamId == c.teamId);
    }
    //are these needed?:
    public bool IsInListByType(List<Owner> list) {
        foreach (Owner b in list) {
            if (EqualsByValue(b)) {
                return true;
            }
        }
        return false;
    }
    public Owner InListByType(List<Owner> list) {
        foreach (Owner b in list) {
            if (EqualsByValue(b)) {

                return b;
            }
        }
        return null;
    }
    /*     public override int GetHashCode(){
            return (int)ownerType; //or perhaps just 0?
        } */
    public override string ToString() {
        return ownerType + "-" + ownerId + "-" + teamId + "-" + ownerName;
    }
}
public enum OwnerType {
    None = 0x00,
    Me = 0x01,
    Ally = 0x02,
    Enemy = 0x04,
    Player = 0x08,
    NPC = 0x10, //16
    Neutral = 0x20,
    AI = 0x40, //64
    All = 0x7F
}