using HarmonyLib;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(SafeInt))]
[HarmonyPatch("Value", MethodType.Setter)]
public static class SafeIntValueSetterPatch {
    // ReSharper disable once InconsistentNaming
    private static bool Prefix(SafeInt __instance, int value) {
        __instance.key = 0;
        __instance.encryptedValue = value;
        return false;
    }
}
