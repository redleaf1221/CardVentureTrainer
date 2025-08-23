using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;

namespace CardVentureTrainer;

public class PluginConfig(Plugin plugin) {

    private readonly ConfigEntry<bool> _configAlwaysParrySide = plugin.Config.Bind("Trainer", "EnableParrySide",
        false, "Allow parrying from sides.");

    private readonly ConfigEntry<bool> _configDisableHadoukenNegDamage = plugin.Config.Bind("Trainer", "DisableHadoukenNegativeDamage",
        false, "Disable negative damage of ability Hadouken.");

    private readonly ConfigEntry<bool> _configDisableParryOldPosCheck = plugin.Config.Bind("Trainer", "DisableParryOldPosCheck",
        false, "Allow parrying even if perviously in the attack range.");

    private readonly ConfigEntry<bool> _configEnableChapter3 = plugin.Config.Bind("Demo", "EnableChapter3",
        false, "After finishing demo take you to partly finished chapter 3.");

    private readonly ConfigEntry<bool> _configEnableEasterEggLife = plugin.Config.Bind("General", "EasterEggLife",
        true, "Make probability of encounter easter eggs in room Life bigger.");

    private readonly ConfigEntry<bool> _configEnableTestVersion = plugin.Config.Bind("General", "EnableTestVersion",
        true, "Enable internal debug menu.");

    private readonly ConfigEntry<bool> _configEnableUnusedRooms = plugin.Config.Bind("Demo", "EnableUnusedRooms",
        true, "Enable unused rooms Apple and Soul.");

    private readonly ConfigEntry<float> _configResetOldPosDelay = plugin.Config.Bind("Trainer", "ResetOldPosDelay",
        0.1f, "Adjust delay of resetting oldPos to make parrying easier or harder.");

    private readonly ConfigEntry<string> _configSealDataList = plugin.Config.Bind("General", "SealDataOverride",
        "", "Override ability pools to choose from.\nLeave empty to disable.\nSample: 1200/1201/1299\n(1200:Bomb, 1201:Bat, 1202:Lighting, 1203:Spawn, 1299:Events)");

    public bool EnableChapter3 => _configEnableChapter3.Value;
    public bool EnableEasterEggLife => _configEnableEasterEggLife.Value;
    public bool EnableTestVersion => _configEnableTestVersion.Value;
    public bool EnableUnusedRooms => _configEnableUnusedRooms.Value;
    public bool DisableHadoukenNegDamage => _configDisableHadoukenNegDamage.Value;
    public bool AlwaysParrySide => _configAlwaysParrySide.Value;
    public bool DisableParryOldPosCheck => _configDisableParryOldPosCheck.Value;
    public float ResetOldPosDelay => _configResetOldPosDelay.Value;
    public List<int> SealDataList => _configSealDataList.Value.Length > 0 ? _configSealDataList.Value.Split('/').Select(int.Parse).ToList() : [];
}
