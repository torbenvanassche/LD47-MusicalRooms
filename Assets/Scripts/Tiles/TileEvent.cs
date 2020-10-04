using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TileEvent : MonoBehaviour
{
    [SerializeField, ValueDropdown(nameof(GetAudioFiles))] private AudioFileSettings _audioFile = null;

    public UnityEvent action = new UnityEvent();
    [HideInInspector] public bool completed = false;
    [HideInInspector] public List<Texture2D> tileClickAnimation = new List<Texture2D>();
    public float highlightDuration = 1;

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

            gameObject.GetComponent<MeshRenderer>().material.mainTexture = tileClickAnimation[1];
            if (highlightDuration != 0)
            {
                StartCoroutine(nameof(ResetMaterial), highlightDuration);   
            }
        });
    }

    private IEnumerator ResetMaterial(float f)
    {
        yield return new WaitForSeconds(f);
        GetComponent<MeshRenderer>().material.mainTexture = tileClickAnimation[0];
        yield return null;
    }
}
