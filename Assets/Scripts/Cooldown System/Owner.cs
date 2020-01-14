using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner : MonoBehaviour
{
    public string ownerId = "";
    public int deviceId = 0;
    public string ownerName = "";
    public bool isHero = false;
    public float teamId = 0;
    public bool connected = false;
    bool _ready = false;
    public bool ready {
        get{ return _ready;}
        set{ _ready = value;
            PlayerOwnerTile tile = GetComponent<PlayerOwnerTile>();
            if(tile != null)
			    tile.Ready(value);
            NetworkCoordinator.SendConfirmReady(deviceId, value);
        }
    }

    [BitMask(typeof(OwnerType))]
    public OwnerType ownerType;
    public Owner Create(Owner newOwner){
        ownerId = newOwner.ownerId;
        ownerName = newOwner.ownerName;
        teamId = newOwner.teamId;
        ownerType = newOwner.ownerType;
        if(IsPlayer())
            connected = true;
        return this;
    }
    public void Create(string id, string nm, bool hero, int devId){
        ownerId = id;
        ownerName = nm;
        isHero = hero;
        ownerType = OwnerType.Player;
        deviceId = devId;
        teamId = OwnersCoordinator.GetOwners().Count;
        if(IsPlayer())
            connected = true;
    }
    public void AddMask(OwnerType[] newTypes){
        ownerType.Set(newTypes);
    }
    public KingUnit GetKing(){
        return KingCoordinator.GetKing(this);
    }
    public Playfield GetPlayfield(){
        return ArenaCoordinator.GetPlayfield(this);
    }
    public PlayerProfile GetPlayerProfile(){
        return PlayerProfileCoordinator.GetProfile(this);
    }
    public string GetToken(int crownCount){
        string token = "sheep"+ownerId+"_!"+crownCount;
        //Debug.Log("token:"+token);
        return CryptoHelper.md5(token);
    }
    public bool IsMine(){
        //int mask = 0x01 | 12345;
        /*/if ((mask & ownerType) == mask) {
        //true if, and only if, bitmask contains 8 | 12345
        }*/
        if ((OwnerType.Me & ownerType) != 0)
            return true;
        else return false;
    }
    public bool IsAlly(){
        if ((OwnerType.Ally & ownerType) != 0)
            return true;
        else return false;
    }  
    public bool IsEnemy(){
        if ((OwnerType.Enemy & ownerType) != 0)
            return true;
        else return false;
    }   
    public bool IsPlayer(){
        if ((OwnerType.Player & ownerType) != 0)
            return true;
        else return false;
    }
    public bool IsNPC(){
        if ((OwnerType.NPC & ownerType) != 0)
            return true;
        else return false;
    }
    public bool IsNeutral(){
        if ((OwnerType.Neutral & ownerType) != 0)
            return true;
        else return false;
    }
    public bool IsAI(){
        if ((OwnerType.AI & ownerType) != 0)
            return true;
        else return false;
    }
    public bool IsNone(){
        if ((OwnerType.None & ownerType) != 0)
            return true;
        else return false;
    }
    public bool EqualsByValue(System.Object obj)
    {
        return EqualsByValue(obj as Owner);
    }
        //use this instead of override obj, cause that is needed still to compare same component!
    public bool EqualsByValue(Owner obj)
    {
            //Debug.Log("ReferenceEquals:null:"+ReferenceEquals(null, obj));
        if (ReferenceEquals(null, obj)) return false;
            //Debug.Log("ReferenceEquals:this:"+ReferenceEquals(this, obj));
        if (ReferenceEquals(this, obj)) return true;
            //Debug.Log("GetType():this:"+ReferenceEquals(this, obj));
        if (obj.GetType() != this.GetType()) return false;
        Owner c = obj as Owner;
        if ((System.Object)c == null){
            //Debug.Log("obj is null, false");
            return false;
        }
       // Debug.Log("passed all, proper checking..."+((ownerType & c.ownerType) != 0 && ownerName == c.ownerName && teamId == c.teamId)+" vs "+c);
        return ((ownerType & c.ownerType) != 0 && ownerName == c.ownerName && teamId == c.teamId);
    }
    //are these needed?:
    public bool IsInListByType(List<Owner> list){
        foreach(Owner b in list){
            if (EqualsByValue(b)){
                return true;
            }
        }
        return false;
    }
    public Owner InListByType(List<Owner> list){
        foreach(Owner b in list){
            if (EqualsByValue(b)){

                return b;
            }
        }
        return null;
    }
/*     public override int GetHashCode(){
        return (int)ownerType; //or perhaps just 0?
    } */
    public override string ToString(){
        return ownerType+"-"+ownerId+"-"+teamId+"-"+ownerName;
    }
}
public enum OwnerType{
    None        = 0x00,
    Me          = 0x01,
    Ally        = 0x02,
    Enemy       = 0x04,
    Player      = 0x08,
    NPC         = 0x10, //16
    Neutral     = 0x20,
    AI          = 0x40, //64
    All         = 0x7F
}
