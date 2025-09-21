using System.Globalization;
using CardVentureTrainer.Features;
using CardVentureTrainer.Features.CoinSoulRoom;
using CardVentureTrainer.Features.FriendUnitLimit;
using CardVentureTrainer.Features.HdkNegDamage;
using CardVentureTrainer.Features.Highlight;
using CardVentureTrainer.Features.ParryCheckOldPos;
using CardVentureTrainer.Features.ParrySide;
using CardVentureTrainer.Features.ResetOldPosDelay;
using CardVentureTrainer.Features.SchoolDataOverride;
using CardVentureTrainer.Features.SpiderParryEnhance;
using CardVentureTrainer.Features.TestVersion;
using UnityEngine;

namespace CardVentureTrainer.UI;

public class MainWindow {

    private static string _resetOldPosDelayString = ResetOldPosDelayFeature.Delay.ToString(CultureInfo.InvariantCulture);

    private static bool _resetOldPosDelaySetSucceed = true;
    private Rect _windowRect = new(100, 100, 640, 150);

    public bool Displaying { get; private set; }

    public void ToggleDisplay() {
        Displaying = !Displaying;
        if (!Displaying && WindowManager.SchoolDataWindow.Displaying) {
            WindowManager.SchoolDataWindow.ToggleDisplay();
        }
    }

    public void Draw() {
        if (!Displaying) return;
        _windowRect = GUILayout.Window(20420001, _windowRect, DoWindow, "CardVentureTrainer");
    }

    private static void DoWindow(int windowID) {
        using (new GUILayout.VerticalScope()) {
            GUILayout.Label("Welcome to the CardVentureTrainer!");
            using (new GUILayout.HorizontalScope(GUILayout.ExpandWidth(true))) {
                using (new GUI.ColorScope(TestVersionFeature.Enabled ? Color.green : GUI.color)) {
                    if (GUILayout.Button("TestVersion", GUILayout.Width(320))) {
                        TestVersionFeature.Enabled = !TestVersionFeature.Enabled;
                    }
                }
                using (new GUI.ColorScope(SpiderParryEnhanceFeature.Enabled ? Color.green : GUI.color)) {
                    if (GUILayout.Button("SpiderParryEnhance", GUILayout.Width(320))) {
                        SpiderParryEnhanceFeature.Enabled = !SpiderParryEnhanceFeature.Enabled;
                    }
                }
            }
            using (new GUILayout.HorizontalScope(GUILayout.ExpandWidth(true))) {
                using (new GUI.ColorScope(HdkNegDamageFeature.Enabled ? Color.green : GUI.color)) {
                    if (GUILayout.Button("DisableHadoukenNegDamage", GUILayout.Width(320))) {
                        HdkNegDamageFeature.Enabled = !HdkNegDamageFeature.Enabled;
                    }
                }
                using (new GUI.ColorScope(ParrySideFeature.Enabled ? Color.green : GUI.color)) {
                    if (GUILayout.Button("ParrySide", GUILayout.Width(320))) {
                        ParrySideFeature.Enabled = !ParrySideFeature.Enabled;
                    }
                }
            }
            using (new GUILayout.HorizontalScope(GUILayout.ExpandWidth(true))) {
                using (new GUI.ColorScope(ParryCheckOldPosFeature.Enabled ? Color.green : GUI.color)) {
                    if (GUILayout.Button("DisableParryOldPosCheck", GUILayout.Width(320))) {
                        ParryCheckOldPosFeature.Enabled = !ParryCheckOldPosFeature.Enabled;
                    }
                }
                using (new GUI.ColorScope(FriendUnitLimitFeature.Enabled ? Color.green : GUI.color)) {
                    if (GUILayout.Button("DisableFriendUnitLimit", GUILayout.Width(320))) {
                        FriendUnitLimitFeature.Enabled = !FriendUnitLimitFeature.Enabled;
                    }
                }
            }
            using (new GUILayout.HorizontalScope(GUILayout.ExpandWidth(true))) {
                using (new GUI.ColorScope(CoinSoulRoomFeature.Enabled ? Color.green : GUI.color)) {
                    if (GUILayout.Button("DisableCoinSoulRoom", GUILayout.Width(320))) {
                        CoinSoulRoomFeature.Enabled = !CoinSoulRoomFeature.Enabled;
                    }
                }
                if (GUILayout.Button("HighlightTest", GUILayout.Width(320))) {
                    HighlightFeature.HighlightLattice(BattleObject.Instance.playerObject.unitPos, new Color(1, 0, 0, 0.5f));
                }
            }
            using (new GUILayout.HorizontalScope(GUILayout.ExpandWidth(true))) {
                using (new GUI.ColorScope(!Mathf.Approximately(ResetOldPosDelayFeature.Delay, 0.1f) ? Color.green : GUI.color)) {
                    GUILayout.Label("ResetOldPosDelay", GUILayout.Width(120));
                }
                var newVal = GUILayout.TextField(_resetOldPosDelayString, GUILayout.Width(180));
                if (newVal != _resetOldPosDelayString) {
                    _resetOldPosDelayString = newVal;
                    var succeed = float.TryParse(newVal, out var result);
                    _resetOldPosDelaySetSucceed = succeed;
                    if (succeed) {
                        var succeed2 = ResetOldPosDelayFeature.TrySetDelay(result);
                        _resetOldPosDelaySetSucceed = succeed2;
                    }
                }
                using (new GUI.ColorScope(_resetOldPosDelaySetSucceed ? Color.green : Color.red)) {
                    GUILayout.Label("S", GUILayout.Width(10));
                }

                using (new GUI.ColorScope(SchoolDataOverrideFeature.SchoolData.Count > 0 ? Color.green : GUI.color)) {
                    if (GUILayout.Button("SchoolDataOverride", GUILayout.Width(320))) {
                        WindowManager.SchoolDataWindow.ToggleDisplay();
                    }
                }
            }
        }
        GUI.DragWindow();
    }
}
