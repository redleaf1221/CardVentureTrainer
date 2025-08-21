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


    private void Awake() {
        Logger = base.Logger;

        Conf = new PluginConfig(this);

        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        if (Conf.EnableTestVersion) {
            harmony.PatchAll(typeof(EnableTestVersionPatch));
            Logger.LogMessage("TestVersionPatch done.");
        }
        if (Conf.EnableDiamondShield) {
            harmony.PatchAll(typeof(DiamondShieldSetPricePatch));
            Logger.LogMessage("DiamondShieldSetPricePatch done.");
            harmony.PatchAll(typeof(DiamondShieldWeaponDeadPatch));
            Logger.LogMessage("DiamondShieldWeaponDeadPatch done.");
            harmony.PatchAll(typeof(DiamondShieldAddWeaponPatch));
            Logger.LogMessage("DiamondShieldAddWeaponPatch done.");
        }
        if (Conf.EnableChapter3) {
            harmony.PatchAll(typeof(DemoRunChapterCompletePatch));
            Logger.LogMessage("DemoRunChapterCompletePatch done.");
            harmony.PatchAll(typeof(DemoRunStartLevelPatch));
            Logger.LogMessage("DemoRunStartLevelPatch done.");
        }
        if (Conf.EnableUnusedRooms) {
            harmony.PatchAll(typeof(DemoRoomGetRandomRoomPatch));
            Logger.LogMessage("DemoRoomGetRandomRoomPatch done.");
        }
        if (Conf.EnableEasterEggLife) {
            harmony.PatchAll(typeof(EasterEggLifePatch));
            Logger.LogMessage("EasterEggLifePatch done.");
        }
        if (Conf.EnableSealDataOverride) {
            harmony.PatchAll(typeof(SealDataInitAbilityPollPatch));
            Logger.LogMessage("SealDataInitAbilityPollPatch done.");
        }
        if (Conf.DisableHadoukenNegDamage) {
            harmony.PatchAll(typeof(HadoukenRandomDamagePatch));
            Logger.LogMessage("HadoukenRandomDamagePatch done.");
        }

        Logger.LogMessage($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded!");
    }
}
