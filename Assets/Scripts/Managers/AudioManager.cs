﻿using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Space, InlineEditor(InlineEditorObjectFieldModes.Hidden), ShowIf(nameof(IsPlaying))]public AudioContainer audioContainer = null;
    private List<AudioSource> sources = new List<AudioSource>();
    private GameObject soundSourceContainer = null;

    private void RemoveSource(AudioSource a)
    {
        soundSourceContainer.GetComponents<AudioSource>().Where(x => x == a).ForEach(DestroySource);
    }

    private bool IsPlaying()
    {
        return !Application.isPlaying;
    }

    #if UNITY_EDITOR
    private void Reset()
    {
        audioContainer = AssetDatabase.LoadAssetAtPath<AudioContainer>("Assets/AudioData.asset");
    }
    #endif

    private void DestroySource(AudioSource t)
    {
        if (Application.isPlaying)
        {
            Destroy(t);   
        }
        else
        {
            DestroyImmediate(t);
        }
        
        sources.Remove(t);
    }

    private AudioSource GetAvailableSource(AudioFileSettings audio)
    {
        if (sources.Exists(source => source.isPlaying && source.clip != audio.clip))
        {
            return null;
        }
        
        var notPlaying = sources.FindAll(source => !source.isPlaying);
        for (var index = 0; index < notPlaying.Count; index++)
        {
            var t = notPlaying[index];
            notPlaying.Remove(t);
            DestroySource(t);
        }
        
        if (notPlaying.Count == 0)
        {
            var source = soundSourceContainer.AddComponent<AudioSource>();
            source.playOnAwake = false;
            sources.Add(source);
            return source;
        }

        return notPlaying[0];
    }

    public void PlaySound(AudioFileSettings audio)
    {
        var source = GetAvailableSource(audio);

        if (source)
        {
            source.clip = audio.clip;
            source.volume = audio.Volume;
            source.pitch = audio.Pitch;
            source.priority = audio.Priority;
            source.loop = audio.Loop;
        
            source.Play();   
        }
    }

    public void StopSound(AudioFileSettings audio)
    {
        var sound = sources.Where(x => x.clip == audio.clip);
        foreach (var audioSource in sound)
        {
            Destroy(audioSource);
        }
    }

    public void Awake()
    {
        soundSourceContainer = new GameObject {name = "SoundPlayer"};
        soundSourceContainer.transform.SetParent(transform);
    }
}
