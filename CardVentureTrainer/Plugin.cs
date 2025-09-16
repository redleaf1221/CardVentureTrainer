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

    // ReSharper disable once MemberCanBePrivate.Global
    public static Harmony HarmonyInstance;


    private void Awake() {
        Logger = base.Logger;

        HarmonyInstance = new Harmony(MyPluginInfo.PLUGIN_GUID);

        RegisterEvents();

        Logger.LogMessage($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded!");
    }

    private void RegisterEvents() {
        TestVersionPatch.InitConfig(this);
        TestVersionPatch.RegisterThis(HarmonyInstance);

        SpiderParryEnhancePatch.InitConfig(this);
        SpiderParryEnhancePatch.RegisterThis(HarmonyInstance);

        SealDataOverridePatch.InitConfig(this);
        SealDataOverridePatch.RegisterThis(HarmonyInstance);

        HadoukenRandomDamagePatch.InitConfig(this);
        HadoukenRandomDamagePatch.RegisterThis(HarmonyInstance);

        SafeIntPatch.RegisterThis(HarmonyInstance);

        ParrySidePatch.InitConfig(this);
        ParrySidePatch.RegisterThis(HarmonyInstance);

        ParryCheckOldPosPatch.InitConfig(this);
        ParryCheckOldPosPatch.RegisterThis(HarmonyInstance);

        FriendUnitLimitPatch.InitConfig(this);
        FriendUnitLimitPatch.RegisterThis(HarmonyInstance);

        ParryDebugPatch.RegisterThis(HarmonyInstance);

        ResetOldPosDelayPatch.InitConfig(this);
        ResetOldPosDelayPatch.RegisterThis(HarmonyInstance);
    }
}
