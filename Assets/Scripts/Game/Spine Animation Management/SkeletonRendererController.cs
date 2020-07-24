using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SkeletonRendererController : Singleton<SkeletonRendererController> {

    public HashSet<SkeletonMecanim> activeMecanims = new HashSet<SkeletonMecanim>();
    public HashSet<SkeletonMecanim> passiveMecanims = new HashSet<SkeletonMecanim>();
    public int subdivisions = 2;

    public static void MakeSheepActive(SkeletonMecanim mecanim) {
        Instance.passiveMecanims.Remove(mecanim);
        Instance.activeMecanims.Add(mecanim);
        mecanim.enabled = true;
    }
    public static void MakeSheepActive(SheepUnit sheep) {
        SkeletonMecanim mecanim = sheep.GetComponentInChildren<SkeletonMecanim>();
        if (mecanim != null) {
            Instance.activeMecanims.Add(mecanim);
            mecanim.enabled = true;
        }
    }
    public static void MakeSheepIdle(SkeletonMecanim mecanim) {
        Instance.activeMecanims.Remove(mecanim);
        Instance.passiveMecanims.Add(mecanim);
        mecanim.enabled = false;
    }
    /*     void Start() {

        }

        void Update() {
            foreach (SkeletonMecanim mec in activeMecanims) {
                //mec.enabled = true;
            }
        } */
}