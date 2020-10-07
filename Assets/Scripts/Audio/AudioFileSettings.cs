using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Container/Audio", fileName = "AudioData"), Serializable]
public class AudioFileSettings : SerializedScriptableObject
{
    public AudioClip clip;
    public AudioMixerGroup mixerGroup;
    
    [Range(0, 256), Space] public int Priority = 128;
    [Range(0, 1)] public float Volume = 1;
    [Range(0, 1)] public float Pitch = 1;
    public bool Loop = false;
}
