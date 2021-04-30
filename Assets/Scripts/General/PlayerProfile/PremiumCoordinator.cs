using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiumCoordinator : Singleton<PremiumCoordinator> {
    List<int> premium_ids = new List<int>();

    public static void AddPremium(int id) {
        if (!Instance.premium_ids.Contains(id)) {
            Instance.premium_ids.Add(id);
        }
    }
    public static bool IsPremium(int id) {
        Debug.Log(Instance.premium_ids.ToString());
        string ids = "premiums: ";
        foreach (int i in Instance.premium_ids) {
            ids += i.ToString() + "; ";
        }
        Debug.Log(ids);
        if (Instance.premium_ids.Contains(id))
            return true;
        return false;
    }
}