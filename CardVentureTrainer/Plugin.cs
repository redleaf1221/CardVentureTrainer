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
    private ConfigEntry<bool> _configChapter3;
    private ConfigEntry<bool> _configDiamondShield;
    private ConfigEntry<bool> _configEasterEggLife;
    private ConfigEntry<bool> _configTestVersion;
    private ConfigEntry<bool> _configUnusedRooms;

    private void Awake() {
        Logger = base.Logger;

        _configTestVersion = Config.Bind("General", "EnableTestVersion",
            true, "Enable internal debug menu.");
        _configDiamondShield = Config.Bind("Demo", "EnableDiamondShield",
            true, "Allow acquiring diamond shield.");
        _configChapter3 = Config.Bind("Demo", "EnableChapter3",
            false, "After finishing demo take you to partly finished chapter 3.");
        _configUnusedRooms = Config.Bind("Demo", "EnableUnusedRooms",
            true, "Enable unused rooms Apple and Soul.");
        _configEasterEggLife = Config.Bind("Probability", "EasterEggLife",
            true, "Make probability of encounter easter egg in room Life bigger.");

        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        if (_configTestVersion.Value) {
            harmony.PatchAll(typeof(EnableTestVersionPatch));
            Logger.LogMessage("TestVersionPatch done.");
        }
        if (_configDiamondShield.Value) {
            harmony.PatchAll(typeof(DiamondShieldSetPricePatch));
            Logger.LogMessage("DiamondShieldSetPricePatch done.");
            harmony.PatchAll(typeof(DiamondShieldWeaponDeadPatch));
            Logger.LogMessage("DiamondShieldWeaponDeadPatch done.");
            harmony.PatchAll(typeof(DiamondShieldAddWeaponPatch));
            Logger.LogMessage("DiamondShieldAddWeaponPatch done.");
        }
        if (_configChapter3.Value) {
            harmony.PatchAll(typeof(DemoRunChapterCompletePatch));
            Logger.LogMessage("DemoRunChapterCompletePatch done.");
            harmony.PatchAll(typeof(DemoRunStartLevelPatch));
            Logger.LogMessage("DemoRunStartLevelPatch done.");
        }
        if (_configUnusedRooms.Value) {
            harmony.PatchAll(typeof(DemoRoomGetRandomRoomPatch));
            Logger.LogMessage("DemoRoomGetRandomRoomPatch done.");
        }
        if (_configEasterEggLife.Value) {
            harmony.PatchAll(typeof(EasterEggLifePatch));
            Logger.LogMessage("EasterEggLifePatch done.");
        }

        Logger.LogMessage($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded!");
    }
}
