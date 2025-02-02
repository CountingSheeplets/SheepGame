using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandNameBucket : Singleton<RandNameBucket>
{
    public List<string> PlayerNames = new List<string>();
    public List<string> UsedNames = new List<string>();
    public List<string> UnusedNames = new List<string>();

    private void Start() {
        UnusedNames = new List<string>(PlayerNames);
    }

    public static string GetRandPlayerName() {
        if (Instance.UnusedNames.Count == 0) {
            Instance.UnusedNames = new List<string>(Instance.PlayerNames);
        }
        int randIndex = Random.Range(0, Instance.UnusedNames.Count);
        string randName = Instance.UnusedNames[randIndex];
        Instance.UnusedNames.RemoveAt(randIndex);
        Instance.UsedNames.Add(randName);
        return randName;
    }
}
