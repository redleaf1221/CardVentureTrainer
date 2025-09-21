using System.Reflection;
using BepInEx.Configuration;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Features.CoinSoulRoom;

public static class CoinSoulRoomFeature {

    private static ConfigEntry<bool> _configEnabled;

    public static bool Enabled {
        get => _configEnabled.Value;
        set => _configEnabled.Value = value;
    }


    public static void Init() {
        _configEnabled = Config.Bind("Trainer", "DisableCoinSoulRoom",
            false, "Replace generated coin and soul room.");
        HarmonyInstance.PatchAll(typeof(CoinSoulRoomPatch));
        _configEnabled.SettingChanged += (sender, args) => {
            Logger.LogInfo($"DisableCoinSoulRoom changed to {Enabled}.");
            HarmonyInstance.Unpatch(typeof(BattleObject).GetMethod(nameof(BattleObject.GenerateChapterRoomSequence),
                    BindingFlags.Public | BindingFlags.Instance),
                typeof(CoinSoulRoomPatch).GetMethod(nameof(CoinSoulRoomPatch.Transpiler),
                    BindingFlags.Public | BindingFlags.Static));
            HarmonyInstance.PatchAll(typeof(CoinSoulRoomPatch));
        };
        Logger.LogInfo("CoinSoulRoomFeature loaded.");
    }
}
