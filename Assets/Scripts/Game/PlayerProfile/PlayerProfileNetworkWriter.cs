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
                SendProfile(owner.GetPlayerProfile());
        }
    }

    void SendProfile(PlayerProfile profile){
        var data = new Dictionary<string, float> { { "health", profile.GetHealth() },
                                                    { "money", profile.GetMoney() },
                                                    { "grass", Mathf.FloorToInt(profile.GetGrass())} };
        AirConsole.instance.Message(profile.owner.deviceId, data);
    }
}
