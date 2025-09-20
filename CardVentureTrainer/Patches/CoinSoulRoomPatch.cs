using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using BepInEx.Configuration;
using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch]
public static class CoinSoulRoomPatch {
    private static ConfigEntry<bool> _configEnabled;

    public static bool Enabled {
        get => _configEnabled.Value;
        set => _configEnabled.Value = value;
    }

    [HarmonyTranspiler]
    [HarmonyPatch(typeof(BattleObject), nameof(BattleObject.GenerateChapterRoomSequence))]
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        if (!Enabled) {
            return instructions;
        }
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldloc_0),
                new CodeMatch(OpCodes.Newobj),
                new CodeMatch(OpCodes.Dup),
                new CodeMatch(OpCodes.Ldc_I4_S, (sbyte)RoomType.Coin),
                new CodeMatch(OpCodes.Callvirt),
                new CodeMatch(OpCodes.Dup),
                new CodeMatch(OpCodes.Ldc_I4_S, (sbyte)RoomType.Soul),
                new CodeMatch(OpCodes.Callvirt),
                new CodeMatch(OpCodes.Callvirt),
                new CodeMatch(OpCodes.Br)
            )
            .ThrowIfInvalid("Failed to patch BattleObject.GenerateChapterRoomSequence!!")
            .Advance(3)
            .SetOperandAndAdvance((sbyte)RoomType.Weapon)
            .Advance(2)
            .SetOperandAndAdvance((sbyte)RoomType.Ability)
            .InstructionEnumeration();
    }

    public static void InitPatch() {
        _configEnabled = Config.Bind("Trainer", "DisableCoinSoulRoom",
            false, "Replace generated coin and soul room.");
        HarmonyInstance.PatchAll(typeof(CoinSoulRoomPatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Logger.LogInfo($"DisableCoinSoulRoom changed to {Enabled}.");
            HarmonyInstance.Unpatch(typeof(BattleObject).GetMethod(nameof(BattleObject.GenerateChapterRoomSequence),
                    BindingFlags.Public | BindingFlags.Instance),
                typeof(CoinSoulRoomPatch).GetMethod(nameof(Transpiler),
                    BindingFlags.NonPublic | BindingFlags.Static));
            HarmonyInstance.PatchAll(typeof(CoinSoulRoomPatch));
        };
        Logger.LogInfo("CoinSoulRoomPatch done.");
    }
}
