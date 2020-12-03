using System.Collections.Generic;
using System.Linq;

public class EventName {
    public class UI {
        public static string ShowScoreScreen() { return "UI_ShowScoreScreen"; }
        public static string ShowCooldownNotReady() { return "ShowCooldownNotReady"; }
        public static List<string> Get() { return new List<string> { ShowScoreScreen(), ShowCooldownNotReady() }; }
    }
    public class Editor {
        public static string None() { return null; }
        public static List<string> Get() { return new List<string> { None() }; }
    }
    public class Input {
        public class KingAbilities {
            public static string SpawnSheep() { return "KingAbilities_SpawnSheep"; }
            public static string KingUpgrade() { return "KingAbilities_KingUpgrade"; }
            public static string BuyGrass() { return "KingAbilities_BuyLawn"; }
            public static string Smash() { return "KingAbilities_Smash"; }
            public static List<string> Get() { return new List<string> { SpawnSheep(), KingUpgrade(), BuyGrass(), Smash() }; }
        }
        /*         public class SheepUpgrade{
                    public static string A() { return "SheepUpgrade_A"; }
                    public static string B() { return "SheepUpgrade_B"; }
                    public static List<string> Get() { return new List<string> { A(), B() }; }          
                } */
        public class Menus {
            public static string None() { return null; }
            public static List<string> Get() { return new List<string> { None() }; }
        }
        public class Network {
            public static string View() { return "View"; }
            public static string PlayerJoined() { return "PlayerJoined"; }
            public static string PlayerRecalculate() { return "PlayerRecalculate"; }
            public static string PlayerLeft() { return "PlayerLeft"; }
            public static List<string> Get() { return new List<string> { View(), PlayerJoined(), PlayerLeft(), PlayerRecalculate() }; }
        }
        public static string SheepUpgrade() { return "Input_SheepUpgrade"; }
        public static string Swipe() { return "Input_Swipe"; }
        public static string Tap() { return "Input_Tap"; }
        public static string ChangeKingItem() { return "ChangeKingItem"; }
        public static string SetKingItem() { return "SetKingItem"; }
        public static string StartGame() { return "StartGame"; }
        public static string PlayersReady() { return "PlayersReady"; }
        public static List<string> Get() {
            return new List<string> {
                    PlayersReady(),
                    StartGame(),
                    SheepUpgrade(),
                    Swipe(),
                    Tap(),
                    ChangeKingItem(),
                    SetKingItem()
                }.Concat(KingAbilities.Get())
                //.Concat(SheepUpgrade.Get())
                .Concat(Menus.Get())
                .Concat(Network.Get())
                .ToList();
        }
    }
    public class System {
        public class Economy {
            public static string EatGrass() { return "Economy_EatGrass"; }
            public static string GrassChanged() { return "Economy_GrassChanged"; } // should be under Playfield
            public static string GoldChanged() { return "Economy_GoldChanged"; }
            public static string ComboChanged() { return "Economy_ComboChanged"; }
            public static List<string> Get() { return new List<string> { GrassChanged(), GoldChanged(), EatGrass(), ComboChanged() }; }
        }
        public class Cooldown {
            public static string Tick() { return "Cooldown_Tick"; }
            public static string Ended() { return "Cooldown_Ended"; }
            public static string PostEnd() { return "Cooldown_PostEnd"; }
            public static List<string> Get() { return new List<string> { Tick(), Ended(), PostEnd() }; }
        }
        public class King {
            public static string Spawned() { return "System_King_Spawned"; }
            public static string StartSmash() { return "System_King_StartSmash"; }
            public static string Smashed() { return "System_King_Smashed"; }
            public static string SmashReset() { return "System_King_SmashReset"; }
            public static string Killed() { return "System_King_Killed"; }
            public static string Hit() { return "System_King_Hit"; }
            public static string ReceivedDamage() { return "System_King_ReceivedDamage"; }
            public static List<string> Get() { return new List<string> { ReceivedDamage(), StartSmash(), Hit(), Spawned(), Smashed(), SmashReset(), Killed() }; }
        }
        public class Sheep {
            public static string Spawned() { return "System_Sheep_Spawned"; }
            public static string Launch() { return "System_Sheep_Launch"; }
            public static string Land() { return "System_Sheep_Land"; }
            public static string KingMissed() { return "System_Sheep_KingMissed"; }
            public static string ReadyToLaunch() { return "System_Sheep_ReadyToLaunch"; }
            public static string Kill() { return "System_Sheep_Kill"; }
            public static string Roam() { return "System_Sheep_Roam"; }
            public static string Upgraded() { return "System_Sheep_Upgraded"; }
            public static List<string> Get() { return new List<string> { Spawned(), Launch(), Land(), KingMissed(), ReadyToLaunch(), Kill(), Roam(), Upgraded() }; }
        }
        public class Environment {
            public static string Initialized() { return "System_Environment_Initialized"; }
            public static string EndMatch() { return "System_Environment_EndMatch"; }
            public static string DestroyArena() { return "System_Environment_DestroyArena"; }
            public static string ArenaDestroyed() { return "System_Environment_ArenaDestroyed"; }
            public static string ArenaAnimating() { return "System_Environment_ArenaAnimating"; }
            public static string PlayfieldAnimated() { return "System_Environment_PlayfieldAnimated"; }
            public static string ArenaAnimated() { return "System_Environment_ArenaAnimated"; }
            public static string ScrollScoresOut() { return "System_Environment_ScrollScoresOut"; }
            public static string CleanScene() { return "System_Environment_CleanScene"; }
            public static List<string> Get() { return new List<string> { Initialized(), EndMatch(), DestroyArena(), ArenaDestroyed(), ArenaAnimating(), PlayfieldAnimated(), ArenaAnimated(), CleanScene() }; }
        }
        public class Player {
            public static string ProfileCreated() { return "System_ProfileCreated"; }
            public static string ProfileUpdate() { return "System_ProfileUpdate"; }
            public static string PlayerCardsSorted() { return "System_PlayerCardsSorted"; }
            public static string Eliminated() { return "System_Eliminated"; }
            public static string PostElimination() { return "System_PostElimination"; }
            public static List<string> Get() { return new List<string> { ProfileCreated(), ProfileUpdate(), PlayerCardsSorted(), Eliminated() }; }
        }
        public class Booster {
            public static string Created() { return "System_BoosterCreated"; }
            public static string Consumed() { return "System_BoosterConsumed"; }
            public static List<string> Get() { return new List<string> { Created(), Consumed() }; }
        }
        //public static string NextScene() { return "NextScene"; }
        //public static string LoadScene() { return "LoadScene"; }
        public static string SceneLoaded() { return "SceneLoaded"; }
        public static List<string> Get() {
            return new List<string> {
                //MapLayoutChanged(),
                //NextScene(), LoadScene(),s
                SceneLoaded()
            }.Concat(Economy.Get()).Concat(King.Get()).Concat(Sheep.Get()).Concat(Cooldown.Get()).Concat(Environment.Get()).Concat(Booster.Get()).ToList();
        }
    }
    public class AI {
        public static string None() { return null; }
        public static List<string> Get() { return new List<string> { None() }; }
    }
    /*     public class PCTest{
            public static string SwitchFakeController() {return "SwitchFakeController";}
            public static List<string> Get() { return new List<string> {
            SwitchFakeController()}.ToList();    }
        } */
    public static List<string> Get() {
        return new List<string> {}.Concat(UI.Get())
            .Concat(Editor.Get())
            .Concat(Input.Get())
            .Concat(System.Get())
            .Concat(AI.Get())
            //.Concat(PCTest.Get())
            .Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
    }
}