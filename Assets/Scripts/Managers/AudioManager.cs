using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [ValueDropdown(nameof(GetAudioFiles)), HorizontalGroup("PlaySound"), HideLabel, SerializeField] private AudioFileSettings selected;
    [HorizontalGroup("PlaySound"), Button] void PlaySound()
    {
        PlaySound(selected);
    }

    private IEnumerable GetAudioFiles()
    {
        return audioContainer.data.Values;
    }
    
    [Space, InlineEditor(InlineEditorObjectFieldModes.Hidden), ShowIf(nameof(IsPlaying))]public AudioContainer audioContainer = null;
    [SerializeField, ReadOnly] private List<AudioSource> sources = new List<AudioSource>();
    [SerializeField] private GameObject soundSourceContainer = null;

    private bool IsPlaying()
    {
        return !Application.isPlaying;
    }

    private void Reset()
    {
        audioContainer = AssetDatabase.LoadAssetAtPath<AudioContainer>("Assets/AudioData.asset");
    }

    private AudioSource GetAvailableSource()
    {
        var notPlaying = sources.FindAll(source => !source.isPlaying);
        for (var index = 0; index < notPlaying.Count; index++)
        {
            var t = notPlaying[index];
            sources.Remove(t);
            notPlaying.Remove(t);
            if (Application.isPlaying)
            {
                Destroy(t);   
            }
            else
            {
                DestroyImmediate(t);
            }
        }
        
        //if no audiosources, return a new one
        if (notPlaying.Count == 0)
        {
            var source = soundSourceContainer.AddComponent<AudioSource>();
            sources.Add(source);
            return source;
        }

        return notPlaying[0];
    }

    public void PlaySound(AudioFileSettings audio)
    {
        var source = GetAvailableSource();
        
        source.clip = audio.clip;
        source.volume = audio.Volume;
        source.pitch = audio.Pitch;
        source.priority = audio.Priority;
        source.loop = audio.Loop;
        
        source.Play();
    }

    public void Awake()
    {
        soundSourceContainer = new GameObject {name = "SoundPlayer"};
        soundSourceContainer.transform.SetParent(transform);
    }
}
