using HarmonyLib;

namespace CardVentureTrainer.Features.SafeInt;

[HarmonyPatch]
public static class SafeIntPatch {
    // ReSharper disable once InconsistentNaming
    [HarmonyPrefix]
    [HarmonyPatch(typeof(global::SafeInt), nameof(global::SafeInt.GenerateKey))]
    private static bool GenerateKeyPatch(out int __result) {
        __result = 0;
        return false;
    }

    // ReSharper disable once InconsistentNaming
    [HarmonyPrefix]
    [HarmonyPatch(typeof(global::SafeInt))]
    [HarmonyPatch("Value", MethodType.Setter)]
    private static bool ValueSetterPatch(global::SafeInt __instance, int value) {
        __instance.key = 0;
        __instance.encryptedValue = value;
        return false;
    }
}
