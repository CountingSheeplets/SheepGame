using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile
{
    public bool isAlive = true;
    public Owner owner;
    public Color playerColor;
    public Sprite playerAvatarImage;
    public Playfield playfield;
    public float GetGrass(){
        if (playfield != null)
            return playfield.GetHitpoints();
        else{
            //Debug.LogWarning("playfield = null, in player profile: "+owner);   //Sort() calls this
            return 0;
        }
    }
    public float ModifyGrass(float amount){
        if (playfield != null){
            float adjustedAmount = playfield.AdjustHitPoints(amount);
            SetDirty();
            return adjustedAmount;
        }else{
            Debug.LogWarning("playfield = null, in player profile: "+owner);
            return 0;
        }
    }
    public float FillGrass(){
        if (playfield != null){
            float prevHitpoints = playfield.currentHitpoints;
            float amount = playfield.AdjustHitPoints(500);
            playfield.AdjustHitPoints(amount);
            SetDirty();
            return prevHitpoints - playfield.currentHitpoints;
        }else{
            Debug.LogWarning("playfield = null, in player profile: "+owner);
            return 0;
        }
    }

    public KingUnit kingUnit;
    public float GetHealth(){
        if(kingUnit != null)
            return kingUnit.GetHealth;
        else{
            //Debug.LogWarning("kingUnit = null, in player profile: "+owner);   //Sort() calls this
            return 0;
        }
    }

    float money;
    float earnedMoney;

    public float GetMoney(){
        return money;
    }
    public float GetMoneyEarned(){
        return earnedMoney;
    }
    public float AddMoney(float amount){
        money += amount;
        if(amount > 0)
            earnedMoney+=amount;
        SetDirty();
        return money;
    }
    public float SetMoney(float amount){
        money = amount;
        SetDirty();
        return money;
    }

    public int crownCount;
    public int GetStars(){
        return crownCount;
    }
    public int AddCrowns(int amount){
        crownCount += amount;
        SetDirty();
        return crownCount;
    }
    public int SetCrowns(int amount){
        crownCount = amount;
        SetDirty();
        return crownCount;
    }
    public bool Buy(string priceName){
        float price = PriceCoordinator.GetPrice(owner, priceName);
        if(price < GetMoney()){
            AddMoney(-price);
            SetDirty();
            return true;
        }
        Debug.Log("Cannto buy ["+priceName+"], not anough money. Need ["+price+"], Have ["+GetMoney()+"]");
        return false;  
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
		// Crownt a download of the given URL
		WWW www = new WWW (url);
		
		// Wait for download to complete
		yield return www;
		
		// assign texture
        playerAvatarImage = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
	}
    public void SetDirty(){
        if(isAlive)
            EventCoordinator.TriggerEvent(EventName.System.Player.ProfileUpdate(), GameMessage.Write().WithPlayerProfile(this).WithOwner(owner));
    }
}
