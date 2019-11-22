using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PriceName
{
    public class King
    {
        public static string BuySheep() { return "King_BuySheep"; }
        public static string BuyGrass() { return "King_BuyGrass"; }
        public static List<string> Get() { return new List<string> { BuySheep(), BuyGrass()}; }
    }
    public class Sheep
    {
/*         public class KingAbilities{
            public static string SpawnSheep() { return "KingAbilities_SpawnSheep"; }
            public static string BuyGrass() { return "KingAbilities_BuyLawn"; }
            public static List<string> Get() { return new List<string> { SpawnSheep(),BuyGrass() }; }          
        } */
        public static string ToKnight() { return "Sheep_ToKnight"; }
        public static string ToMini() { return "Sheep_ToMini"; }
        public static string ToSpikey() { return "Sheep_ToSpikey"; }
        public static List<string> Get() { return new List<string> {
            ToKnight(),
            ToMini(), ToSpikey()
            }//.Concat(KingAbilities.Get())
                .ToList(); }
    }
    public static List<string> Get()
    {
        return new List<string> { }.Concat(Sheep.Get())
                                    .Concat(King.Get())
                                    .Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
    }
}
