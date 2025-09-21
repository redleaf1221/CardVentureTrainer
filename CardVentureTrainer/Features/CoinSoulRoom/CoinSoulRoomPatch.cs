using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace CardVentureTrainer.Features.CoinSoulRoom;

[HarmonyPatch]
public static class CoinSoulRoomPatch {

    [HarmonyTranspiler]
    [HarmonyPatch(typeof(BattleObject), nameof(BattleObject.GenerateChapterRoomSequence))]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        if (!CoinSoulRoomFeature.Enabled) {
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
}
