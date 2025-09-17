using System.Diagnostics.CodeAnalysis;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.Mono;
using CardVentureTrainer.Patches;
using HarmonyLib;

namespace CardVentureTrainer;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[SuppressMessage("Class Declaration", "BepInEx002:Classes with BepInPlugin attribute must inherit from BaseUnityPlugin")]
public class Plugin : BaseUnityPlugin {
    public new static ManualLogSource Logger;

    public new static ConfigFile Config;

    public static Harmony HarmonyInstance;


    private void Awake() {
        Logger = base.Logger;
        Config = base.Config;

        HarmonyInstance = new Harmony(MyPluginInfo.PLUGIN_GUID);

        InitPatches();

        Logger.LogMessage($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded!");
    }

    private static void InitPatches() {
        TestVersionPatch.InitPatch();
        SpiderParryEnhancePatch.InitPatch();
        SealDataOverridePatch.InitPatch();
        HadoukenRandomDamagePatch.InitPatch();
        SafeIntPatch.InitPatch();
        ParrySidePatch.InitPatch();
        ParryCheckOldPosPatch.InitPatch();
        FriendUnitLimitPatch.InitPatch();
        ParryDebugPatch.InitPatch();
        ResetOldPosDelayPatch.InitPatch();
    }
}
