using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile
{
    public bool isAlive = false;
    public Owner owner;
    public Color playerColor;
    public Sprite playerAvatarImage;
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

    public IEnumerator DisplayUrlPicture (string url) {
		// Start a download of the given URL
		WWW www = new WWW (url);
		
		// Wait for download to complete
		yield return www;
		
		// assign texture
        playerAvatarImage = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
	}
}
