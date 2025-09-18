using System.Globalization;
using CardVentureTrainer.Patches;
using UnityEngine;

namespace CardVentureTrainer.UI;

public class MainWindow {

    private static string _resetOldPosDelayString = ResetOldPosDelayPatch.Delay.ToString(CultureInfo.InvariantCulture);

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
                using (new GUI.ColorScope(TestVersionPatch.Enabled ? Color.green : GUI.color)) {
                    if (GUILayout.Button("TestVersion", GUILayout.Width(320))) {
                        TestVersionPatch.Enabled = !TestVersionPatch.Enabled;
                    }
                }
                using (new GUI.ColorScope(SpiderParryEnhancePatch.Enabled ? Color.green : GUI.color)) {
                    if (GUILayout.Button("SpiderParryEnhance", GUILayout.Width(320))) {
                        SpiderParryEnhancePatch.Enabled = !SpiderParryEnhancePatch.Enabled;
                    }
                }
            }
            using (new GUILayout.HorizontalScope(GUILayout.ExpandWidth(true))) {
                using (new GUI.ColorScope(HadoukenRandomDamagePatch.Enabled ? Color.green : GUI.color)) {
                    if (GUILayout.Button("DisableHadoukenNegDamage", GUILayout.Width(320))) {
                        HadoukenRandomDamagePatch.Enabled = !HadoukenRandomDamagePatch.Enabled;
                    }
                }
                using (new GUI.ColorScope(ParrySidePatch.Enabled ? Color.green : GUI.color)) {
                    if (GUILayout.Button("ParrySide", GUILayout.Width(320))) {
                        ParrySidePatch.Enabled = !ParrySidePatch.Enabled;
                    }
                }
            }
            using (new GUILayout.HorizontalScope(GUILayout.ExpandWidth(true))) {
                using (new GUI.ColorScope(ParryCheckOldPosPatch.Enabled ? Color.green : GUI.color)) {
                    if (GUILayout.Button("DisableParryOldPosCheck", GUILayout.Width(320))) {
                        ParryCheckOldPosPatch.Enabled = !ParryCheckOldPosPatch.Enabled;
                    }
                }
                using (new GUI.ColorScope(FriendUnitLimitPatch.Enabled ? Color.green : GUI.color)) {
                    if (GUILayout.Button("DisableFriendUnitLimit", GUILayout.Width(320))) {
                        FriendUnitLimitPatch.Enabled = !FriendUnitLimitPatch.Enabled;
                    }
                }
            }
            using (new GUILayout.HorizontalScope(GUILayout.ExpandWidth(true))) {
                using (new GUI.ColorScope(!Mathf.Approximately(ResetOldPosDelayPatch.Delay, 0.1f) ? Color.green : GUI.color)) {
                    GUILayout.Label("ResetOldPosDelay", GUILayout.Width(120));
                }
                var newVal = GUILayout.TextField(_resetOldPosDelayString, GUILayout.Width(180));
                if (newVal != _resetOldPosDelayString) {
                    _resetOldPosDelayString = newVal;
                    var succeed = float.TryParse(newVal, out var result);
                    _resetOldPosDelaySetSucceed = succeed;
                    if (succeed) {
                        var succeed2 = ResetOldPosDelayPatch.TrySetDelay(result);
                        _resetOldPosDelaySetSucceed = succeed2;
                    }
                }
                using (new GUI.ColorScope(_resetOldPosDelaySetSucceed ? Color.green : Color.red)) {
                    GUILayout.Label("S", GUILayout.Width(10));
                }

                using (new GUI.ColorScope(SchoolDataOverridePatch.SchoolData.Count > 0 ? Color.green : GUI.color)) {
                    if (GUILayout.Button("SchoolDataOverride", GUILayout.Width(320))) {
                        WindowManager.SchoolDataWindow.ToggleDisplay();
                    }
                }
            }
        }
        GUI.DragWindow();
    }
}
