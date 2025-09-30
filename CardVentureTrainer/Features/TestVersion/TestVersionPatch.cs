using HarmonyLib;

namespace CardVentureTrainer.Features.TestVersion;

[HarmonyPatch(typeof(GameController), nameof(GameController.Awake))]
public static class TestVersionPatch {

    public static void Postfix() {
        GameSettings.testVersion = TestVersionFeature.Enabled;
    }
}
