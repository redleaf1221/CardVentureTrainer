using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(UnitObjectPlayer), nameof(UnitObjectPlayer.PlayerInputCheck))]
public static class HadoukenRandomDamagePatch {
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        if (!Conf.ConfigDisableHadoukenNegDamage.Value) {
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
    public static void RegisterThis(Harmony harmony) {
        harmony.PatchAll(typeof(HadoukenRandomDamagePatch));
        Conf.ConfigDisableHadoukenNegDamage.SettingChanged += (sender, args) => {
            Plugin.Logger.LogInfo($"DisableHadoukenNegativeDamage changed to {Conf.ConfigDisableHadoukenNegDamage.Value}.");
            harmony.Unpatch(typeof(UnitObjectPlayer).GetMethod(nameof(UnitObjectPlayer.PlayerInputCheck)),
                typeof(HadoukenRandomDamagePatch).GetMethod(nameof(Transpiler)));
            harmony.PatchAll(typeof(HadoukenRandomDamagePatch));
        };
        Plugin.Logger.LogInfo("HadoukenRandomDamagePatch done.");
    }
}
