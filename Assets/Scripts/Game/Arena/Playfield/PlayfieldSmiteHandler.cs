using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfieldSmiteHandler : MonoBehaviour {
    bool trigger;
    float timeToDestroy = 3f;
    public float visibility = 0f;
    SpriteRenderer rend;
    Owner owner;
    public List<Texture> crackTextures;
    void Start() {
        EventCoordinator.StartListening(EventName.System.King.Smashed(), OnKingSmashed);
        rend = GetComponent<SpriteRenderer>();
        owner = GetComponentInParent<Owner>();
        rend.material.SetFloat("_RandomSeed", Random.Range(0, 10));
        rend.material.SetFloat("_CrackFill", 1);
    }
    void OnDestroy() {
        EventCoordinator.StopListening(EventName.System.King.Smashed(), OnKingSmashed);
    }
    void OnKingSmashed(GameMessage msg) {
        if (!owner.EqualsByValue(msg.owner))
            return;
        SelectRandomSprite();
        trigger = true;
        visibility = 1f;
        Vector2 relativePosition = msg.coordinates / ConstantsBucket.GridSize * 2.2f;
        rend.material.SetVector("_SmashPosition", relativePosition);
        rend.material.SetFloat("_SmashCrackLevel", visibility);
    }
    void Update() {
        if (trigger) {
            visibility -= Time.deltaTime / timeToDestroy;
            if (visibility >= 0)
                rend.material.SetFloat("_SmashCrackLevel", visibility);
            else {
                rend.material.SetFloat("_SmashCrackLevel", 0);
                trigger = false;
            }
        }
    }

    void SelectRandomSprite() {
        if (crackTextures.Count <= 0) {
            Debug.LogWarning("Zero Textures to select a random from, not randoming any tex...");
            return;
        }
        int index = Random.Range(0, crackTextures.Count - 1);
        rend.material.SetTexture("_SmashCrackTexture", crackTextures[index]);
    }
}