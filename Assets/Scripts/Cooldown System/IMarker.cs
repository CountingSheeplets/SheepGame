using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Allows the Marker object to get mass-marked/unmarked and stored
/// </summary>
public interface IMarker
{
    bool IsMarked { get; }
    //void Mark(Transform tr);
    void UnMark();
}