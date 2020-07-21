using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NDream.AirConsole;
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
        //Debug.Log("adding a profile");
        PlayerProfile profile = new PlayerProfile().Create(owner);
        //assign data
        if (ConstantsBucket.PlayerColors.Count > Instance.profiles.Count)
            profile.playerColor = ConstantsBucket.PlayerColors[Instance.profiles.Count];
        else
            profile.playerColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        Instance.profiles.Add(profile);
        GetPlayerAvatarIcon(owner);
        return profile;
    }

    public static PlayerProfile RemoveProfile(Owner owner) {
        PlayerProfile profile = GetProfile(owner);
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
        if (AirConsole.instance == null)
            return;
        string urlOfProfilePic = AirConsole.instance.GetProfilePicture(owner.deviceId, 512);
        Instance.StartCoroutine(GetProfile(owner).DisplayUrlPicture(urlOfProfilePic));
    }
}