using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NDream.AirConsole;

public class PlayerProfileCoordinator : Singleton<PlayerProfileCoordinator>
{
    public List<PlayerProfile> profiles = new List<PlayerProfile>();
    public List<Color> colors = new List<Color>();

    public static float ModifyPlayerGrass(Owner owner, float amount){
        return GetProfile(owner).ModifyGrass(amount);
    }

    public static PlayerProfile GetProfile(Owner owner){
        return Instance.profiles.Where(x => x.owner.EqualsByValue(owner)).FirstOrDefault();
    }

    public static PlayerProfile AddProfile(Owner owner){
        //Debug.Log("adding a profile");
        PlayerProfile profile = new PlayerProfile().Create(owner);
        //assign data
        if(Instance.colors.Count > Instance.profiles.Count)
            profile.playerColor = Instance.colors[Instance.profiles.Count];
        else
            profile.playerColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        //send assigned data
        var data = new Dictionary<string, string> { { "color", ColorUtility.ToHtmlStringRGBA(profile.playerColor)}};
        AirConsole.instance.Message(owner.deviceId, data);

        Instance.profiles.Add(profile);
        GetPlayerAvatarIcon(owner);
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
    public static void GetPlayerAvatarIcon(Owner owner){
		string urlOfProfilePic = AirConsole.instance.GetProfilePicture (owner.deviceId, 512);
        Instance.StartCoroutine(GetProfile(owner).DisplayUrlPicture(urlOfProfilePic));
    }
}
