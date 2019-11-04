using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner : MonoBehaviour
{
    public float ownerId = 0;
    public string ownerName = "";
    public bool isHero = false;
    public float teamId = 0;

    [BitMask(typeof(OwnerType))]
    public OwnerType ownerType;
    public Owner Create(Owner newOwner){
        ownerId = newOwner.ownerId;
        ownerName = newOwner.ownerName;
        teamId = newOwner.teamId;
        ownerType = newOwner.ownerType;
        return this;
    }
    public void Create(int id, string nm, bool hero){
        ownerId = id;
        ownerName = nm;
        isHero = hero;
        ownerType = OwnerType.Player;
    }
    public void AddMask(OwnerType[] newTypes){
        ownerType.Set(newTypes);
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
    public override bool Equals(System.Object obj)
    {
        return Equals(obj as Owner);
    }
    public bool Equals(Owner obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        Owner c = obj as Owner;
        if ((System.Object)c == null)
            return false;
        return ((ownerType & c.ownerType) != 0 && ownerId == c.ownerId && teamId == c.teamId);
    }
    public override int GetHashCode(){
        return (int)ownerType; //or perhaps just 0?
    }
    public override string ToString(){
        return ownerType+"-"+ownerId+"-"+teamId;
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
