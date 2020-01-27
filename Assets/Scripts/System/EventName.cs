using System.Collections.Generic;
using System.Linq;

public class EventName
{
    public class UI
    {
        public static string ShowScoreScreen() { return "UI_ShowScoreScreen"; }
        public static string ShowCooldownNotReady() { return "ShowCooldownNotReady"; }
        public static List<string> Get() { return new List<string> { ShowScoreScreen(), ShowCooldownNotReady()}; }
    }
    public class Editor
    {
        public static string None() { return null; }
        public static List<string> Get() { return new List<string> { None() }; }
    }
    public class Input
    {
        public class KingAbilities{
            public static string SpawnSheep() { return "KingAbilities_SpawnSheep"; }
            public static string BuyGrass() { return "KingAbilities_BuyLawn"; }
            public static string Smash() { return "KingAbilities_Smash"; }
            public static List<string> Get() { return new List<string> { SpawnSheep(),BuyGrass(),Smash() }; }          
        }
/*         public class SheepUpgrade{
            public static string A() { return "SheepUpgrade_A"; }
            public static string B() { return "SheepUpgrade_B"; }
            public static List<string> Get() { return new List<string> { A(), B() }; }          
        } */
        public class Menus{
            public static string None() { return null; }
            public static List<string> Get() { return new List<string> { None() }; }
        }
        public class Network{
            public static string View() { return "View"; }
            public static string PlayerJoined() { return "PlayerJoined"; }
            public static string PlayerLeft() { return "PlayerLeft"; }
            public static List<string> Get() { return new List<string> {View(), PlayerJoined(),PlayerLeft() }; }
        }
        public static string SheepUpgrade() { return "Input_SheepUpgrade"; }
        public static string Swipe() { return "Input_Swipe"; }
        public static string ChangeHat() { return "King_ChangeHat"; }
        public static string ChangeScepter() { return "King_ChangeScepter"; }
        public static string StartGame() { return "StartGame"; }
        public static List<string> Get() { return new List<string> {
            StartGame(), SheepUpgrade(),
            Swipe(), ChangeHat(), ChangeScepter()
            }.Concat(KingAbilities.Get())
                //.Concat(SheepUpgrade.Get())
                .Concat(Menus.Get())
                .Concat(Network.Get())
                .ToList(); }
    }
    public class System
    {
        public class Economy{
            public static string EatGrass() { return "Economy_EatGrass"; }
            public static string IncomeTick() { return "Economy_IncomeTick"; }
            public static string CoinChange() { return "Economy_CoinChange"; }
            public static List<string> Get() { return new List<string> { IncomeTick(), CoinChange(), EatGrass()}; }          
        }
        public class Cooldown{
            public static string Tick() { return "Cooldown_Tick"; }
            public static string Ended() { return "Cooldown_Ended"; }
            public static string PostEnd() { return "Cooldown_PostEnd"; }
            public static List<string> Get() { return new List<string> { Tick(), Ended(), PostEnd()}; }          
        }
        public class King{
            public static string Spawned() { return "System_King_Spawned"; }
            public static string Smashed() { return "System_King_Smashed"; }
            public static string SmashReset() { return "System_King_SmashReset"; }
            public static string Killed() { return "System_King_Killed"; }
            public static string Hit() { return "System_King_Hit"; }
            public static string ReceivedDamage() { return "System_King_ReceivedDamage"; }
            public static List<string> Get() { return new List<string> { ReceivedDamage(), Hit(), Spawned(), Smashed(), SmashReset(), Killed()}; }          
        }
        public class Sheep{
            public static string Spawned() { return "System_Sheep_Spawned"; }
            public static string Launch() { return "System_Sheep_Launch"; }
            public static string Land() { return "System_Sheep_Land"; }
            public static string ReadyToLaunch() { return "System_Sheep_ReadyToLaunch"; }
            public static string Kill() { return "System_Sheep_Kill"; }
            public static string Roam() { return "System_Sheep_Roam"; }
            public static string Upgraded() { return "System_Sheep_Upgraded"; }
            public static List<string> Get() { return new List<string> { Spawned(), Launch(), Land(), ReadyToLaunch(), Kill(), Roam(), Upgraded()}; }          
        }
        public class Environment{
            public static string Initialized() {return "System_Environment_Initialized";}
            public static string SetField() { return "System_Environment_SetField"; }
            public static string AdjustField() { return "System_Environment_AdjustField"; }
            public static string EndMatch() { return "System_Environment_EndMatch"; }
            public static string ArenaAnimating() {return "System_Environment_ArenaAnimating";}
            public static string PlayfieldAnimated() { return "System_Environment_PlayfieldAnimated"; }
            public static string ArenaAnimated() { return "System_Environment_ArenaAnimated"; }
            public static List<string> Get() { return new List<string> {Initialized(), SetField(), AdjustField(), EndMatch(), ArenaAnimating(), PlayfieldAnimated(), ArenaAnimated() }; }          
        }
        public class Player{
            public static string ProfileCreated() { return "System_ProfileCreated"; }
            public static string ProfileUpdate() { return "System_ProfileUpdate"; }
            public static string PlayerCardsSorted() { return "System_PlayerCardsSorted"; }
            public static string Eliminated() { return "System_Eliminated"; }
            public static string PostElimination() { return "System_PostElimination"; }
            public static List<string> Get() { return new List<string> {ProfileCreated(), ProfileUpdate(),PlayerCardsSorted(),Eliminated()}; }          
        }
        //public static string NextScene() { return "NextScene"; }
        //public static string LoadScene() { return "LoadScene"; }
        //public static string SceneLoaded() { return "SceneLoaded"; }
        public static List<string> Get() { return new List<string> {
            //MapLayoutChanged(),
            //NextScene(), LoadScene(),s
             //SceneLoaded()
            }.Concat(Economy.Get()).Concat(King.Get()).Concat(Sheep.Get()).Concat(Cooldown.Get()).Concat(Environment.Get()).ToList(); }
    }
    public class AI
    {
        public static string None() { return null; }
        public static List<string> Get() { return new List<string> { None() }; }
    }
/*     public class PCTest{
        public static string SwitchFakeController() {return "SwitchFakeController";}
        public static List<string> Get() { return new List<string> {
        SwitchFakeController()}.ToList();    }
    } */
    public static List<string> Get()
    {
        return new List<string> { }.Concat(UI.Get())
                                    .Concat(Editor.Get())
                                    .Concat(Input.Get())
                                    .Concat(System.Get())
                                    .Concat(AI.Get())
                                    //.Concat(PCTest.Get())
                                    .Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
    }
}