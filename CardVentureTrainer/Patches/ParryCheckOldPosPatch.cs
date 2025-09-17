using System.Collections.Generic;
using System.Reflection.Emit;
using BepInEx.Configuration;
using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(UnitObjectAbility), nameof(UnitObjectAbility.AddDamageRange))]
public static class ParryCheckOldPosPatch {
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
                new CodeMatch(OpCodes.Ldc_I4_1),
                new CodeMatch(OpCodes.Stloc_3),
                new CodeMatch(OpCodes.Ldc_I4_0),
                new CodeMatch(OpCodes.Stloc_S)
            )
            .ThrowIfInvalid("Failed to patch UnitObjectAbility.AddDamageRange!!")
            .Advance(2)
            .SetAndAdvance(OpCodes.Nop, null)
            .SetAndAdvance(OpCodes.Nop, null)
            .InstructionEnumeration();
    }

    public static void InitPatch(Plugin plugin, Harmony harmony) {
        _configEnabled = plugin.Config.Bind("Trainer", "DisableParryOldPosCheck",
            false, "Allow parrying even if perviously in the attack range.");
        harmony.PatchAll(typeof(ParryCheckOldPosPatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Logger.LogInfo($"DisableParryOldPosCheck changed to {Enabled}.");
            harmony.Unpatch(typeof(UnitObjectPlayer).GetMethod(nameof(UnitObjectAbility.AddDamageRange)),
                typeof(ParryCheckOldPosPatch).GetMethod(nameof(Transpiler)));
            harmony.PatchAll(typeof(ParryCheckOldPosPatch));
        };
        Logger.LogInfo("ParryCheckOldPosPatch done.");
    }
}
