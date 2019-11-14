using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatChanger : MonoBehaviour
{
    public GameObject currentHatGO;
    public List<GameObject> hatSprites = new List<GameObject>();
    int currentHatIndex = 0; //0 = default
    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.ChangeHat(), OnChangeHat);
    }
    void OnDestroy(){
        EventCoordinator.StopListening(EventName.Input.ChangeHat(), OnChangeHat);
    }
    void OnChangeHat(GameMessage msg)
    {
        if(GetComponentInParent<Owner>().EqualsByValue(msg.owner)){
            if(msg.intMessage > 0){
                currentHatIndex ++;
                if(currentHatIndex >= hatSprites.Count)
                    currentHatIndex = 0;
            } else {
                currentHatIndex --;
                if(currentHatIndex < 0)
                    currentHatIndex = hatSprites.Count-1;
            }
            ChangeHatTo(currentHatIndex);
        }
    }
    void ChangeHatTo(int index){
        currentHatIndex = index;
        GameObject newHat = Instantiate(hatSprites[currentHatIndex]);
        newHat.transform.parent = transform;
        newHat.transform.localPosition = currentHatGO.transform.localPosition;
        newHat.transform.localScale = currentHatGO.transform.localScale;
        Destroy(currentHatGO);
        currentHatGO = newHat;
    }
}
