using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioContainer))]
public class AudioContainerEditor : OdinEditor
{
    protected override void OnEnable()
    {
        //Create folder if it doesnt exist
        var folderPath = "Assets/Audio/Containers";
        if (!AssetDatabase.IsValidFolder(folderPath)) AssetDatabase.CreateFolder("Assets/Audio", "Containers");
        
        var t = target as AudioContainer;

        var clips = AssetDatabase.FindAssets($"t:{nameof(AudioClip)}");

        foreach (var clip in clips)
        {
            var path = AssetDatabase.GUIDToAssetPath(clip);
            var audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
            if (t.data.All(pair => pair.Value.clip != audioClip))
            {
                var settings = CreateInstance<AudioFileSettings>();
                settings.clip = audioClip;
                settings.name = audioClip.name;
                
                AssetDatabase.CreateAsset(settings, $"Assets/Audio/Containers/{settings.name}.asset");
                t.data.Add(settings.name, settings);
            }
        }
    }
}
