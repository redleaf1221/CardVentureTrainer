using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;
using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(BattleObject), nameof(BattleObject.InitAbilityPool))]
public static class SealDataOverridePatch {
    private static ConfigEntry<string> _configSchoolData;

    private static List<int> _patchSchoolData = [];

    // ReSharper disable once MemberCanBePrivate.Global
    public static List<int> SchoolData => _patchSchoolData;

    // ReSharper disable once InconsistentNaming
    private static void Prefix(BattleObject __instance) {
        // to bypass index out of range without transpiler.
        // I'm lazy.
        if (SchoolData.Count == 0) return;
        __instance.schoolDataNow = new List<int>([1200, 1201, 1202, 1299]);
    }

    // ReSharper disable once InconsistentNaming
    private static void Postfix(BattleObject __instance) {
        if (SchoolData.Count <= 0) return;
        Logger.LogInfo("schoolDataNow patched!");
        __instance.schoolDataNow = new List<int>(SchoolData);
    }

    public static void InitPatch() {
        _configSchoolData = Config.Bind("General", "SealDataOverride",
            "", "Override ability pools to choose from.\nLeave empty to disable.\nSample: 1200/1201/1299\n(1200:Bomb, 1201:Bat, 1202:Lightning,\n 1203:Spawn, 1204:Burn, 1205:Shuriken,\n 1206:Prop, 1207:Cannon, 1208:Invincible, 1299:Events)");
        if (!_parseSchoolData(_configSchoolData.Value, out List<int> result)) _configSchoolData.Value = "";
        _patchSchoolData = result;

        HarmonyInstance.PatchAll(typeof(SealDataOverridePatch));
        _configSchoolData.SettingChanged += (sender, args) => {
            Logger.LogInfo($"SealDataList changed to {_configSchoolData.Value}.");
            _parseSchoolData(_configSchoolData.Value, out _patchSchoolData);
        };
        Logger.LogInfo("SealDataOverridePatch done.");
    }

    public static bool TrySetSchoolData(string schoolData) {
        if (!_parseSchoolData(schoolData, out List<int> _)) return false;
        _configSchoolData.Value = schoolData;
        return true;
    }

    private static bool _parseSchoolData(string schoolData, out List<int> result) {
        try {
            result = schoolData.Length > 0 ? schoolData.Split('/').Select(int.Parse).ToList() : [];
            return true;
        } catch {
            result = [];
            return false;
        }
    }
}
