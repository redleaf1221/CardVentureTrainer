using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;

namespace CardVentureTrainer;

public class PluginConfig(Plugin plugin) {

    private readonly ConfigEntry<bool> _configAlwaysParrySide = plugin.Config.Bind("Trainer", "EnableParrySide",
        false, "Allow parrying from sides.");

    private readonly ConfigEntry<bool> _configDisableFriendUnitLimit = plugin.Config.Bind("Trainer", "DisableFriendUnitLimit",
        false, "Disable friend unit spawn limit.");

    private readonly ConfigEntry<bool> _configDisableHadoukenNegDamage = plugin.Config.Bind("Trainer", "DisableHadoukenNegativeDamage",
        false, "Disable negative damage of ability Hadouken.");

    private readonly ConfigEntry<bool> _configDisableParryOldPosCheck = plugin.Config.Bind("Trainer", "DisableParryOldPosCheck",
        false, "Allow parrying even if perviously in the attack range.");

    private readonly ConfigEntry<bool> _configEnableTestVersion = plugin.Config.Bind("General", "EnableTestVersion",
        true, "Enable internal debug menu.");

    private readonly ConfigEntry<float> _configResetOldPosDelay = plugin.Config.Bind("Trainer", "ResetOldPosDelay",
        0.1f, "Adjust delay of resetting oldPos to make parrying easier or harder.");

    private readonly ConfigEntry<string> _configSealDataList = plugin.Config.Bind("General", "SealDataOverride",
        "", "Override ability pools to choose from.\nLeave empty to disable.\nSample: 1200/1201/1299\n(1200:Bomb, 1201:Bat, 1202:Lighting, 1203:Spawn, 1299:Events)");

    private readonly ConfigEntry<bool> _configSpiderParryEnhance = plugin.Config.Bind("Trainer", "SpiderParryEnhancePatch",
        false, "Reduce animation time to make parrying spiders easier.");

    public bool EnableTestVersion => _configEnableTestVersion.Value;
    public bool DisableHadoukenNegDamage => _configDisableHadoukenNegDamage.Value;
    public bool AlwaysParrySide => _configAlwaysParrySide.Value;
    public bool DisableParryOldPosCheck => _configDisableParryOldPosCheck.Value;
    public float ResetOldPosDelay => _configResetOldPosDelay.Value;
    public List<int> SealDataList => _configSealDataList.Value.Length > 0 ? _configSealDataList.Value.Split('/').Select(int.Parse).ToList() : [];
    public bool SpiderParryEnhance => _configSpiderParryEnhance.Value;
    public bool DisableFriendUnitLimit => _configDisableFriendUnitLimit.Value;
}
