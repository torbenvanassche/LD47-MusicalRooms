using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Container/Audio", fileName = "AudioData"), Serializable]
public class AudioFileSettings : SerializedScriptableObject
{
    [Space] public AudioClip clip;
    
    [Range(0, 256)] public int Priority = 128;
    [Range(0, 1)] public float Volume = 1;
    [Range(0, 1)] public float Pitch = 1;
    public bool Loop = false;
}
