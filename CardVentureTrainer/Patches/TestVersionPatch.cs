using HarmonyLib;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(GameController), nameof(GameController.Awake))]
public static class TestVersionPatch {
    static void Postfix() {
        GameSettings.testVersion = true;
    }
}
