using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch]
public class FriendUnitLimitPatch {
    [HarmonyTranspiler]
    [HarmonyPatch(typeof(BattleObject), nameof(BattleObject.SpawnUnit))]
    private static IEnumerable<CodeInstruction> SpawnUnitTranspiler(IEnumerable<CodeInstruction> instructions) {
        if (!Conf.ConfigDisableFriendUnitLimit.Value) {
            return instructions;
        }
        return new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(CodeInstruction.LoadField(typeof(BattleObject), "friendObjects")),
                new CodeMatch(OpCodes.Callvirt),
                new CodeMatch(OpCodes.Ldc_I4_4),
                new CodeMatch(OpCodes.Ble)
            )
            .ThrowIfInvalid("Failed to patch BattleObject.SpawnUnit!!")
            .SetAndAdvance(OpCodes.Nop, null)
            .SetAndAdvance(OpCodes.Nop, null)
            .SetAndAdvance(OpCodes.Nop, null)
            .SetAndAdvance(OpCodes.Nop, null)
            .SetOpcodeAndAdvance(OpCodes.Br)
            .InstructionEnumeration();
    }

    public static void RegisterThis(Harmony harmony) {
        harmony.PatchAll(typeof(FriendUnitLimitPatch));
        Conf.ConfigDisableFriendUnitLimit.SettingChanged += (sender, args) => {
            Logger.LogInfo($"DisableFriendUnitLimit changed to {Conf.ConfigDisableFriendUnitLimit.Value}.");
            harmony.Unpatch(typeof(BattleObject).GetMethod(nameof(BattleObject.SpawnUnit)),
                typeof(FriendUnitLimitPatch).GetMethod(nameof(SpawnUnitTranspiler)));
            harmony.PatchAll(typeof(FriendUnitLimitPatch));
        };
        Logger.LogInfo("FriendUnitLimitPatch done.");
    }
}
