using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public static class PropertyDrawersHelper {
  #if UNITY_EDITOR

  public static string[] AllSceneNames()
  {
    var temp = new List<string>();
    foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
    {
      if (S.enabled)
      {
        string name = S.path.Substring(S.path.LastIndexOf('/')+1);
        name = name.Substring(0,name.Length-6);
        temp.Add(name);
      }
    }
    return temp.ToArray();
  }
  public static string[] AllPriceNames(){
    return PriceName.Get().ToArray();
  }
  #endif
}