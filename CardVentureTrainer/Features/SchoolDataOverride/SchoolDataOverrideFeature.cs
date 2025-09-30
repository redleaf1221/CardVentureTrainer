using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;

namespace CardVentureTrainer.Features.SchoolDataOverride;

public static class SchoolDataOverrideFeature {
    private static ConfigEntry<string> _configSchoolData;
    private static List<int> _patchSchoolData = [];

    public static readonly Dictionary<int, string> SchoolDataNames = new() {
        { 1200, "Bomb" },
        { 1201, "Bat" },
        { 1202, "Lightning" },
        { 1203, "Spawn" },
        { 1204, "Burn" },
        { 1205, "Shuriken" },
        { 1206, "Prop" },
        { 1207, "Cannon" },
        { 1208, "Invincible" },
        { 1299, "Events" }
    };

    public static List<int> SchoolData => _patchSchoolData;
    public static void Init() {
        _configSchoolData = Plugin.Config.Bind("General", "SchoolDataOverride",
            "", "Override ability pools to choose from.\nLeave empty to disable.");
        if (!_parseSchoolData(_configSchoolData.Value, out List<int> result)) _configSchoolData.Value = "";
        _patchSchoolData = result;

        Plugin.HarmonyInstance.PatchAll(typeof(SchoolDataOverridePatch));
        _configSchoolData.SettingChanged += (sender, args) => {
            Plugin.Logger.LogInfo($"SchoolDataOverride changed to {_configSchoolData.Value}.");
            _parseSchoolData(_configSchoolData.Value, out _patchSchoolData);
        };
        Plugin.Logger.LogInfo("SchoolDataOverrideFeature loaded.");
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
