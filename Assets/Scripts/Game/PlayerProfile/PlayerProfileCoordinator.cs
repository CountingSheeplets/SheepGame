using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerProfileCoordinator : Singleton<PlayerProfileCoordinator>
{
    public List<PlayerProfile> profiles = new List<PlayerProfile>();

    public static float ModifyPlayerGrass(Owner owner, float amount){
        return GetProfile(owner).ModifyGrass(amount);
    }

    public static PlayerProfile GetProfile(Owner owner){
        return Instance.profiles.Where(x => x.owner = owner).FirstOrDefault();
    }

    public static PlayerProfile AddProfile(Owner owner){
        Debug.Log("adding a profile");
        PlayerProfile profile = new PlayerProfile().Create(owner);
        Instance.profiles.Add(profile);
        return profile;
    }

    public static PlayerProfile RemoveProfile(Owner owner){
        PlayerProfile profile = GetProfile(owner);
        Instance.profiles.Remove(profile);
        return profile;
    }
}
