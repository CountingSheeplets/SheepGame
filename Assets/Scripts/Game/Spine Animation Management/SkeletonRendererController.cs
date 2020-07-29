using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SkeletonRendererController : Singleton<SkeletonRendererController> {

    public HashSet<SkeletonMecanim> passiveMecanims = new HashSet<SkeletonMecanim>();
    //There's a task created, after subdivs are known, change List>Array for speed.
    public List<HashSet<SkeletonMecanim>> activeMecanimSets = new List<HashSet<SkeletonMecanim>>();

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
        Instance.activeMecanimSets[smallest].Add(mecanim);
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
            }
    }
}