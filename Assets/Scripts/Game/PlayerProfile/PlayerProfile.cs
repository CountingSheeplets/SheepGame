using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile
{
    public Owner owner;

    public Playfield playfield;
    public float GetGrass(){
        if (playfield != null)
            return playfield.GetHitpoints();
        else{
            Debug.LogWarning("playfield = null, in player profile: "+owner);
            return 0;
        }
    }
    public float ModifyGrass(float amount){
        if (playfield != null)
            return playfield.AdjustHitPoints(amount);
        else{
            Debug.LogWarning("playfield = null, in player profile: "+owner);
            return 0;
        }
    }

    public KingUnit kingUnit;
    public float GetHealth(){
        if(kingUnit != null)
            return kingUnit.GetHealth;
        else{
            Debug.LogWarning("kingUnit = null, in player profile: "+owner);
            return 0;
        }
    }

    float money;
    public float GetMoney(){
        return money;
    }
    public float AddMoney(float amount){
        money += amount;
        return money;
    }
    public float SetMoney(float amount){
        money = amount;
        return money;
    }

    public int starCount;
    public int GetStarCount(){
        return starCount;
    }
    public int AddStarCount(int amount){
        starCount += amount;
        return starCount;
    }
    public int SetStarCount(int amount){
        starCount = amount;
        return starCount;
    }

    public int highScore;
    public int score;

    public string Print(){
        string output = "";
        output += "HP:"+GetHealth() + " ||| ";
        output += "GR:"+GetGrass() + " ||| ";
        output += "$$:"+GetMoney() + " ||| ";
        output += owner;// + " ||| ";
        return output;
    }
}
