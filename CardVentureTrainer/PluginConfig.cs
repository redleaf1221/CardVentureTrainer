using BepInEx.Configuration;

namespace CardVentureTrainer;

public class PluginConfig(Plugin plugin) {

    public readonly ConfigEntry<bool> ConfigAlwaysParrySide = plugin.Config.Bind("Trainer", "EnableParrySide",
        false, "Allow parrying from sides.");

    public readonly ConfigEntry<bool> ConfigDisableFriendUnitLimit = plugin.Config.Bind("Trainer", "DisableFriendUnitLimit",
        false, "Disable friend unit spawn limit.");

    public readonly ConfigEntry<bool> ConfigDisableHadoukenNegDamage = plugin.Config.Bind("Trainer", "DisableHadoukenNegativeDamage",
        false, "Disable negative damage of ability Hadouken.");

    public readonly ConfigEntry<bool> ConfigDisableParryOldPosCheck = plugin.Config.Bind("Trainer", "DisableParryOldPosCheck",
        false, "Allow parrying even if perviously in the attack range.");

    public readonly ConfigEntry<bool> ConfigEnableTestVersion = plugin.Config.Bind("General", "EnableTestVersion",
        true, "Enable internal debug menu.");

    public readonly ConfigEntry<float> ConfigResetOldPosDelay = plugin.Config.Bind("Trainer", "ResetOldPosDelay",
        0.1f, "Adjust delay of resetting oldPos to make parrying easier or harder.");

    public readonly ConfigEntry<string> ConfigSealDataList = plugin.Config.Bind("General", "SealDataOverride",
        "", "Override ability pools to choose from.\nLeave empty to disable.\nSample: 1200/1201/1299\n(1200:Bomb, 1201:Bat, 1202:Lightning,\n 1203:Spawn, 1204:Burn, 1205:Shuriken,\n 1206:Prop, 1207:Cannon, 1208:Invincible, 1299:Events)");

    public readonly ConfigEntry<bool> ConfigSpiderParryEnhance = plugin.Config.Bind("Trainer", "SpiderParryEnhance",
        false, "Reduce animation time to make parrying spiders easier.");
}
