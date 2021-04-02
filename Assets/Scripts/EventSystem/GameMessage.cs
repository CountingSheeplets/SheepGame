using System;
using System.Collections.Generic;
using UnityEngine;
//idea for upgrade: this could be composite, made out of generic MessagePage or smth like it, whithin which would contain isSet states and other things if needed;
public class GameMessage : BaseMessage {
    public static GameMessage Write() {
        return new GameMessage();
    }
    //example how could handle empty messages better
    private string _strMessage;
    public string strMessage {
        get {
            if (strMessageSet)
                return _strMessage;
            else throw new Exception("No strMessage was set before request for GameMessage: " + this);
        }
        set { _strMessage = value; }
    } //must not be type of bool (if bool needed, use int)
    private bool strMessageSet; // must be private bool
    public GameMessage WithStringMessage(string value) {
        strMessage = value;
        strMessageSet = true;
        return this;
    }

    public Vector2 coordinates;
    private bool coordinatesSet;
    public GameMessage WithCoordinates(Vector2 value) {
        coordinates = value;
        coordinatesSet = true;
        return this;
    }

    public Transform transform;
    private bool transformSet;
    public GameMessage WithTransform(Transform value) {
        transform = value;
        transformSet = true;
        return this;
    }

    public GameObject gameObject;
    private bool gameObjectSet;
    public GameMessage WithGameObject(GameObject value) {
        gameObject = value;
        gameObjectSet = true;
        return this;
    }

    public Owner owner;
    private bool ownerSet;
    public GameMessage WithOwner(Owner value) {
        owner = value;
        ownerSet = true;
        return this;
    }
    public Owner targetOwner;
    private bool targetOwnerSet;
    public GameMessage WithTargetOwner(Owner value) {
        targetOwner = value;
        targetOwnerSet = true;
        return this;
    }
    public List<Owner> targetOwners;
    private bool targetOwnersSet;
    public GameMessage WithTargetOwners(List<Owner> value) {
        targetOwners = value;
        targetOwnersSet = true;
        return this;
    }
    public int intMessage;
    private bool intMessageSet;
    public GameMessage WithIntMessage(int value) {
        intMessage = value;
        intMessageSet = true;
        return this;
    }
    public int state;
    private bool stateSet;
    public GameMessage WithState(bool value) {
        stateSet = true;
        if (value)
            state = 1;
        else
            state = 0;
        return this;
    }
    public Swipe swipe;
    private bool swipeSet;
    public GameMessage WithSwipe(Swipe value) {
        swipe = value;
        swipeSet = true;
        return this;
    }
    public Playfield playfield;
    private bool playfieldSet;
    public GameMessage WithPlayfield(Playfield value) {
        playfield = value;
        playfieldSet = true;
        return this;
    }
    public SheepUnit sheepUnit;
    private bool sheepUnitSet;
    public GameMessage WithSheepUnit(SheepUnit value) {
        sheepUnit = value;
        sheepUnitSet = true;
        return this;
    }
    public KingUnit kingUnit;
    private bool kingUnitSet;
    public GameMessage WithKingUnit(KingUnit value) {
        kingUnit = value;
        kingUnitSet = true;
        return this;
    }
    public float floatMessage;
    private bool floatMessageSet;
    public GameMessage WithFloatMessage(float value) {
        floatMessage = value;
        floatMessageSet = true;
        return this;
    }
    public float targetFloatMessage;
    private bool targetFloatMessageSet;
    public GameMessage WithTargetFloat(float value) {
        targetFloatMessage = value;
        targetFloatMessageSet = true;
        return this;
    }
    public float deltaFloat;
    private bool deltaFloatSet;
    public GameMessage WithDeltaFloat(float value) {
        deltaFloat = value;
        deltaFloatSet = true;
        return this;
    }
    public PlayerProfile playerProfile;
    private bool playerProfileSet;
    public GameMessage WithPlayerProfile(PlayerProfile value) {
        playerProfile = value;
        playerProfileSet = true;
        return this;
    }
    public UpgradeType upgradeType;
    private bool upgradeTypeSet;
    public GameMessage WithUpgradeType(UpgradeType value) {
        upgradeType = value;
        upgradeTypeSet = true;
        return this;
    }
    public List<SheepUnit> sheepUnits;
    private bool sheepUnitsSet;
    public GameMessage WithSheepUnits(List<SheepUnit> value) {
        sheepUnits = value;
        sheepUnitSet = true;
        return this;
    }
    public KingItemType kingItemType;
    private bool kingItemTypeSet;
    public GameMessage WithKingItemType(KingItemType value) {
        kingItemType = value;
        kingItemTypeSet = true;
        return this;
    }
    public SheepType sheepType;
    private bool sheepTypeSet;
    public GameMessage WithSheepType(SheepType value) {
        sheepType = value;
        sheepTypeSet = true;
        return this;
    }
}