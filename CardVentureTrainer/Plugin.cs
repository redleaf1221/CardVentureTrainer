using System.Diagnostics.CodeAnalysis;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.Mono;
using CardVentureTrainer.Patches;
using HarmonyLib;

namespace CardVentureTrainer;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[SuppressMessage("Class Declaration", "BepInEx002:Classes with BepInPlugin attribute must inherit from BaseUnityPlugin")]
public class Plugin : BaseUnityPlugin {
    public new static ManualLogSource Logger;
    public static PluginConfig Conf;

    // ReSharper disable once MemberCanBePrivate.Global
    public static Harmony HarmonyInstance;


    private void Awake() {
        Logger = base.Logger;

        Conf = new PluginConfig(this);

        HarmonyInstance = new Harmony(MyPluginInfo.PLUGIN_GUID);

        DoAllPatches();

        Logger.LogMessage($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded!");
    }

    private static void DoAllPatches() {
        if (Conf.EnableTestVersion) {
            HarmonyInstance.PatchAll(typeof(TestVersionPatch));
            Logger.LogInfo("TestVersionPatch done.");
        }
        if (Conf.SealDataList.Count > 0) {
            Logger.LogInfo(Conf.SealDataList.Count);
            HarmonyInstance.PatchAll(typeof(SealDataOverridePatch));
            Logger.LogInfo("SealDataOverridePatch done.");
        }
        if (Conf.DisableHadoukenNegDamage) {
            HarmonyInstance.PatchAll(typeof(HadoukenRandomDamagePatch));
            Logger.LogInfo("HadoukenRandomDamagePatch done.");
        }
        HarmonyInstance.PatchAll(typeof(SafeIntPatch));
        Logger.LogInfo("SafeIntPatch done.");
        if (Conf.AlwaysParrySide) {
            HarmonyInstance.PatchAll(typeof(ParrySidePatch));
            Logger.LogInfo("ParrySidePatch done.");
        }
        if (Conf.DisableParryOldPosCheck) {
            HarmonyInstance.PatchAll(typeof(ParryCheckOldPosPatch));
            Logger.LogInfo("ParryCheckOldPosPatch done.");
        }
        if (Conf.SpiderParryEnhance) {
            HarmonyInstance.PatchAll(typeof(SpiderParryEnhance));
            Logger.LogInfo("SpiderParryEnhance done.");
        }
        HarmonyInstance.PatchAll(typeof(ParryDebugPatch));
        Logger.LogInfo("ParryDebugPatch done.");
        HarmonyInstance.PatchAll(typeof(ResetOldPosDelayPatch));
        Logger.LogInfo("ResetOldPosDelayPatch done.");
    }
}
