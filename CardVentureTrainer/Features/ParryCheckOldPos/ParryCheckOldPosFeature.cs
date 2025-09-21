using System.Reflection;
using BepInEx.Configuration;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Features.ParryCheckOldPos;

public static class ParryCheckOldPosFeature {
    private static ConfigEntry<bool> _configEnabled;

    public static bool Enabled {
        get => _configEnabled.Value;
        set => _configEnabled.Value = value;
    }

    public static void Init() {
        _configEnabled = Config.Bind("Trainer", "DisableParryOldPosCheck",
            false, "Allow parrying even if perviously in the attack range.");
        HarmonyInstance.PatchAll(typeof(ParryCheckOldPosPatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Logger.LogInfo($"DisableParryOldPosCheck changed to {Enabled}.");
            HarmonyInstance.Unpatch(typeof(UnitObjectAbility).GetMethod(nameof(UnitObjectAbility.AddDamageRange),
                    BindingFlags.Public | BindingFlags.Instance),
                typeof(ParryCheckOldPosPatch).GetMethod(nameof(ParryCheckOldPosPatch.Transpiler),
                    BindingFlags.Public | BindingFlags.Static));
            HarmonyInstance.PatchAll(typeof(ParryCheckOldPosPatch));
        };
        Logger.LogInfo("ParryCheckOldPosFeature loaded.");
    }
}
