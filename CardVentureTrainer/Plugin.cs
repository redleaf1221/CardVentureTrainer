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

        RegisterEvents();

        Logger.LogMessage($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded!");
    }

    private static void RegisterEvents() {
        TestVersionPatch.RegisterThis(HarmonyInstance);
        SpiderParryEnhancePatch.RegisterThis(HarmonyInstance);
        SealDataOverridePatch.RegisterThis(HarmonyInstance);
        HadoukenRandomDamagePatch.RegisterThis(HarmonyInstance);
        SafeIntPatch.RegisterThis(HarmonyInstance);
        ParrySidePatch.RegisterThis(HarmonyInstance);
        ParryCheckOldPosPatch.RegisterThis(HarmonyInstance);
        FriendUnitLimitPatch.RegisterThis(HarmonyInstance);
        ParryDebugPatch.RegisterThis(HarmonyInstance);
        ResetOldPosDelayPatch.RegisterThis(HarmonyInstance);
    }
}
