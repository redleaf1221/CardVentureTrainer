using System.Reflection;
using BepInEx.Configuration;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Features.HdkNegDamage;

public static class HdkNegDamageFeature {
    private static ConfigEntry<bool> _configEnabled;

    public static bool Enabled {
        get => _configEnabled.Value;
        set => _configEnabled.Value = value;
    }

    public static void Init() {
        _configEnabled = Config.Bind("Trainer", "DisableHdkNegDamage",
            false, "Disable negative damage of ability Hadouken.");

        HarmonyInstance.PatchAll(typeof(HdkNegDamagePatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Logger.LogInfo($"DisableHdkNegDamage changed to {Enabled}.");
            HarmonyInstance.Unpatch(typeof(UnitObjectPlayer).GetMethod(nameof(UnitObjectPlayer.PlayerInputCheck),
                    BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(HdkNegDamagePatch).GetMethod(nameof(HdkNegDamagePatch.Transpiler),
                    BindingFlags.Public | BindingFlags.Static));
            HarmonyInstance.PatchAll(typeof(HdkNegDamagePatch));
        };
        Logger.LogInfo("HdkNegDamageFeature loaded.");
    }
}
