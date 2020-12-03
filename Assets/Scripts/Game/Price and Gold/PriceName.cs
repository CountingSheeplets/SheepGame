using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PriceName {
    public class King {
        public static string BuySheep() { return "King_BuySheep"; }
        public static string Upgrade() { return "King_Upgrade"; }
        public static string BuyGrass() { return "King_BuyGrass"; }
        public static string Smash() { return "King_Smash"; }
        public static List<string> Get() { return new List<string> { BuySheep(), Upgrade(), BuyGrass(), Smash() }; }
    }
    public class SheepUpgrade {
        public static string Small() { return "SheepUpgrade_Small"; }
        public static string Bouncy() { return "SheepUpgrade_Bouncy"; }
        public static string Greedy() { return "SheepUpgrade_Greedy"; }
        public static string Armored() { return "SheepUpgrade_Armored"; }
        public static string Trench() { return "SheepUpgrade_Trench"; }
        public static string Tank() { return "SheepUpgrade_Tank"; }
        public static List<string> Get() {
            return new List<string> {
                    Small(),
                    Bouncy(),
                    Greedy(),
                    Armored(),
                    Trench(),
                    Tank()
                }
                .ToList();
        }
    }
    public class Player {
        public static string KingElimStar() { return "Player_KingElimStar"; }
        public static string KingElimGold() { return "Player_KingElimGold"; }
        public static List<string> Get() { return new List<string> { KingElimStar(), KingElimGold() }; }
    }
    public static List<string> Get() {
        return new List<string> {}.Concat(SheepUpgrade.Get())
            .Concat(King.Get())
            .Concat(Player.Get())
            .Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
    }
}