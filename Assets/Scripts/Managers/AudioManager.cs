using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Space, InlineEditor(InlineEditorObjectFieldModes.Hidden)]public AudioContainer audioContainer = null;
    [ReadOnly] private List<AudioSource> _sources = new List<AudioSource>();
    private GameObject _soundSourceContainer = null;

    [SerializeField] private AudioMixerGroup mixer;

#if UNITY_EDITOR
    private string _assetPath = "Assets/Audio/AudioData.asset";
    private void Reset()
    {
        audioContainer = AssetDatabase.LoadAssetAtPath<AudioContainer>(_assetPath);
        if (!audioContainer)
        {
            audioContainer = ScriptableObject.CreateInstance<AudioContainer>();
            AssetDatabase.CreateAsset(audioContainer, _assetPath);
        }
    }
#endif

    private void DestroySource(AudioSource t)
    {
        _sources.Remove(t);
        DestroyImmediate(t);
    }

    public void SetMasterVolume(float amount)
    {
        mixer.audioMixer.SetFloat("MasterVolume", amount);
    }

    private AudioSource GetAvailableSource(AudioFileSettings file)
    {
        //return null if the sound provided is already playing
        if (_sources.Exists(source => source.isPlaying && source.clip != file.clip))
        {
            return null;
        }
        
        //gather the sources that are not playing
        var notPlaying = _sources.FindAll(source => !source.isPlaying);
        for (var index = 0; index < notPlaying.Count; index++)
        {
            var t = notPlaying[index];
            notPlaying.Remove(t);
            DestroySource(t);
        }

        if (notPlaying.Count != 0) return notPlaying[0];
        {
            var source = _soundSourceContainer.AddComponent<AudioSource>();
            source.playOnAwake = false;
            _sources.Add(source);
            return source;
        }

    }

    public void PlaySound(AudioFileSettings file)
    {
        var source = GetAvailableSource(file);
        if (!source) return;
        
        source.clip = file.clip;
        source.outputAudioMixerGroup = file.mixerGroup;
        
        source.volume = file.Volume;
        source.pitch = file.Pitch;
        source.priority = file.Priority;
        source.loop = file.Loop;
        source.outputAudioMixerGroup = file.mixerGroup;

        
        source.Play();
    }

    public void StopSound(AudioFileSettings file)
    {
        var sound = _sources.Where(x => x.clip == file.clip);
        foreach (var audioSource in sound) Destroy(audioSource);
    }

    public void Awake()
    {
        _soundSourceContainer = new GameObject {name = "SoundPlayer"};
        _soundSourceContainer.transform.SetParent(transform);
    }
}
