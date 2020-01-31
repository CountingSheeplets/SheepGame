using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public float hdrIntensity = 2f;
    public GameObject debrisParticles;
    public DeathFx txToAddOnDeath;
    public bool isClone = false;
    public Color playerColor = Color.white;
    void CreateDebris(){
        if(debrisParticles == null)
            return;
        GameObject newDebris = Instantiate(debrisParticles);
        newDebris.transform.position = transform.position;
        newDebris.transform.rotation = transform.rotation;
        newDebris.transform.parent = ArenaCoordinator.Instance.transform;
        newDebris.gameObject.name = gameObject.name+("(ParticleFX)");
    }
    void Start(){
        Playfield pl = transform.GetComponentInParent<Playfield>();
        if(pl != null)
            playerColor = pl.owner.GetPlayerProfile().playerColor;
    }
    void OnDestroy(){
        if (isClone || ArenaCoordinator.Instance == null || GameStateView.HasState(GameState.ended))
            return;
        Debug.Log("dead, creating fx...");
        isClone = true;

        CreateDebris();

        GameObject newTempObj = Instantiate(gameObject, ArenaCoordinator.GetFxContainer);
        newTempObj.GetComponent<Destructable>().isClone = true;
        newTempObj.name = gameObject.name+("(TempDeathFX)");
        newTempObj.transform.position = transform.position;
        newTempObj.transform.localScale = new Vector3( transform.lossyScale.x, transform.lossyScale.y, 1);

        DeathFx fx = newTempObj.AddComponent(txToAddOnDeath.GetType()) as DeathFx;
        if(playerColor != Color.white)
            fx.SetColorHDR(playerColor, hdrIntensity); 
        newTempObj.DestroyAllMonosExcept<DeathFx>();
        newTempObj.SetActive(true);
    }
}
