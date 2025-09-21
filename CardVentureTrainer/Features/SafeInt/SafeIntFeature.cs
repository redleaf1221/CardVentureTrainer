namespace CardVentureTrainer.Features.SafeInt;

public static class SafeIntFeature {
    public static void Init() {
        Plugin.HarmonyInstance.PatchAll(typeof(SafeIntPatch));
        Plugin.Logger.LogInfo("SafeIntFeature loaded.");
    }
}
