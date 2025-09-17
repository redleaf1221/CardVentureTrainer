using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch]
public static class SafeIntPatch {
    // ReSharper disable once InconsistentNaming
    [HarmonyPrefix]
    [HarmonyPatch(typeof(SafeInt), nameof(SafeInt.GenerateKey))]
    private static bool GenerateKeyPatch(out int __result) {
        __result = 0;
        return false;
    }

    // ReSharper disable once InconsistentNaming
    [HarmonyPrefix]
    [HarmonyPatch(typeof(SafeInt))]
    [HarmonyPatch("Value", MethodType.Setter)]
    private static bool ValueSetterPatch(SafeInt __instance, int value) {
        __instance.key = 0;
        __instance.encryptedValue = value;
        return false;
    }

    public static void InitPatch() {
        HarmonyInstance.PatchAll(typeof(SafeIntPatch));
        Logger.LogInfo("SafeIntPatch done.");
    }
}
