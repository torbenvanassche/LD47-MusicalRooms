using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TileEvent : MonoBehaviour
{
    [SerializeField, ValueDropdown(nameof(GetAudioFiles))] private AudioFileSettings _audioFile = null;
    public UnityEvent action = null;
    [HideInInspector] public bool completed = false;

    private IEnumerable GetAudioFiles()
    {
        return Manager.Instance._audio ? Manager.Instance._audio.audioContainer.data.Values : null;
    }
    
    public void SetCompleted()
    {
        completed = true;
    }

    private void Start()
    {
        action.AddListener(() =>
        {
            if (_audioFile && Manager.Instance._audio)
            {
                Manager.Instance._audio.PlaySound(_audioFile);
            }
        });
    }
}
