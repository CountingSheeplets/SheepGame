using System.Collections.Generic;
using UnityEngine;
public class GameMessage {
    public static GameMessage Write() {
        return new GameMessage();
    }
    public string strMessage;
    public GameMessage WithStringMessage(string value) {
        strMessage = value;
        return this;
    }

    public Vector2 coordinates;
    public GameMessage WithCoordinates(Vector2 value) {
        coordinates = value;
        return this;
    }

    public Transform transform;
    public GameMessage WithTransform(Transform value) {
        transform = value;
        return this;
    }
    /*     public List<Transform> transforms;
        public GameMessage WithTransforms(List<Transform> value)
        {
            transforms = value;
            return this;
        } */

    public GameObject gameObject;
    public GameMessage WithGameObject(GameObject value) {
        gameObject = value;
        return this;
    }

    public Owner owner;
    public GameMessage WithOwner(Owner value) {
        owner = value;
        return this;
    }
    public Owner targetOwner;
    public GameMessage WithTargetOwner(Owner value) {
        targetOwner = value;
        return this;
    }
    public List<Owner> targetOwners;
    public GameMessage WithTargetOwners(List<Owner> value) {
        targetOwners = value;
        return this;
    }
    public int intMessage;
    public GameMessage WithIntMessage(int value) {
        intMessage = value;
        return this;
    }
    public bool state;
    public GameMessage WithState(bool value) {
        state = value;
        return this;
    }
    public Swipe swipe;
    public GameMessage WithSwipe(Swipe value) {
        swipe = value;
        return this;
    }
    public Playfield playfield;
    public GameMessage WithPlayfield(Playfield value) {
        playfield = value;
        return this;
    }
    public SheepUnit sheepUnit;
    public GameMessage WithSheepUnit(SheepUnit value) {
        sheepUnit = value;
        return this;
    }
    public KingUnit kingUnit;
    public GameMessage WithKingUnit(KingUnit value) {
        kingUnit = value;
        return this;
    }
    public float floatMessage;
    public GameMessage WithFloatMessage(float value) {
        floatMessage = value;
        return this;
    }
    public float targetFloatMessage;
    public GameMessage WithTargetFloat(float value) {
        targetFloatMessage = value;
        return this;
    }
    public float deltaFloat;
    public GameMessage WithDeltaFloat(float value) {
        deltaFloat = value;
        return this;
    }
    public PlayerProfile playerProfile;
    public GameMessage WithPlayerProfile(PlayerProfile value) {
        playerProfile = value;
        return this;
    }
    public UpgradeType upgradeType;
    public GameMessage WithUpgradeType(UpgradeType value) {
        upgradeType = value;
        return this;
    }
    public List<SheepUnit> sheepUnits;
    public GameMessage WithSheepUnits(List<SheepUnit> value) {
        sheepUnits = value;
        return this;
    }
    public KingItemType kingItemType;
    public GameMessage WithKingItemType(KingItemType value) {
        kingItemType = value;
        return this;
    }
    public SheepType sheepType;
    public GameMessage WithSheepType(SheepType value) {
        sheepType = value;
        return this;
    }
}