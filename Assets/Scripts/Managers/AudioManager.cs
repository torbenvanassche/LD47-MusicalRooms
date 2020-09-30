using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [InlineEditor(InlineEditorObjectFieldModes.Hidden), ShowIf(nameof(IsPlaying))]public AudioContainer audioContainer = null;
    [SerializeField, ReadOnly, ShowIf(nameof(SourcesAlive))] private List<AudioSource> sources = new List<AudioSource>();
    private GameObject soundSourceContainer = null;

    private bool IsPlaying()
    {
        return !Application.isPlaying;
    }

    private bool SourcesAlive()
    {
        return sources.Count != 0;
    }
    
    private void Reset()
    {
        audioContainer = AssetDatabase.LoadAssetAtPath<AudioContainer>("Assets/AudioData.asset");
    }

    private void LateUpdate()
    {
        //Get all sources that are currently Idle
        var notPlaying = sources.FindAll(source => !source.isPlaying);
        for (var index = 0; index < notPlaying.Count; index++)
        {
            var t = notPlaying[index];
            sources.Remove(t);
            notPlaying.Remove(t);
            Destroy(t);
        }
    }

    private AudioSource GetAvailableSource()
    {
        var notPlaying = sources.FindAll(source => !source.isPlaying);
        
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
        soundSourceContainer = new GameObject();
        soundSourceContainer.name = "SoundPlayer";
        soundSourceContainer.transform.SetParent(transform);
        
        PlaySound(audioContainer.Get("Wacky Waiting"));
    }
}
