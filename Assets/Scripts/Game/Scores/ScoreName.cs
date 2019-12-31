using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreName : MonoBehaviour
{
    public class Achievement
    {
        public static string GetThatAction() { return "Get that action"; }
        public static string Education() { return "Higher Education"; }
        public static string Baaah() { return "Dr. Baaah"; }
        public static string Paladin() { return "Paladin"; }
        public static List<string> Get() { return new List<string> { GetThatAction(), Education(), Baaah(), Paladin()}; }
    }
    public class Counter
    {
        public static string Angry() { return "Angry sheep"; }
        public static string Shepherd() { return "Grand Shepherd"; }
        public static string Culling() { return "The Culling"; }
        public static string Elvish() { return "Elvish-sheep"; }
        public static string Merchant() { return "Merchant"; }
        public static List<string> Get() { return new List<string> {
            Angry(),
            Shepherd(), Culling(),
            Elvish(), Merchant()
            }//.Concat(KingAbilities.Get())
                .ToList(); }
    }
    public static List<string> Get()
    {
        return new List<string> { }.Concat(Achievement.Get())
                                    .Concat(Counter.Get())
                                    .Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
    }
}
