using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Score", order = 2)]
public class ScoreScriptable : ScriptableObject {
#if UNITY_EDITOR
    [StringInList(typeof(PropertyDrawersHelper), "AllScoreNames")]
#endif
    [SerializeField]
    public string scoreName;
    public int reward;
    public string description;
    public int rewardDelta;
    public string wordDelta;
    public ScoreType scoreType;
    public Sprite icon;
}