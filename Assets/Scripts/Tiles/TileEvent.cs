using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TileEvent : MonoBehaviour
{
    [SerializeField, ValueDropdown(nameof(GetAudioFiles))] private AudioFileSettings _audioFile = null;
    public float highlightDuration = 1;

    public UnityEvent action = new UnityEvent();
    [HideInInspector] public bool completed = false;
    private Texture2D defaultTexture;

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

        defaultTexture = GetComponent<MeshRenderer>().material.mainTexture as Texture2D;
    }

    public void ChangeMaterial(Texture2D tex)
    {
        gameObject.GetComponent<MeshRenderer>().material.mainTexture = tex;
        if (highlightDuration != 0)
        {
            StartCoroutine(nameof(ResetMaterial), highlightDuration);   
        }
    }

    private IEnumerator ResetMaterial(float f)
    {
        yield return new WaitForSeconds(f);
        GetComponent<MeshRenderer>().material.mainTexture = defaultTexture;
        yield return null;
    }
}
