using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SkeletonRendererController : Singleton<SkeletonRendererController> {
    public List<SkeletonMecanim> passiveMechanimsList = new List<SkeletonMecanim>();
    public List<SkeletonMecanim> activeMecanimSetsListA = new List<SkeletonMecanim>();
    public List<SkeletonMecanim> activeMecanimSetsListB = new List<SkeletonMecanim>();
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
        if (!ConstantsBucket.UseAnimFrameSkipping) {
            this.enabled = false;
            return;
        }
        for (int i = 0; i < subdivisions; i++)
            activeMecanimSets.Add(new HashSet<SkeletonMecanim>());
    }
    public static void MakeSheepActive(SkeletonMecanim mecanim) {
        if (!ConstantsBucket.UseAnimFrameSkipping)return;
        if (Instance.passiveMecanims.Contains(mecanim))
            Instance.passiveMecanims.Remove(mecanim);
        if (Instance.passiveMechanimsList.Contains(mecanim))
            Instance.passiveMechanimsList.Remove(mecanim);
        if (Instance.ContainsInSets(mecanim))
            return;
        int smallest = Instance.GetSmallestSet();
        Instance.activeMecanimSets[smallest].Add(mecanim);
        if (smallest == 0)
            Instance.activeMecanimSetsListA.Add(mecanim);
        if (smallest == 1)
            Instance.activeMecanimSetsListB.Add(mecanim);
        mecanim.enabled = true;
    }
    public static void MakeSheepActive(SheepUnit sheep) {
        if (!ConstantsBucket.UseAnimFrameSkipping)return;

        SkeletonMecanim mecanim = sheep.GetComponentInChildren<SkeletonMecanim>();
        if (mecanim != null) {
            MakeSheepActive(mecanim);
        }
    }
    public static void MakeSheepIdle(SkeletonMecanim mecanim) {
        if (!ConstantsBucket.UseAnimFrameSkipping)return;
        RemoveFromSets(mecanim);
        Instance.passiveMecanims.Add(mecanim);
        Instance.passiveMechanimsList.Add(mecanim);
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
        if (Instance != null) {
            foreach (HashSet<SkeletonMecanim> hs in Instance.activeMecanimSets) {
                if (hs.Contains(skel))
                    hs.Remove(skel);
            }
            if (Instance.activeMecanimSetsListA.Contains(skel))
                Instance.activeMecanimSetsListA.Remove(skel);
            if (Instance.activeMecanimSetsListB.Contains(skel))
                Instance.activeMecanimSetsListB.Remove(skel);
        }
    }
    bool ContainsInSets(SkeletonMecanim skel) {
        if (Instance != null) {
            foreach (HashSet<SkeletonMecanim> hs in Instance.activeMecanimSets) {
                if (hs.Contains(skel))
                    return true;
            }
        }
        return false;
    }
}