using HarmonyLib;
using UnityEngine;

namespace CardVentureTrainer.Features.ErrorReport;

[HarmonyPatch]
public class ErrorReportPatch {
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GameController), nameof(GameController.HandleException))]
    public static bool Prefix(string condition, string stackTrace, LogType type) {
        if (type is LogType.Exception or LogType.Error) {
            Plugin.Logger.LogInfo("CardVentureTrainer installed, error report disabled.");
        }
        return false;
    }
}
