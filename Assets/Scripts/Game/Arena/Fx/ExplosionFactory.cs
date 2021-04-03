using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ExplosionFactory : MonoBehaviour {
    public List<Explosion> explosions = new List<Explosion>();
    public Dictionary<FxType, PersistentFactory> factories = new Dictionary<FxType, PersistentFactory>();

    private static ExplosionFactory _instance;
    public static ExplosionFactory Instance {
        get {

            if (_instance != null)
                return _instance;
            _instance = FindObjectOfType(typeof(ExplosionFactory))as ExplosionFactory;
            return _instance;
        }
    }
    public static GameObject GetOrCreateFx(FxType type, float timeout) {
        GameObject fxObj = Instance.factories[type].GetOrCreateItem();
        if (!fxObj.name.Contains("ParticleFX"))
            fxObj.name = "ParticleFX_" + type.ToString() + "_" + fxObj.name;

        Instance.HideFX(type, fxObj, timeout);
        return fxObj;
    }
    void Awake() {
        foreach (Explosion expl in explosions) {
            PersistentFactory newFact = gameObject.AddComponent<PersistentFactory>()as PersistentFactory;
            factories.Add(expl.fxType, newFact);
            newFact.itemPrefab = expl.prefab;
        }
    }
    void Start() {
        foreach (KeyValuePair<FxType, PersistentFactory> pair in factories) {
            //Debug.Log(pair.Key + "   " + pair.Value);
        }
    }
    public void HideFX(FxType type, GameObject obj, float timeout) {
        StartCoroutine(HideAfterTimeout(type, obj, timeout));
    }
    public IEnumerator HideAfterTimeout(FxType type, GameObject obj, float timeout) {
        yield return new WaitForSeconds(timeout);
        Instance.factories[type].HideObject(obj);
    }

    public GameObject GetPrefab(FxType type) {
        return explosions.Where(x => x.fxType == type).FirstOrDefault().prefab;
    }
}

[System.Serializable]
public class Explosion {
    public FxType fxType;
    public GameObject prefab;
}
public enum FxType {
    None,
    SheepDeath,
    SheepGrassLand,
    KingSmash
}