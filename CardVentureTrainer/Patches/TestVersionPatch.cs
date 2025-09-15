using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(GameController), nameof(GameController.Awake))]
public static class TestVersionPatch {
    private static void Postfix() {
        GameSettings.testVersion = Conf.ConfigEnableTestVersion.Value;
    }

    public static void RegisterThis(Harmony harmony) {
        harmony.PatchAll(typeof(TestVersionPatch));
        Conf.ConfigEnableTestVersion.SettingChanged += (sender, args) => {
            Logger.LogInfo($"EnableTestVersion changed to {Conf.ConfigEnableTestVersion.Value}.");
            Logger.LogInfo("However this won't work until next restart.");
        };
        Logger.LogInfo("TestVersionPatch done.");
    }
}
