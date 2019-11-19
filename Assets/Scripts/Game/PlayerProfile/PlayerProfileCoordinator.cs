using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerProfileCoordinator : Singleton<PlayerProfileCoordinator>
{
    public List<PlayerProfile> profiles = new List<PlayerProfile>();

    public static float ModifyPlayerGrass(Owner owner, float amount){
        return Instance.GetProfile(owner).ModifyGrass(amount);
    }

    public PlayerProfile GetProfile(Owner owner){
        return profiles.Where(x => x.owner = owner).FirstOrDefault();
    }
}
