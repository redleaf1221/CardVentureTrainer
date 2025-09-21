namespace CardVentureTrainer.Features.ParryDebug;

public static class ParryDebugFeature {

    public static void Init() {
        Plugin.HarmonyInstance.PatchAll(typeof(ParryDebugPatch));
        Plugin.Logger.LogInfo("ParryDebugFeature loaded.");
    }
}
