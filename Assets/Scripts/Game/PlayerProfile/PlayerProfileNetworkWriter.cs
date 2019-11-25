using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;

public class PlayerProfileNetworkWriter : MonoBehaviour
{
    public float sendInterval = 1f;
    float counter;

    // Update is called once per frame
    void Update()
    {
        counter+=Time.deltaTime;
        if(counter > sendInterval){
            counter = 0;
            foreach(Owner owner in OwnersCoordinator.GetOwners())
                if(SendProfile(owner.GetPlayerProfile())){
                    Debug.Log("Cant Send. profile not found for owner:"+owner);
                };
        }
    }

    bool SendProfile(PlayerProfile profile){
        if(profile == null){
            return false;
        }
        var data = new Dictionary<string, float> { { "health", profile.GetHealth() },
                                                    { "money", profile.GetMoney() },
                                                    { "grass", Mathf.FloorToInt(profile.GetGrass())} };
        AirConsole.instance.Message(profile.owner.deviceId, data);
        return true;
    }
}
