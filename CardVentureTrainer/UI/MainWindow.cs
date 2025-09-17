using CardVentureTrainer.Patches;
using UnityEngine;

namespace CardVentureTrainer.UI;

public class MainWindow {
    private Rect _windowRect = new(100, 100, 640, 480);
    public bool Displaying { get; private set; }

    public void ToggleDisplay() {
        Displaying = !Displaying;
    }

    public void Draw() {
        if (!Displaying) return;
        _windowRect = GUILayout.Window(20420001, _windowRect, DoWindow, "Main Window");
    }

    private static void DoWindow(int windowID) {
        using (new GUILayout.VerticalScope()) {
            GUILayout.Label("Welcome to the Card Venture Trainer!");
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
        }
        GUI.DragWindow();
    }
}
