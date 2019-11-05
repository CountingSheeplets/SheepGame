using System.Collections.Generic;
using System.Linq;

public class EventName
{
    public class UI
    {
        public static string None() { return null; }
        public static string ShowCooldownNotReady() { return "ShowCooldownNotReady"; }
        public static List<string> Get() { return new List<string> { None(), ShowCooldownNotReady()}; }
    }
    public class Editor
    {
        public static string None() { return null; }
        public static List<string> Get() { return new List<string> { None() }; }
    }
    public class Input
    {
        public class Button{
            public static string None() { return null; }
            public static List<string> Get() { return new List<string> { None() }; }          
        }
        public class Menus{
            public static string None() { return null; }
            public static List<string> Get() { return new List<string> { None() }; }
        }
        public class Network{
            public static string PlayerJoined() { return "PlayerJoined"; }
            public static string PlayerLeft() { return "PlayerLeft"; }
            public static List<string> Get() { return new List<string> { PlayerJoined(),PlayerLeft() }; }
        }
        public static string Swipe() { return "Input_Swipe"; }
        public static string StartGame() { return "StartGame"; }
        public static List<string> Get() { return new List<string> {
            StartGame(),
            Swipe()
            }.Concat(Button.Get())
                .Concat(Menus.Get())
                .ToList(); }
    }
    public class System
    {
        public class Economy{
            public static string IncomeTick() { return "Economy_IncomeTick"; }
            public static string CoinChange() { return "Economy_CoinChange"; }
            public static List<string> Get() { return new List<string> { IncomeTick(), CoinChange()}; }          
        }
        public class Cooldown{
            public static string Tick() { return "Cooldown_Tick"; }
            public static string Ended() { return "Cooldown_Ended"; }
            public static string PostEnd() { return "Cooldown_PostEnd"; }
            public static List<string> Get() { return new List<string> { Tick(), Ended(), PostEnd()}; }          
        }
        public class Sheep{
            public static string Built() { return "System_Sheep_Built"; }
            public static string Destroyed() { return "System_Building_Destroyed"; }
            public static List<string> Get() { return new List<string> { Destroyed(), Built()}; }          
        }
        public class Environment{
            public static string SetField() { return "System_SetField"; }
            public static string AdjustField() { return "System_AdjustField"; }
            public static List<string> Get() { return new List<string> { SetField(), AdjustField()}; }          
        //public static string MapLayoutChanged() { return "MapLayoutChanged"; }
        }

        //public static string NextScene() { return "NextScene"; }
        //public static string LoadScene() { return "LoadScene"; }
        public static string SceneLoaded() { return "SceneLoaded"; }
        public static List<string> Get() { return new List<string> {
            //MapLayoutChanged(),
            //NextScene(), LoadScene(),s
             SceneLoaded()
            }.Concat(Economy.Get()).Concat(Sheep.Get()).Concat(Cooldown.Get()).Concat(Environment.Get()).ToList(); }
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
    public List<string> Get()
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