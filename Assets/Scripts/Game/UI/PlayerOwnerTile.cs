using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOwnerTile : MonoBehaviour
{
    public GameObject shade;
    public void Ready(){
        shade.SetActive(true);
    }
}
