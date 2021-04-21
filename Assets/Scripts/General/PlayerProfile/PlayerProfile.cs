using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
[System.Serializable]
public class PlayerProfile {
    public bool isAlive = true;
    public int eliminatedPlace = 10;
    bool _isPremium = false;
    public bool isPremium { get { return _isPremium; } }
    public void SetPremium() { _isPremium = true; }
    public Owner owner;
    public Color playerColor;
    public Sprite playerAvatarImage;
    public Playfield playfield;
    public int tutorialIndex = 1;
    int _selectedHat = 0;
    public int selectedHat {
        get { return _selectedHat; }
        set {
            SetNetworkDirty();
            _selectedHat = value;
        }
    }
    int _selectedScepter = 0;
    public int selectedScepter {
        get { return _selectedScepter; }
        set {
            SetNetworkDirty();
            _selectedScepter = value;
        }
    }
    int _seenHat = 0;
    public int seenHat {
        get { return _seenHat; }
        set {
            SetNetworkDirty();
            _seenHat = value;
        }
    }
    int _seenScepter = 0;
    public int seenScepter {
        get { return _seenScepter; }
        set {
            SetNetworkDirty();
            _seenScepter = value;
        }
    }
    public void SelectItem(KingItemType itemType, int index) {
        switch (itemType) {
            case KingItemType.hat:
                selectedHat = index;
                break;
            case KingItemType.scepter:
                selectedScepter = index;
                break;
        }
    }
    public void SeeItem(KingItemType itemType, int index) {
        switch (itemType) {
            case KingItemType.hat:
                seenHat = index;
                break;
            case KingItemType.scepter:
                seenScepter = index;
                break;
        }
    }
    public int GetSelectedItem(KingItemType itemType) {
        switch (itemType) {
            case KingItemType.hat:
                return selectedHat;
            case KingItemType.scepter:
                return selectedScepter;
        }
        return 0;
    }
    public int GetSeenItem(KingItemType itemType) {
        switch (itemType) {
            case KingItemType.hat:
                return seenHat;
            case KingItemType.scepter:
                return seenScepter;
        }
        return 0;
    }
    bool _dirty = false; // dirty for network updates and such;
    public bool IsNetworkDirty() {
        return _dirty;
    }
    public void SetNetworkDirty() {
        _dirty = true;
    }
    public void CleanNetworkDirty() {
        _dirty = false;
    }
    public float GetGrass() {
        if (playfield != null)
            return playfield.GetGrass();
        else {
            //Debug.LogWarning("playfield = null, in player profile: "+owner);   //Sort() calls this
            return 0;
        }
    }
    public float ModifyGrass(float amount) {
        if (playfield != null) {
            float adjustedAmount = playfield.ModifyGrass(amount);
            SetNetworkDirty();
            return adjustedAmount;
        } else {
            Debug.LogWarning("playfield = null, in player profile: " + owner);
            return 0;
        }
    }
    public float FillGrass() {
        if (playfield != null) {
            float prevHitpoints = playfield.currentHitpoints;
            playfield.SetGrassTo(ConstantsBucket.MaxPlayfieldGrass + 1);
            SetNetworkDirty();
            return prevHitpoints - playfield.currentHitpoints;
        } else {
            //Debug.LogWarning("playfield = null, in player profile: " + owner);
            return 0;
        }
    }

    public KingUnit kingUnit;
    public float GetHealth() {
        if (kingUnit != null)
            return kingUnit.GetHealth;
        else {
            //Debug.LogWarning("kingUnit = null, in player profile: "+owner);   //Sort() calls this
            return 0;
        }
    }

    float money;
    float earnedMoney;

    public float GetMoney() {
        return money;
    }
    public float GetMoneyEarned() {
        return earnedMoney;
    }
    public float AddMoney(float amount) {
        money += amount;
        if (amount > 0)
            earnedMoney += amount;
        SetNetworkDirty();
        return money;
    }
    public float SetMoney(float amount) {
        money = amount;
        SetNetworkDirty();
        return money;
    }
    int _permanentCrownCount = 0;
    public int permanentCrownCount {
        get { return _permanentCrownCount; }
        set {
            _permanentCrownCount = value;
            SetNetworkDirty();
        }
    }

    int crownCount;
    public int GetCrowns() {
        return crownCount;
    }
    public int AddCrowns(int amount) {
        crownCount += amount;
        SetNetworkDirty();
        return crownCount;
    }
    public int SetCrowns(int amount) {
        crownCount = amount;
        SetNetworkDirty();
        return crownCount;
    }
    public bool Buy(string priceName) {
        float price = PriceCoordinator.GetPrice(owner, priceName);
        if (price < GetMoney()) {
            AddMoney(-price);
            SetNetworkDirty();
            return true;
        }
        Debug.Log("Cannto buy [" + priceName + "], not anough money. Need [" + price + "], Have [" + GetMoney() + "]");
        return false;
    }
    public int highScore;
    public int score;

    public string Print() {
        string output = "";
        output += "HP:" + GetHealth() + " ||| ";
        output += "GR:" + GetGrass() + " ||| ";
        output += "$$:" + GetMoney() + " ||| ";
        output += owner; // + " ||| ";
        return output;
    }

    public IEnumerator DisplayUrlPicture(string url) {
        // Coint a download of the given URL
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);

        // Wait for download to complete
        yield return uwr.SendWebRequest();
        // Check if downloaded succesfully
        if (uwr.result != UnityWebRequest.Result.Success) {
            Debug.Log(uwr.error);
        } else {
            // Get downloaded asset bundle
            var texture = DownloadHandlerTexture.GetContent(uwr);
            // assign texture
            playerAvatarImage = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        }
    }
}