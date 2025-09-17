using BepInEx.Configuration;
using BepInEx.Unity.Mono.Configuration;
using UnityEngine;
using static CardVentureTrainer.Plugin;

namespace CardVentureTrainer.UI;

public class WindowManager : MonoBehaviour {

    private static ConfigEntry<KeyboardShortcut> _configShowMainWindowHotkey;

    private static MainWindow _mainWindow;

    public void Awake() {
        _configShowMainWindowHotkey = Config.Bind("Hotkeys", "ShowMainWindowHotkey",
            new KeyboardShortcut(KeyCode.F12), "Hotkey of main window");
        _mainWindow = new MainWindow();
    }

    public void Update() {
        if (_configShowMainWindowHotkey.Value.IsDown()) {
            _mainWindow.ToggleDisplay();
        }
    }

    public void OnGUI() {
        _mainWindow.Draw();
    }
}
