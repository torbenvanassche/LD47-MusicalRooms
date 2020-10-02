using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Container/Audio list", fileName = "AudioDataContainer")]
public class AudioContainer : SerializedScriptableObject
{
    public Dictionary<string, AudioFileSettings> data = new Dictionary<string, AudioFileSettings>();

    public AudioFileSettings Get(string key)
    {
        return data.TryGetValue(key, out var clip) ? clip : null;
    }
}
