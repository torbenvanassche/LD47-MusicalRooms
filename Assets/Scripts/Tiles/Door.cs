using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class Door : SerializedMonoBehaviour
{
    [SerializeField, ValueDropdown(nameof(GetAudioFiles))] public AudioFileSettings audioFile = null;

    private IEnumerable GetAudioFiles()
    {
        return Manager.Instance.audio ? Manager.Instance.audio.audioContainer.data.Values : null;
    }

    
    public void Open()
    {
        if(audioFile) Manager.Instance.audio.PlaySound(audioFile);
        gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(true);
    }
}
