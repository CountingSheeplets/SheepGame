using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KingSmashTimer : MonoBehaviour {
    //public List<Vector2> destinations = new List<Vector2>();
    //public List<Vector2> initPos = new List<Vector2>();
    Owner owner;
    KingUnit king;
    float counter = 0f;
    float smashTime;
    GameMessage smashMsg;

    void Start() {
        smashTime = ConstantsBucket.SmashSpeed;
        if (king == null)king = GetComponent<KingUnit>();
        if (owner == null)owner = king.owner;
        EventCoordinator.StartListening(EventName.System.King.StartSmash(), OnStartSmash);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.StartSmash(), OnStartSmash);
    }
    void OnStartSmash(GameMessage msg) {
        if (owner.EqualsByValue(msg.owner)) {
            smashMsg = msg;
            king.SetUsingAbility();
        }
    }
    void Update() {
        //for (int i = 0; i < destinations.Count; i++)
        //    Debug.DrawLine(initPos[i], destinations[i], king.owner.GetPlayerProfile().playerColor);

        if (king.GetIsUsingAbility())
            counter += Time.deltaTime;
        if (counter >= smashTime) {
            List<SheepUnit> sheepWithinRange = SheepCoordinator.GetSheepInField(king.myPlayfield)
                .Where(x => !x.owner.EqualsByValue(king.owner) && x.sheepType != SheepType.Tank)
                //.Where(x => (x.GetComponent<Transform>().position - transform.position).magnitude <= ConstantsBucket.KingSmiteRange)
                //.Where(x => !x.isReadyToFly)
                .ToList();
            string smashedSHeep = "";
            foreach (SheepUnit sheep in sheepWithinRange) { //SheepCoordinator.GetSheepInField(king.myPlayfield).Where(x => !x.owner.EqualsByValue(king.owner))) {
                smashedSHeep += sheep.name + "  ";
            }
            Debug.Log("sheep found in field to smash: " + smashedSHeep);
            counter = 0f;
            king.StopUsingAbility();
            EventCoordinator.TriggerEvent(EventName.System.King.Smashed(), smashMsg.WithSheepUnits(sheepWithinRange).WithCoordinates(king.transform.localPosition).WithOwner(king.owner));
        }
    }
}