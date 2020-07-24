using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SkeletonRendererController : Singleton<SkeletonRendererController> {

    public HashSet<SkeletonMecanim> passiveMecanims = new HashSet<SkeletonMecanim>();
    public List<HashSet<SkeletonMecanim>> activeMecanimSets = new List<HashSet<SkeletonMecanim>>();

    /*     public List<SkeletonMecanim> activesA = new List<SkeletonMecanim>();
        public List<SkeletonMecanim> activesB = new List<SkeletonMecanim>();
     */
    public int subdivisions = 2;
    public int currentStep = 0;
    int previousStep {
        get {
            if (currentStep == 0)
                return subdivisions - 1;
            else
                return currentStep - 1;
        }
    }
    void Start() {
        for (int i = 0; i < subdivisions; i++)
            activeMecanimSets.Add(new HashSet<SkeletonMecanim>());
    }
    public static void MakeSheepActive(SkeletonMecanim mecanim) {
        Instance.passiveMecanims.Remove(mecanim);
        int smallest = Instance.GetSmallestSet();
        // Debug.Log(smallest);
        //RemoveFromSets(mecanim);
        Instance.activeMecanimSets[smallest].Add(mecanim);
        /*         switch (smallest) {
                    case 0:
                        if (!Instance.activesA.Contains(mecanim))
                            Instance.activesA.Add(mecanim);
                        break;
                    case 1:
                        if (!Instance.activesB.Contains(mecanim))
                            Instance.activesB.Add(mecanim);
                        break;
                } */
        mecanim.enabled = true;
    }
    public static void MakeSheepActive(SheepUnit sheep) {
        SkeletonMecanim mecanim = sheep.GetComponentInChildren<SkeletonMecanim>();
        if (mecanim != null) {
            MakeSheepActive(mecanim);
        }
    }
    public static void MakeSheepIdle(SkeletonMecanim mecanim) {
        RemoveFromSets(mecanim);
        Instance.passiveMecanims.Add(mecanim);
        mecanim.enabled = false;
    }
    void Update() {
        foreach (SkeletonMecanim skel in activeMecanimSets[currentStep]) {
            skel.enabled = true;
        }
    }

    void LateUpdate() {
        foreach (SkeletonMecanim skel in activeMecanimSets[previousStep]) {
            skel.enabled = false;
        }
        currentStep++;
        if (currentStep >= subdivisions) {
            currentStep = 0;
        }
    }

    int GetSmallestSet() {
        int count = 999;
        int index = 0;
        for (int i = 0; i < activeMecanimSets.Count; i++) {
            if (activeMecanimSets[i].Count < count) {
                count = activeMecanimSets[i].Count;
                index = i;
            }
        }
        return index;
    }
    public static void RemoveFromSets(SkeletonMecanim skel) {
        if (Instance != null)
            foreach (HashSet<SkeletonMecanim> hs in Instance.activeMecanimSets) {
                hs.Remove(skel);
                /*                         if (Instance.activesA.Contains(skel))
                                            Instance.activesA.Remove(skel);
                                        if (Instance.activesB.Contains(skel))
                                            Instance.activesB.Remove(skel);
                                             */
            }
    }
    void ClearDead() {
        foreach (HashSet<SkeletonMecanim> hs in activeMecanimSets) {
            foreach (SkeletonMecanim mec in hs) {
                if (mec == null)
                    hs.Remove(mec);
            }
        }
    }
}