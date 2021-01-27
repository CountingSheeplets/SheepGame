using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerProfileFactory {
    public static PlayerProfile Create(this PlayerProfile profile, Owner owner) {
        PlayerProfile newProfile = new PlayerProfile();
        newProfile.owner = owner;
        newProfile.playerColor = PlayerColorCoordinator.UseFirstUnused();
        return newProfile;
    }
    public static PlayerProfile Modify(this PlayerProfile profile, KingUnit kingUnit, Playfield playfield) {
        profile.kingUnit = kingUnit;
        profile.playfield = playfield;
        return profile;
    }
}