using BepInEx.Configuration;

namespace CardVentureTrainer;

public class PluginConfig(Plugin plugin) {
    public readonly ConfigEntry<bool> ConfigChapter3 = plugin.Config.Bind("Demo", "EnableChapter3",
        false, "After finishing demo take you to partly finished chapter 3.");

    public readonly ConfigEntry<bool> ConfigDiamondShield = plugin.Config.Bind("Demo", "EnableDiamondShield",
        true, "Allow acquiring diamond shield.");

    public readonly ConfigEntry<bool> ConfigEasterEggLife = plugin.Config.Bind("Probability", "EasterEggLife",
        true, "Make probability of encounter easter egg in room Life bigger.");

    public readonly ConfigEntry<bool> ConfigTestVersion = plugin.Config.Bind("General", "EnableTestVersion",
        true, "Enable internal debug menu.");

    public readonly ConfigEntry<bool> ConfigUnusedRooms = plugin.Config.Bind("Demo", "EnableUnusedRooms",
        true, "Enable unused rooms Apple and Soul.");
}
