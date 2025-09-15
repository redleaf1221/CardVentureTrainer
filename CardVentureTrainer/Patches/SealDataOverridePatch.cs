using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.Patches;

[HarmonyPatch(typeof(BattleObject), nameof(BattleObject.InitAbilityPool))]
public static class SealDataOverridePatch {
    private static List<int> _patchSchoolData = [];

    // ReSharper disable once InconsistentNaming
    private static void Prefix(BattleObject __instance) {
        // to bypass index out of range without transpiler.
        // I'm lazy.
        if (_patchSchoolData.Count > 0) {
            __instance.schoolDataNow = new List<int>([1200, 1201, 1202, 1299]);
        }
    }

    // ReSharper disable once InconsistentNaming
    private static void Postfix(BattleObject __instance) {
        if (_patchSchoolData.Count > 0) {
            __instance.schoolDataNow = _patchSchoolData;
        }
    }

    public static void RegisterThis(Harmony harmony) {
        harmony.PatchAll(typeof(SealDataOverridePatch));
        _updateSchoolData();
        Conf.ConfigSealDataList.SettingChanged += (sender, args) => {
            Logger.LogInfo($"SealDataList changed to {Conf.ConfigSealDataList.Value}.");
            _updateSchoolData();
        };
        Logger.LogInfo("SealDataOverridePatch done.");
    }

    private static void _updateSchoolData() {
        try {
            _patchSchoolData = Conf.ConfigSealDataList.Value.Length > 0 ? Conf.ConfigSealDataList.Value.Split('/').Select(int.Parse).ToList() : [];
        } catch (Exception e) {
            Logger.LogError(e);
            _patchSchoolData = [];
        }
    }
}
