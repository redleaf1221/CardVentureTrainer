using HarmonyLib;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(SafeInt), nameof(SafeInt.GenerateKey))]
public static class SafeIntGenerateKeyPatch {
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once RedundantAssignment
    private static bool Prefix(ref int __result) {
        __result = 0;
        return false;
    }
}
