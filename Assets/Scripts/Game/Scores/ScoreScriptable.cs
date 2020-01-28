using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Score", order = 2)]
public class ScoreScriptable : ScriptableObject
{
    [SerializeField]
    [StringInList(typeof(PropertyDrawersHelper), "AllScoreNames")]
    public string scoreName;
    public int reward;
    public string description;
    public int rewardDelta;
    public ScoreType scoreType;
    public Sprite icon;
}