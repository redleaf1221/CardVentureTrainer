using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;

namespace CardVentureTrainer;

public class PluginConfig(Plugin plugin) {

    private readonly ConfigEntry<bool> _configDisableHadoukenNegDamage = plugin.Config.Bind("RNG", "DisableHadoukenNegativeDamage",
        false, "Disable negative damage of ability Hadouken.");

    private readonly ConfigEntry<bool> _configDisableSafeInt = plugin.Config.Bind("General", "DisableSafeInt",
        true, "Disable SafeInt so Cheat Engine works again.");

    private readonly ConfigEntry<bool> _configEnableChapter3 = plugin.Config.Bind("Demo", "EnableChapter3",
        false, "After finishing demo take you to partly finished chapter 3.");

    private readonly ConfigEntry<bool> _configEnableDiamondShield = plugin.Config.Bind("Demo", "EnableDiamondShield",
        true, "Allow acquiring diamond shield.");

    private readonly ConfigEntry<bool> _configEnableEasterEggLife = plugin.Config.Bind("RNG", "EasterEggLife",
        true, "Make probability of encounter easter egg in room Life bigger.");

    private readonly ConfigEntry<bool> _configEnableSealDataOverride = plugin.Config.Bind("General", "EnableSealDataOverride",
        false, "Limit the ability pools to choose from.");

    private readonly ConfigEntry<bool> _configEnableTestVersion = plugin.Config.Bind("General", "EnableTestVersion",
        true, "Enable internal debug menu.");

    private readonly ConfigEntry<bool> _configEnableUnusedRooms = plugin.Config.Bind("Demo", "EnableUnusedRooms",
        true, "Enable unused rooms Apple and Soul.");

    private readonly ConfigEntry<string> _configSealDataList = plugin.Config.Bind("General", "SealDataOverride",
        "1200/1201/1202/1299", "Ability pools to choose from.\n(1200:Bomb, 1201:Bat, 1202:Lighting, 1203:Spawn, 1299:Events)");

    public bool EnableChapter3 => _configEnableChapter3.Value;
    public bool EnableDiamondShield => _configEnableDiamondShield.Value;
    public bool EnableEasterEggLife => _configEnableEasterEggLife.Value;
    public bool EnableSealDataOverride => _configEnableSealDataOverride.Value;
    public bool EnableTestVersion => _configEnableTestVersion.Value;
    public bool EnableUnusedRooms => _configEnableUnusedRooms.Value;
    public bool DisableHadoukenNegDamage => _configDisableHadoukenNegDamage.Value;
    public bool DisableSafeInt => _configDisableSafeInt.Value;
    public List<int> SealDataList => _configSealDataList.Value.Split('/').Select(int.Parse).ToList();
}
