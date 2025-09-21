using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace CardVentureTrainer.Features.FriendUnitLimit;

[HarmonyPatch]
public class FriendUnitLimitPatch {


    [HarmonyTranspiler]
    [HarmonyPatch(typeof(BattleObject), nameof(BattleObject.SpawnUnit))]
    public static IEnumerable<CodeInstruction> SpawnUnitTranspiler(IEnumerable<CodeInstruction> instructions) {
        if (!FriendUnitLimitFeature.Enabled) {
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
}
