using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Playfield : MonoBehaviour {
    public Owner owner;

    public FieldCorners fieldCorners;

    public GameObject fenceTilePrefab;
    public GameObject backgroundSprite;
    public float currentHitpoints = 0f;
    public bool generateFence = true;
    public Transform sheepParent; // used by SheepFactory

    public bool isAnimating = false;
    public Playfield Init() {
        GameObject newPlayfieldGO = Instantiate(gameObject);
        Playfield playfield = newPlayfieldGO.GetComponent<Playfield>();
        return playfield;
    }

    void Start() {
        fieldCorners = new FieldCorners(ArenaCoordinator.fieldSize);
        EventCoordinator.StartListening(EventName.Input.StartGame(), OnStart);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.Input.StartGame(), OnStart);
        if (ScoreCoordinator.Instance == null) {
            Debug.Log("ScoreCoordinator.Instance is null!");
            return;
        } else {
            ScoreCoordinator.SetTechTier2Counts(owner, GetComponent<SheepUpgrade>().tier2UpgradeCount);
        }
    }
    void OnStart(GameMessage msg) {
        //generate fences around field: //this has to be done via a shader too
        //FenceController fenceController = GetComponentInChildren<FenceController>();
        //if (generateFence) {
        //}
        currentHitpoints = ConstantsBucket.MaxPlayfieldGrass;
        owner.GetPlayerProfile().FillGrass();
    }

    public float GetGrass() {
        return currentHitpoints;
    }

    public void SetGrassTo(float targetAmount) {
        float delta = targetAmount - currentHitpoints;
        currentHitpoints = Mathf.Clamp(targetAmount, 0, ConstantsBucket.MaxPlayfieldGrass);
        EventCoordinator.TriggerEvent(EventName.System.Economy.GrassChanged(), GameMessage.Write().WithDeltaFloat(delta).WithTargetFloat(currentHitpoints).WithOwner(owner));
        IsKingVulnerable();
    }

    public float ModifyGrass(float amount) {
        currentHitpoints = Mathf.Clamp(amount + currentHitpoints, 0, ConstantsBucket.MaxPlayfieldGrass);
        EventCoordinator.TriggerEvent(EventName.System.Economy.GrassChanged(), GameMessage.Write().WithDeltaFloat(amount).WithTargetFloat(currentHitpoints).WithOwner(owner));
        IsKingVulnerable();
        return currentHitpoints;
    }
    bool IsKingVulnerable() {
        KingUnit king = owner.GetKing();
        if (king == null)
            return true;
        if (currentHitpoints <= 0) {
            currentHitpoints = 0;
            king.isVulnerable = true;
            return true;
        } else {
            king.isVulnerable = false;
            return false;
        }
    }
    public override string ToString() {
        return "[" + gameObject.name + "]";
    }
}