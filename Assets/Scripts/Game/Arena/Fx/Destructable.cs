using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//vis tiek sukuria modelį
//nesukuria particle

//karaliaus deatch galima turėt žvaigždelių field particle effektą

public class Destructable : MonoBehaviour {
    public static bool Quitting { get; set; }
    public float hdrIntensity = 2f;
    public GameObject debrisParticles;
    public DeathFx txToAddOnDeath;
    public bool isClone = false;
    public Color playerColor = Color.white;
    void CreateDebris() {
        if (debrisParticles == null)
            return;
        GameObject newDebris = Instantiate(debrisParticles);
        newDebris.transform.position = transform.position;
        newDebris.transform.rotation = transform.rotation;
        newDebris.transform.parent = ArenaCoordinator.GetFxContainer;
        ParticleSystemRenderer[] systems = newDebris.GetComponentsInChildren<ParticleSystemRenderer>();
        foreach (ParticleSystemRenderer system in systems) {
            SortingGroup sGroup = GetComponent<SortingGroup>();
            if (sGroup)
                system.sortingOrder = sGroup.sortingOrder + 1;
        }
        newDebris.gameObject.name = gameObject.name + ("(ParticleFX)");
        Destroy(newDebris, 1f);
    }
    void Start() {
        Playfield pl = transform.GetComponentInParent<Playfield>();
        if (pl != null)
            playerColor = pl.owner.GetPlayerProfile().playerColor;
    }
    void OnDestroy() {
        if (GameStateView.HasState(GameState.gameReloaded) || GameStateView.HasState(GameState.ended))
            return;
        if (isClone ||
            GameStateView.HasState(GameState.ended)
        )
            return;
        if (GameStateView.GetGameState() == 0)
            return;
        if (ArenaCoordinator.Instance == null)
            return;

        CreateDebris();
        if (txToAddOnDeath == null)
            return;
        Debug.Log("dead, creating fx...");
        isClone = true;

        GameObject newTempObj = Instantiate(gameObject, ArenaCoordinator.GetFxContainer);
        newTempObj.GetComponent<Destructable>().isClone = true;
        newTempObj.name = gameObject.name + ("(TempDeathFX)");
        newTempObj.transform.position = transform.position;
        newTempObj.transform.localScale = new Vector3(transform.lossyScale.x, transform.lossyScale.y, 1);

        DeathFx fx = newTempObj.AddComponent(txToAddOnDeath.GetType())as DeathFx;
        if (playerColor != Color.white)
            fx.SetColorHDR(playerColor, hdrIntensity);
        newTempObj.DestroyAllMonosExcept<DeathFx>();
        newTempObj.SetActive(true);
    }
    void OnApplicationQuit() {
        Quitting = true;
    }
}