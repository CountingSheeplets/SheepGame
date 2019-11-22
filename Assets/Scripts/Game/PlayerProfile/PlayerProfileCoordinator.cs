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
        return Instance.profiles.Where(x => x.owner.EqualsByValue(owner)).FirstOrDefault();
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

    public static int GetAliveTeamCount(){
        List<float> teams = new List<float>();
        foreach(PlayerProfile profile in Instance.profiles){
            float id = profile.owner.teamId;
            if(!teams.Contains(id))
                teams.Add(id);
        }
        return teams.Count;
    }
}
