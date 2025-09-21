using CardVentureTrainer.Features.CoinSoulRoom;
using CardVentureTrainer.Features.FriendUnitLimit;
using CardVentureTrainer.Features.HdkNegDamage;
using CardVentureTrainer.Features.Highlight;
using CardVentureTrainer.Features.ParryCheckOldPos;
using CardVentureTrainer.Features.ParryDebug;
using CardVentureTrainer.Features.ParrySide;
using CardVentureTrainer.Features.ResetOldPosDelay;
using CardVentureTrainer.Features.SafeInt;
using CardVentureTrainer.Features.SchoolDataOverride;
using CardVentureTrainer.Features.SpiderParryEnhance;
using CardVentureTrainer.Features.TestVersion;

namespace CardVentureTrainer.Features;

public static class FeatureManager {
    public static void InitFeatures() {
        TestVersionFeature.Init();
        SpiderParryEnhanceFeature.Init();
        SchoolDataOverrideFeature.Init();
        HdkNegDamageFeature.Init();
        SafeIntFeature.Init();
        ParrySideFeature.Init();
        ParryCheckOldPosFeature.Init();
        FriendUnitLimitFeature.Init();
        ParryDebugFeature.Init();
        ResetOldPosDelayFeature.Init();
        CoinSoulRoomFeature.Init();
        HighlightFeature.Init();
    }
}
