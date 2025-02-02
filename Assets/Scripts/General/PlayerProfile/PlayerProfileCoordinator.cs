using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerProfileCoordinator : Singleton<PlayerProfileCoordinator> {
    public List<PlayerProfile> profiles = new List<PlayerProfile>();

    public static float ModifyPlayerGrass(Owner owner, float amount) {
        float targetAmount = GetProfile(owner).ModifyGrass(amount);
        return targetAmount;
    }

    public static PlayerProfile GetProfile(Owner owner) {
        return Instance.profiles.Where(x => x.owner.EqualsByValue(owner)).FirstOrDefault();
    }

    public static PlayerProfile AddProfile(Owner owner) {
        PlayerProfile profile = new PlayerProfile().Create(owner);
        Instance.profiles.Add(profile);
        GetPlayerAvatarIcon(owner);
        return profile;
    }

    public static PlayerProfile RemoveProfile(Owner owner) {
        PlayerProfile profile = GetProfile(owner);
        PlayerColorCoordinator.UnUse(profile.playerColor);
        Instance.profiles.Remove(profile);
        return profile;
    }

    public static List<float> GetAliveTeams() {
        List<float> teams = new List<float>();
        foreach (PlayerProfile profile in Instance.profiles) {
            float id = profile.owner.teamId;
            if (!teams.Contains(id))
                teams.Add(id);
        }
        return teams;
    }
    public static List<Owner> GetAliveOwners() {
        List<Owner> owners = new List<Owner>();
        foreach (PlayerProfile profile in Instance.profiles) {
            Owner owner = profile.owner;
            if (!owners.Contains(owner) && profile.isAlive)
                owners.Add(owner);
        }
        return owners;
    }

    public static void GetPlayerAvatarIcon(Owner owner) {
        //if (AirConsole.instance == null)
        //    return;
        //string urlOfProfilePic = AirConsole.instance.GetProfilePicture(owner.deviceId, 512);
        //Instance.StartCoroutine(GetProfile(owner).DisplayUrlPicture(urlOfProfilePic));
    }
}