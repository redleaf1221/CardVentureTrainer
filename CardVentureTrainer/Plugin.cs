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

    private static Harmony _harmonyInstance;


    private void Awake() {
        Logger = base.Logger;

        _harmonyInstance = new Harmony(MyPluginInfo.PLUGIN_GUID);

        InitPatches();

        Logger.LogMessage($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded!");
    }

    private void InitPatches() {
        TestVersionPatch.InitPatch(this, _harmonyInstance);
        SpiderParryEnhancePatch.InitPatch(this, _harmonyInstance);
        SealDataOverridePatch.InitPatch(this, _harmonyInstance);
        HadoukenRandomDamagePatch.InitPatch(this, _harmonyInstance);
        SafeIntPatch.InitPatch(this, _harmonyInstance);
        ParrySidePatch.InitPatch(this, _harmonyInstance);
        ParryCheckOldPosPatch.InitPatch(this, _harmonyInstance);
        FriendUnitLimitPatch.InitPatch(this, _harmonyInstance);
        ParryDebugPatch.InitPatch(this, _harmonyInstance);
        ResetOldPosDelayPatch.InitPatch(this, _harmonyInstance);
    }
}
