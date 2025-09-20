using System.Collections.Generic;
using System.Linq;
using CardVentureTrainer.Patches;
using CardVentureTrainer.Utils;
using UnityEngine;

namespace CardVentureTrainer.UI;

public class SchoolDataWindow {
    private static readonly Dictionary<int, bool> SchoolDataDictionary = DefinedConsts.SchoolDataNames.Keys
        .ToDictionary(id => id, id => SchoolDataOverridePatch.SchoolData.Contains(id));

    private Rect _windowRect = new(200, 100, 100, 200);

    public bool Displaying { get; private set; }

    public void ToggleDisplay() {
        Displaying = !Displaying;
    }

    public void Draw() {
        if (!Displaying) return;
        _windowRect = GUILayout.Window(20420002, _windowRect, DoWindow, "SchoolData");
    }

    private static void DoWindow(int windowID) {
        using (new GUILayout.VerticalScope()) {
            foreach (var (id, name) in DefinedConsts.SchoolDataNames) {
                SchoolDataDictionary[id] = GUILayout.Toggle(SchoolDataDictionary[id], name);
            }
        }
        GUI.DragWindow();
        if (!GUI.changed) return;
        var selectedIds = SchoolDataDictionary
            .Where(kv => kv.Value)
            .Select(kv => kv.Key.ToString())
            .ToArray();
        SchoolDataOverridePatch.TrySetSchoolData(string.Join("/", selectedIds));
    }
}
