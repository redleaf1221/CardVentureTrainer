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
            HarmonyInstance.PatchAll(typeof(EnableTestVersionPatch));
            Logger.LogMessage("TestVersionPatch done.");
        }
        HarmonyInstance.PatchAll(typeof(DiamondShieldSetPricePatch));
        Logger.LogMessage("DiamondShieldSetPricePatch done.");
        HarmonyInstance.PatchAll(typeof(DiamondShieldWeaponDeadPatch));
        Logger.LogMessage("DiamondShieldWeaponDeadPatch done.");
        HarmonyInstance.PatchAll(typeof(DiamondShieldAddWeaponPatch));
        Logger.LogMessage("DiamondShieldAddWeaponPatch done.");
        if (Conf.EnableChapter3) {
            HarmonyInstance.PatchAll(typeof(DemoRunChapterCompletePatch));
            Logger.LogMessage("DemoRunChapterCompletePatch done.");
            HarmonyInstance.PatchAll(typeof(DemoRunStartLevelPatch));
            Logger.LogMessage("DemoRunStartLevelPatch done.");
        }
        if (Conf.EnableUnusedRooms) {
            HarmonyInstance.PatchAll(typeof(DemoRoomGetRandomRoomPatch));
            Logger.LogMessage("DemoRoomGetRandomRoomPatch done.");
        }
        if (Conf.EnableEasterEggLife) {
            HarmonyInstance.PatchAll(typeof(EasterEggLifePatch));
            Logger.LogMessage("EasterEggLifePatch done.");
        }
        if (Conf.EnableSealDataOverride) {
            HarmonyInstance.PatchAll(typeof(SealDataInitAbilityPollPatch));
            Logger.LogMessage("SealDataInitAbilityPollPatch done.");
        }
        if (Conf.DisableHadoukenNegDamage) {
            HarmonyInstance.PatchAll(typeof(HadoukenRandomDamagePatch));
            Logger.LogMessage("HadoukenRandomDamagePatch done.");
        }
        HarmonyInstance.PatchAll(typeof(SafeIntGenerateKeyPatch));
        Logger.LogMessage("SafeIntGenerateKeyPatch done.");
        HarmonyInstance.PatchAll(typeof(SafeIntValueSetterPatch));
        Logger.LogMessage("SafeIntValueGetPatch done.");
        if (Conf.AlwaysParrySide) {
            HarmonyInstance.PatchAll(typeof(ParrySidePatch));
            Logger.LogMessage("ParrySidePatch done.");
        }
        if (Conf.DisableParryOldPosCheck) {
            HarmonyInstance.PatchAll(typeof(ParryCheckOldPosPatch));
            Logger.LogMessage("ParryCheckOldPosPatch done.");
        }
        HarmonyInstance.PatchAll(typeof(ParryDebugPatch));
        Logger.LogMessage("ParryDebugPatch done.");
        if (Conf.DelayResetOldPos) {
            HarmonyInstance.PatchAll(typeof(ParryDelayResetOldPosPatch));
            Logger.LogMessage("ParryDelayResetOldPosPatch done.");
        }
    }
}
