using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomTileContainer))]
public class RoomContainerEditor : OdinEditor
{
    protected override void OnEnable()
    {
        //Create folder if it doesnt exist
        var folderPath = "Assets/Sprites/Containers";
        if (!AssetDatabase.IsValidFolder(folderPath)) AssetDatabase.CreateFolder("Assets/Sprites", "Containers");
        
        var t = target as RoomTileContainer;

        var clips = AssetDatabase.FindAssets($"t:{nameof(Sprite)}", new[] {"Assets/Sprites/Source"});
        
        //find function data
        var functions = AssetDatabase.FindAssets($"t:{nameof(TileFunctions)}", new[] {"Assets/Sprites"});
        if (functions.Length == 0)
        {
            AssetDatabase.CreateAsset(CreateInstance<TileFunctions>(), "Assets/Sprites/TileFunctions.asset");
        }

        foreach (var clip in clips)
        {
            var path = AssetDatabase.GUIDToAssetPath(clip);
            var container = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            if (t.data.All(pair => pair.Value.tile != container))
            {
                var settings = CreateInstance<RoomTile>();
                settings.name = container.name;
                settings.tile = container;

                AssetDatabase.CreateAsset(settings, $"Assets/Sprites/Containers/{settings.name}.asset");
                t.data.Add(settings.name, settings);
            }
        }
    }
}
