using System.Reflection;
using BepInEx.Configuration;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Features.FriendUnitLimit;

public static class FriendUnitLimitFeature {
    private static ConfigEntry<bool> _configEnabled;

    public static bool Enabled {
        get => _configEnabled.Value;
        set => _configEnabled.Value = value;
    }

    public static void Init() {
        _configEnabled = Config.Bind("Trainer", "DisableFriendUnitLimit",
            false, "Disable friend unit spawn limit.");

        HarmonyInstance.PatchAll(typeof(FriendUnitLimitPatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Logger.LogInfo($"DisableFriendUnitLimit changed to {Enabled}.");
            HarmonyInstance.Unpatch(typeof(BattleObject).GetMethod(nameof(BattleObject.SpawnUnit),
                    BindingFlags.Public | BindingFlags.Instance),
                typeof(FriendUnitLimitPatch).GetMethod(nameof(FriendUnitLimitPatch.SpawnUnitTranspiler),
                    BindingFlags.Public | BindingFlags.Static));
            HarmonyInstance.PatchAll(typeof(FriendUnitLimitPatch));
        };
        Logger.LogInfo("FriendUnitLimitFeature loaded.");
    }
}
