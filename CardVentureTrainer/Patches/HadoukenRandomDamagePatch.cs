using System.Collections.Generic;
using System.Reflection.Emit;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(UnitObjectPlayer), nameof(UnitObjectPlayer.PlayerInputCheck))]
public static class HadoukenRandomDamagePatch {
    private static ConfigEntry<bool> _configEnabled;

    public static bool Enabled {
        get => _configEnabled.Value;
        set => _configEnabled.Value = value;
    }

    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        if (!Enabled) {
            return instructions;
        }
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(CodeInstruction.Call(typeof(Random), "get_value")),
                new CodeMatch(OpCodes.Ldc_R4, 0.2f),
                new CodeMatch(OpCodes.Bge_Un)
            )
            .ThrowIfInvalid("Failed to patch UnitObjectPlayer.PlayerInputCheck!!")
            .SetAndAdvance(OpCodes.Nop, null)
            .SetAndAdvance(OpCodes.Nop, null)
            .SetOpcodeAndAdvance(OpCodes.Br)
            .InstructionEnumeration();
    }
    public static void InitConfig(Plugin plugin) {
        _configEnabled = plugin.Config.Bind("Trainer", "DisableHadoukenNegativeDamage",
            false, "Disable negative damage of ability Hadouken.");
    }
    public static void RegisterThis(Harmony harmony) {
        harmony.PatchAll(typeof(HadoukenRandomDamagePatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Plugin.Logger.LogInfo($"DisableHadoukenNegativeDamage changed to {Enabled}.");
            harmony.Unpatch(typeof(UnitObjectPlayer).GetMethod(nameof(UnitObjectPlayer.PlayerInputCheck)),
                typeof(HadoukenRandomDamagePatch).GetMethod(nameof(Transpiler)));
            harmony.PatchAll(typeof(HadoukenRandomDamagePatch));
        };
        Plugin.Logger.LogInfo("HadoukenRandomDamagePatch done.");
    }
}
