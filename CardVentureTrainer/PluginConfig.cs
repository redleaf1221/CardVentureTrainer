using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;

namespace CardVentureTrainer;

public class PluginConfig(Plugin plugin) {
    public readonly ConfigEntry<bool> ConfigChapter3 = plugin.Config.Bind("Demo", "EnableChapter3",
        false, "After finishing demo take you to partly finished chapter 3.");

    public readonly ConfigEntry<bool> ConfigDiamondShield = plugin.Config.Bind("Demo", "EnableDiamondShield",
        true, "Allow acquiring diamond shield.");

    public readonly ConfigEntry<bool> ConfigEasterEggLife = plugin.Config.Bind("General", "EasterEggLife",
        true, "Make probability of encounter easter egg in room Life bigger.");

    public readonly ConfigEntry<string> ConfigSealData = plugin.Config.Bind("General", "SealDataOverride",
        "1200/1201/1202/1299", "Ability pools to choose from.\n(1200:Bomb, 1201:Bat, 1202:Lighting, 1203:Spawn, 1299:Events)");

    public readonly ConfigEntry<bool> ConfigTestVersion = plugin.Config.Bind("General", "EnableTestVersion",
        true, "Enable internal debug menu.");

    public readonly ConfigEntry<bool> ConfigUnusedRooms = plugin.Config.Bind("Demo", "EnableUnusedRooms",
        true, "Enable unused rooms Apple and Soul.");

    public readonly ConfigEntry<bool> ConfigUseSealData = plugin.Config.Bind("General", "EnableSealDataOverride",
        false, "Limit the ability pools to choose from.");

    public List<int> ConfigSealDataList => ConfigSealData.Value.Split('/').Select(int.Parse).ToList();
}
