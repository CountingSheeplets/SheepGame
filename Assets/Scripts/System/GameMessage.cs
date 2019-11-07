using UnityEngine;
using System.Collections.Generic;
public class GameMessage
{
    public static GameMessage Write()
    {
        return new GameMessage();
    }
    public string strMessage;
    public GameMessage WithStringMessage(string value)
    {
        strMessage = value;
        return this;
    }

    public Vector2 coordinates;
    public GameMessage WithCoordinates(Vector2 value)
    {
        coordinates = value;
        return this;
    }

    public Transform transform;
    public GameMessage WithTransform(Transform value)
    {
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
    public GameMessage WithGameObject(GameObject value){
        gameObject = value;
        return this;
    }
    public CooldownGroup cooldownGroup;
    public GameMessage WithCooldownGroup(CooldownGroup value){
        cooldownGroup = value;
        return this;
    }

    public Owner owner;
    public GameMessage WithOwner(Owner value){
        owner = value;
        return this;
    }

    public int intMessage;
    public GameMessage WithIntMessage(int value){
        intMessage = value;
        return this;
    }
    public bool state;
    public GameMessage WithState(bool value){
        state = value;
        return this;
    }
    public Swipe swipe;
    public GameMessage WithSwipe(Swipe value){
        swipe = value;
        return this;
    }
    public Playfield playfield;
    public GameMessage WithPlayfield(Playfield value){
        playfield = value;
        return this;
    }
    public SheepUnit sheepUnit;
    public GameMessage WithSheepUnit(SheepUnit value){
        sheepUnit = value;
        return this;
    }
}