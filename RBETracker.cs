using System.Collections.Generic;
using MelonLoader;
using BTD_Mod_Helper;
using System.IO;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using RBETracker;

[assembly: MelonInfo(typeof(RBETracker.RBETracker), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace RBETracker;

public class RBETracker : BloonsTD6Mod
{
    private static int maxRound;
    private static int startRound;
    private static int[] rounds;
    private static Dictionary<string, int> endRounds = new Dictionary<string, int>
    {
        {"easy", 40},
        {"medium", 60},
        {"hard", 80},
        {"impoppable", 100},
        {"chimps", 100}
    };
    public override void OnApplicationStart()
    {
        ModHelper.Msg<RBETracker>("RBETracker loaded!");
        Assembly assembly = Assembly.GetExecutingAssembly();
        using (Stream stream = assembly.GetManifestResourceStream("RBE.csv"))
        using (StreamReader reader = new StreamReader(stream))
        {
            rounds = reader.ReadToEnd().Split("\n").Select(int.Parse).ToArray();
        }
    }

    [HarmonyPatch(typeof(InGame), nameof(InGame.RoundStart))]
    class Patch_RoundStart
    {
        [HarmonyPostfix]
        public static void Ingame_RoundStart(InGame __instance)
        {
            if (startRound == 0)
            {
                startRound = __instance.GetStartRound();
            }

            if (maxRound == 0)
            {
                maxRound = endRounds[__instance.SelectedDifficulty];
            }
        }
    }
    
    [HarmonyPatch(typeof(Bloon), nameof(Bloon.RecieveDamage))]
    class Patch_RecieveDamage
    {
        [HarmonyPrefix]
        public static void Bloon_RecieveDamage()
        {
            
        }
    }
}