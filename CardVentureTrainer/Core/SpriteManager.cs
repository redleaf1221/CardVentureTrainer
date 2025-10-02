using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CardVentureTrainer.Core;

public static class SpriteManager {
    private static readonly Dictionary<string, Sprite> Sprites = new();

    private static void AddSprite(string name, Sprite sprite) {
        Sprites.Add(name, sprite);
    }

    public static Sprite GetSprite(string name) {
        Sprites.TryGetValue(name, out Sprite sprite);
        if (sprite != null) return sprite;
        Plugin.Logger.LogError($"Sprite Not Found: {name}");
        return null;
    }

    public static void InitSpriteManager() {
        var assembly = Assembly.GetExecutingAssembly();
        assembly.GetManifestResourceNames()
            .Where(name => name.StartsWith("CardVentureTrainer.Resources.Sprites"))
            .Select(resourceName => new {
                FullPath = resourceName,
                FileName = resourceName.Split(".")[^2]
            })
            .Select(resource => new {
                Name = resource.FileName,
                Sprite = LoadSpriteFromResource(assembly, resource.FullPath, resource.FileName)
            })
            .ToList()
            .ForEach(sprite => AddSprite(sprite.Name, sprite.Sprite));
        Plugin.Logger.LogInfo($"SpriteManager initialized, {Sprites.Count} sprites loaded.");
    }

    private static Sprite LoadSpriteFromResource(Assembly assembly, string resourcePath, string spriteName) {
        using Stream stream = assembly.GetManifestResourceStream(resourcePath);
        if (stream == null) {
            Plugin.Logger.LogError($"Stream is null: {resourcePath}");
            return null;
        }

        var buffer = new byte[stream.Length];
        if (stream.Read(buffer, 0, buffer.Length) != stream.Length) {
            Plugin.Logger.LogError($"Stream read terminated early: {resourcePath}");
            return null;
        }

        var texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        if (!texture.LoadImage(buffer)) {
            Plugin.Logger.LogError($"LoadImage failed: {resourcePath}");
            return null;
        }
        texture.filterMode = FilterMode.Point;

        var sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );
        sprite.name = spriteName;

        return sprite;
    }
}
