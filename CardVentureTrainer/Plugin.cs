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

    private void Awake() {
        Logger = base.Logger;
        
        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll(typeof(EnableTestVersionPatch));
        Logger.LogMessage("TestVersionPatch done.");
        harmony.PatchAll(typeof(DiamondShieldSetPricePatch));
        Logger.LogMessage("DiamondShieldSetPricePatch done.");
        harmony.PatchAll(typeof(DiamondShieldWeaponDeadPatch));
        Logger.LogMessage("DiamondShieldWeaponDeadPatch done.");
        harmony.PatchAll(typeof(DiamondShieldAddWeaponPatch));
        Logger.LogMessage("DiamondShieldAddWeaponPatch done.");
        harmony.PatchAll(typeof(DemoRunChapterCompletePatch));
        Logger.LogMessage("DemoRunChapterCompletePatch done.");
        harmony.PatchAll(typeof(DemoRunStartLevelPatch));
        Logger.LogMessage("DemoRunStartLevelPatch done.");
        harmony.PatchAll(typeof(DemoRoomGetRandomRoomPatch));
        Logger.LogMessage("DemoRoomGetRandomRoomPatch done.");
        
        Logger.LogMessage($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded!");
    }
}