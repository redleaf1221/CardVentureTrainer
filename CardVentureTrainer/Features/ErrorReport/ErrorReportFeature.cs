using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Features.ErrorReport;

public static class ErrorReportFeature {

    public static void Init() {
        HarmonyInstance.PatchAll(typeof(ErrorReportPatch));
        Logger.LogInfo("ErrorReportFeature loaded.");
    }
}
