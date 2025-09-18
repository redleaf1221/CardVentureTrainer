using BepInEx.Configuration;
using BepInEx.Unity.Mono.Configuration;
using UnityEngine;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.UI;

public class WindowManager : MonoBehaviour {

    private static ConfigEntry<KeyboardShortcut> _configShowMainWindowHotkey;

    public static MainWindow MainWindow;

    public static SchoolDataWindow SchoolDataWindow;

    public void Awake() {
        _configShowMainWindowHotkey = Config.Bind("Hotkeys", "ShowMainWindowHotkey",
            new KeyboardShortcut(KeyCode.F12), "Hotkey of main window");
        MainWindow = new MainWindow();
        SchoolDataWindow = new SchoolDataWindow();
    }

    public void Update() {
        if (_configShowMainWindowHotkey.Value.IsDown()) {
            MainWindow.ToggleDisplay();
        }
    }

    public void OnGUI() {
        MainWindow.Draw();
        SchoolDataWindow.Draw();
    }
}
