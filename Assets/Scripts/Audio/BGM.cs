using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGM : MonoBehaviour
{
    public AudioFileSettings initialBGM;
    private AudioFileSettings queued;

    private AudioSource source;

    public void Start()
    {
        source = GetComponent<AudioSource>();
        if (initialBGM) PlaySound(initialBGM);
        else source.clip = null;
    }

    private void Update()
    {
        if (queued && !source.isPlaying)
        {
            PlaySound(queued);
            queued = null;
        }
    }

    public void Queue(AudioFileSettings file = null)
    {
        if (!file) file = initialBGM;
        
        queued = file;
        source.loop = false;
    }

    private void PlaySound(AudioFileSettings audio)
    {
        source.clip = audio.clip;
        source.volume = audio.Volume;
        source.pitch = audio.Pitch;
        source.priority = audio.Priority;
        source.loop = true;
        source.outputAudioMixerGroup = audio.mixerGroup;
        
        source.Play();
    }
}
